using SorDataAPI.Utils;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IOrganizationService, OrganizationService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Checks if the SOR.csv files has been downloaded, else downloads
await CsvFileRetriever.EnsureCsvFileAsync();

// Parse the CSV file
string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "sor.csv");
var organizations = CsvParser.ParseCsvFile(filePath);

// Output the number of organizations parsed (for debugging purposes)
Console.WriteLine($"Parsed {organizations.Count()} organizations from the CSV file.");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapGet("/", () => "Hello world! Made by Peter Fjordbak Poulsen");

    // Configure app to use Swagger
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SorDataAPI v1");
        options.RoutePrefix = string.Empty; // Optional: Makes Swagger UI available at the root URL
    });
}

app.UseHttpsRedirection();
// app.UseAuthorization();


// Map controllers to routes
app.MapControllers();


// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
