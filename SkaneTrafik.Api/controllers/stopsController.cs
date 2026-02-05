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
    public async Task<IActionResult> GetStops([FromQuery] string from)
    {
        await _db.OpenAsync();
        var cmd = new MySqlCommand(
            @"SELECT * FROM Stops
          WHERE stopName LIKE CONCAT('%', @name, '%')
          AND date = @date",
            _db
        );
        Console.WriteLine($"Searching for: '{from}'");

        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = from + "%";
        cmd.Parameters.Add("@date", MySqlDbType.DateTime)
            .Value = new DateTime(2025, 10, 4);

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
