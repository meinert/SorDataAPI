# Sor Data API

This is a 

**Project features**:
- retrieves the sor.csv file from https://sor-filer.sundhedsdata.dk/sor_produktion/data/sor/sorcsv/V_1_2_1_19/sor.csv (hardcoded for now) if not already downloaded
- Imports the data and saves it in memory, using a relevant hierarchial domain model
- Exposes a REST interface with the following endPoints - documentation of the endPoints can be found in ([`Swagger`](https://localhost:5122/Swagger)):

  - Lookup Endpoint
  - Traverse Endpoint

**Future features**:
- TLS encryption of the endpoints
- Authentication endpoint to get a Json-Web-Token (JWT)
- Proper logging framework, e.g. Serilog instead of writing to console
- Re-Download CSV if file already downloaded is old

## Setting up and starting the application



## Project folder structure

```
src/
├── Controllers/         # API endpoints are defined here
├── Models/              # Data models and DTOs
├── Services/            # Business logic and service classes
├── Repositories/        # Data access logic and repository pattern implementation
├── Data/                # Database context and migration files
├── Utilities/           # Helper or utility classes and methods
├── Configurations/      # Configuration classes for appsettings.json or custom configs
├── Middlewares/         # Custom middleware for request processing
├── Filters/             # Action filters and exception filters
├── Properties/          # Assembly-related files (e.g., launchSettings.json)
├── wwwroot/             # Static files (if serving static content)
├── Program.cs           # Main entry point of the application
└── appsettings.json     # Application configuration
```


Notes for implementation....clean up before submitting

```
src/
├── Controllers/
│   └── OrganizationController.cs
├── Models/
│   ├── Organization.cs
│   ├── OrganizationDto.cs
├── Services/
│   └── OrganizationService.cs
├── Repositories/
│   ├── IOrganizationRepository.cs
│   └── OrganizationRepository.cs
├── Data/
│   ├── AppDbContext.cs
│   └── Migrations/
├── Utilities/
│   └── CsvHelperUtility.cs
├── Configurations/
│   ├── DatabaseConfig.cs
│   └── SwaggerConfig.cs
├── Middlewares/
│   └── ErrorHandlingMiddleware.cs
├── Filters/
│   └── ExceptionFilter.cs
├── Properties/
│   └── launchSettings.json
├── wwwroot/
├── Program.cs
└── appsettings.json
```

## Branching strategy

The project follows a standard Git Flow branching strategy. The features for this 