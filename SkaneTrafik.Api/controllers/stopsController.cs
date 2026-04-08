using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

[ApiController]
[Route("api/stops")]
public class StopsController : ControllerBase
{
    private readonly MySqlConnection _db;

    public StopsController(MySqlConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetStops([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query)) return Ok(new List<object>());

        await _db.OpenAsync();
        var cmd = new MySqlCommand(
            @"SELECT stopId, stopName, date, StopPlatformCode 
                FROM Stops 
                WHERE stopName LIKE @name 
                AND date = @date
                ORDER BY 
                    CASE 
                        WHEN stopName = @exact THEN 0
                        WHEN stopName LIKE @starts THEN 1
                        ELSE 2
                    END,
                    stopName ASC;",
            _db
        );

        // Använd bara ett ställe för wildcards för att ha kontroll
        cmd.Parameters.AddWithValue("@name", $"%{query}%");
        cmd.Parameters.AddWithValue("@exact", query);
        cmd.Parameters.AddWithValue("@starts", $"{query}%");
        cmd.Parameters.AddWithValue("@date", new DateTime(2025, 10, 4));

        using var reader = await cmd.ExecuteReaderAsync();
        var stops = new List<object>();

        while (await reader.ReadAsync())
        {
            stops.Add(new
            {
                id = reader.GetString("stopId"),
                name = reader.GetString("stopName"),
                date = reader.GetDateTime("date"),
                platformCode = reader.GetString("StopPlatformCode")
            });
        }

        await _db.CloseAsync();
        return Ok(stops);
    }
}