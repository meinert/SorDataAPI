using static StartupHelper;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and services to the container
builder.Services.AddControllers(); // Injecting Controllers
builder.Services.AddScoped<IOrganizationService, OrganizationService>(); // Injecting OrganizationService
builder.Services.AddSingleton<IOrganizationDataProvider, CsvOrganizationDataProvider>(); // Injecting data provider


// Swagger and OpenAPI
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Add Swagger services and include XML comments.
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

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
