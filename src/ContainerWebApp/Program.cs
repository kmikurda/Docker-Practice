using Npgsql;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
 

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_URL") ?? "localhost:6379")
);

builder.Services.AddTransient<Func<NpgsqlConnection>>(sp => () => 
    new NpgsqlConnection(Environment.GetEnvironmentVariable("DATABASE_URL"))
);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.MapGet("/db", async (Func<NpgsqlConnection> createConnection) =>
{
    try
    {
        using var dbConn = createConnection();
        await dbConn.OpenAsync();
        using var cmd = new NpgsqlCommand("SELECT version()", dbConn);
        var result = await cmd.ExecuteScalarAsync();
        return Results.Ok($"PostgreSQL version: {result}");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Database connection error: {ex.Message}");
    }
});

app.MapGet("/redis", async (IConnectionMultiplexer redis) =>
{
    try
    {
        var db = redis.GetDatabase();
        await db.StringSetAsync("test_key", $"Hello Redis! Time: {DateTime.Now}");
        var value = await db.StringGetAsync("test_key");
        return Results.Ok($"Redis Works!: {value}");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Redis connection Error: {ex.Message}");
    }
});

app.Run();