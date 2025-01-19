using SorDataAPI.Utils;

public static class StartupHelper
{
    /// <summary>
    /// Ensures the CSV file is downloaded and parsed into the application.
    /// </summary>
    public static async Task EnsureCsvDataAsync(IServiceProvider services)
    {
        await CsvFileRetriever.EnsureCsvFileAsync();

        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "sor.csv");
        var organizations = CsvParser.ParseCsvFile(filePath);

        Console.WriteLine($"Parsed {organizations.Count()} organizations from the CSV file.");

        var dataProvider = services.GetRequiredService<IOrganizationDataProvider>();
        dataProvider.SetOrganizations(organizations);
    }

    /// <summary>
    /// Configures Swagger UI for development environments.
    /// </summary>
    public static void ConfigureSwagger(WebApplication app)
    {
        app.MapOpenApi();

        app.MapGet("/easteregg", () => "Made by Peter Fjordbak Poulsen on his 45'th birthday");

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "SorDataAPI v1");
            options.RoutePrefix = string.Empty;
            options.DisplayRequestDuration();
        });
    }
}
