using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

[ApiController]
[Route("api/travel")]
public class TravelController : ControllerBase
{
    private readonly MySqlConnection _db;

    public TravelController(MySqlConnection db)
    {
        _db = db;
    }

    [HttpGet("time")]
    public async Task<IActionResult> GetTravelTime(
        [FromQuery] string startName, 
        [FromQuery] string endName, 
        [FromQuery] string date)
    {
        if (string.IsNullOrEmpty(startName) || string.IsNullOrEmpty(endName) || string.IsNullOrEmpty(date))
        {
            return BadRequest("Parametrar saknas (startName, endName, date).");
        }

        // Formatera datumet för att matcha databasens format (YYYY-MM-DD 00:00:00)
        string formattedDate = $"{date} 00:00:00";

        await _db.OpenAsync();

        try
        {
            // SQL-Kommandot för att hämta en resa
            var cmd = new MySqlCommand(
                @"SELECT DISTINCT 
                    r.RouteLongName AS Linje,
                    st1.DepartureTime AS Avgång, 
                    st2.ArrivalTime AS Ankomst,
                    CONCAT(ROUND(TIME_TO_SEC(TIMEDIFF(st2.ArrivalTime, st1.DepartureTime)) / 60), ' min') AS Restid
                FROM Stops s1
                INNER JOIN StopTimes st1 ON s1.StopId = st1.StopId AND s1.date = st1.date
                INNER JOIN StopTimes st2 ON st1.TripId = st2.TripId AND st1.date = st2.date
                INNER JOIN Stops s2 ON s2.StopId = st2.StopId AND s2.date = st2.date
                INNER JOIN Trips t ON st1.TripId = t.TripId AND st1.date = t.date
                INNER JOIN Routes r ON t.RouteId = r.RouteId AND t.date = r.date
                WHERE s1.StopName = @startName 
                  AND s2.StopName = @endName
                  AND s1.date = @date
                  AND st1.StopSequence < st2.StopSequence
                ORDER BY st1.DepartureTime
                LIMIT 20;", 
                _db
            );

            // Parametrar för att skydda mot SQL Injection
            cmd.Parameters.AddWithValue("@startName", startName);
            cmd.Parameters.AddWithValue("@endName", endName);
            cmd.Parameters.AddWithValue("@date", formattedDate);

            using var reader = await cmd.ExecuteReaderAsync();
            var travelTimes = new List<object>();

            while (await reader.ReadAsync())
            {
                // Lägger till det i listan som resultat
                travelTimes.Add(new
                {
                    linje = reader.GetString("Linje"),
                    avgång = reader.GetString("Avgång"),
                    ankomst = reader.GetString("Ankomst"),
                    restid = reader.GetString("Restid")
                });
            }
            
            return Ok(travelTimes); // Returnerar resorna
        }
        finally
        {
            await _db.CloseAsync();
        }
    }
}