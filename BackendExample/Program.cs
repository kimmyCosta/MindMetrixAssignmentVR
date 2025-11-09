var builder = WebApplication.CreateBuilder(args);

if (args.Length == 0)
{
    builder.WebHost.UseUrls("http://localhost:5000");
}

var app = builder.Build();

app.MapPost("/api/reaction/event", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();
    Console.WriteLine($"Received JSON EVENT : {body}");
    return Results.Ok(new { message = "Data EVENT received successfully!" });
});

app.MapPost("/api/reaction/recap", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();
    Console.WriteLine($"Received JSON RECAP INFO : {body}");
    return Results.Ok(new { message = "Data RECAP received successfully!" });
});

app.Run();