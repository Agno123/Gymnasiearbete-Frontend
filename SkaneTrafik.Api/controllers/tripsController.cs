using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

[ApiController]
[Route("api/trips")]
public class TripsController : ControllerBase
{
    private readonly MySqlConnection _db;

    public TripsController(MySqlConnection db)
    {
        _db = db;
    }

    string name = "%Munka Ljungby%";

    [HttpGet]
    public async Task<IActionResult> GetStops()
    {
        await _db.OpenAsync();

        var cmd = new MySqlCommand(
            "SELECT * FROM Trips WHERE tripId LIKE @name",
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
                name = reader.GetString("stopName")
            });
        }

        await _db.CloseAsync();

        return Ok(stops);
    }
}
