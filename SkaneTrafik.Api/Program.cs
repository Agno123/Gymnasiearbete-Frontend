using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DB connection
builder.Services.AddScoped<MySqlConnection>(_ =>
    new MySqlConnection(
        builder.Configuration.GetConnectionString("SkaneTrafikDb")
    )
);

// CORS for SvelteKit
builder.Services.AddCors(options =>
{
     options.AddPolicy("AllowSvelte", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowSvelte"); 

app.MapControllers();
app.Run();

