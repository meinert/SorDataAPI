# Sor Data API

The Sor Data API is a RESTful service designed to handle healthcare organization data from the Danish "SOR" system. The project fetches, processes, and exposes this data for lookup and traversal operations.

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

**1. Install the latest .NET SDK**

Using a Linux environment this can be done using

``` Bash
sudo apt-get install -y dotnet-runtime-9.0
```

Check the proper version is installed. This application was build using version **9.0.102**

``` Bash
dotnet --version
```


**2. Run the project**

Run the following command to restore dependencies and run the project:

``` Bash
dotnet run
```

## Swagger/OpenAPI Documentation
   - A Swagger UI is available at `http://localhost:5211/index.html`, which provides interactive documentation for the API.
   - The Swagger UI allows testing endpoints and viewing request/response details.

## Application Flow

The Sor Data API processes data from a CSV file to dynamically serve information through REST endpoints. Below is an overview of the application's flow:

### 1. **Application Startup**
   - The application starts when `Program.cs` executes.
   - Core services and configurations are registered in the dependency injection container:
     - `IOrganizationService` is implemented by `OrganizationService` to provide business logic.
     - `IOrganizationDataProvider` is implemented by `CsvOrganizationDataProvider` to manage the organization's data in memory.
   - Swagger/OpenAPI is configured for endpoint documentation.

### 2. **CSV File Handling**
   - On startup, the application ensures that the required `sor.csv` file is present. If not, it is downloaded from a predefined URL.
   - The file is parsed using the `CsvParser` utility, which converts it into a hierarchical data model representing organizations and their relationships.

### 3. **Data Loading**
   - Once parsed, the organization data is passed to the `CsvOrganizationDataProvider` service, where it is stored in memory.
   - The data model is structured into parent-child relationships to enable hierarchical queries.

### 4. **Exposed REST Endpoints**
   The following REST endpoints are exposed to interact with the data:
   
   #### 4.1. **Lookup Endpoint**
   - **Purpose:** Retrieve information about an organization using its unique SOR code.
   - **Flow:**
     1. The SOR code is validated as part of input validation.
     2. The application searches for the organization in memory.
     3. If found, the details are returned as a DTO.
     4. If not found, a `404 Not Found` response is returned.

   #### 4.2. **Traverse Endpoint**
   - **Purpose:** Traverse upwards in the hierarchy to find the top-level parent organization.
   - **Flow:**
     1. Input validation ensures a valid SOR code is provided.
     2. The application recursively identifies the top-level parent organization.
     3. If a parent organization is found, it is returned as a DTO.
     4. If the organization or parent cannot be determined, a `404 Not Found` or `400 Bad Request` response is returned.

### 5. **Error Handling**
   - The application uses appropriate HTTP status codes for various scenarios:
     - `200 OK`: Successful responses.
     - `400 Bad Request`: Invalid input, such as invalid SOR codes.
     - `404 Not Found`: Data not found in memory.
     - `501 Not Implemented`: Features not yet implemented.

## Project folder structure

```
sln/                     # Solution dir
├── SorDataAPI.Tests     # Test project
└── SorDataAPI           # Main project
    ├── Properties/          # Assembly-related files (e.g., launchSettings.json)
    └── Src
        ├── Controllers/         # API endpoints are defined here
        ├── Data/                # Database context and migration files
        ├── Models/              # Data models and DTOs
        ├── Repositories/        # Data access logic and repository pattern implementation
        ├── Services/            # Business logic and service classes
        ├── Utilities/           # Helper or utility classes and methods
        ├── Configurations/      # Configuration classes for appsettings.json or custom configs
        ├── Validators/          # For ValidationAttribute classes
        ├── wwwroot/             # Static files (if serving static content)
        ├── Program.cs           # Main entry point of the application
        ├── SorDataAPI.csproj
        ├── appsettings.Development.json     
        └── appsettings.json     # Application configuration
```




## Branching strategy

The project follows a standard Git Flow branching strategy. See [HERE](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow). Note that the git repository is not using the git flow extension, but vanilla git inspired by the git flow branching strategy.


# Notes for implementation

Design prior to implementation. This is is left here to show the process. **It deviates from the final application.**

```
src/
├── Controllers/
│   └── OrganizationController.cs
├── Models/
│   ├── Organization.cs
│   ├── OrganizationDto.cs
├── Services/
    ├── IOrganizationService.cs
│   └── OrganizationService.cs
├── Repositories/
│   ├── CsvOrganizationDataProvider.cs
│   └── IOrganizationDataProvider.cs
├── Utilities/
│   └── CsvHelperUtility.cs
├── Configurations/
│   ├── DatabaseConfig.cs
│   └── SwaggerConfig.cs
├── Program.cs
└── appsettings.json
```