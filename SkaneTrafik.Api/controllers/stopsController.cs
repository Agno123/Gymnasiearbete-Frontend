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

    string name = "Ängelholm station";

    [HttpGet]
    public async Task<IActionResult> GetStops()
    {
        await _db.OpenAsync();

        var cmd = new MySqlCommand(
            "SELECT * FROM Stops WHERE stopName LIKE @name AND date LIKE '2025-10-04 00:00:00.000000'",
            _db
        );

        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name + "%";

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
