using SorDataAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and services to the container
builder.Services.AddControllers(); // Injecting Controllers
builder.Services.AddScoped<IOrganizationService, OrganizationService>(); // Injecting OrganizationService
builder.Services.AddSingleton<IOrganizationDataProvider, CsvOrganizationDataProvider>(); // Injecting data provider


// Swagger and OpenAPI
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Checks if the SOR.csv files has been downloaded, else downloads
await EnsureCsvDataAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ConfigureSwagger(app);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

/// <summary>
/// Ensures the CSV file is downloaded and parsed into the application.
/// </summary>
async Task EnsureCsvDataAsync(IServiceProvider services)
{
    // Ensure CSV file is downloaded
    await CsvFileRetriever.EnsureCsvFileAsync();

    // Parse the CSV file
    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "sor.csv");
    var organizations = CsvParser.ParseCsvFile(filePath);

    // Output the number of organizations parsed (for debugging purposes)
    Console.WriteLine($"Parsed {organizations.Count()} organizations from the CSV file.");

    // Get the scoped data provider and set the organizations
    var dataProvider = services.GetRequiredService<IOrganizationDataProvider>();
    dataProvider.SetOrganizations(organizations);

}

/// <summary>
/// Configures Swagger UI for development environments.
/// </summary>
void ConfigureSwagger(WebApplication app)
{
    app.MapOpenApi();
    // app.MapGet("/", () => "Hello world! Made by Peter Fjordbak Poulsen");

    // Configure Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SorDataAPI v1");
        options.RoutePrefix = string.Empty; // Swagger UI available at the root URL
    });
}
