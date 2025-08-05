# Recipe API

A .NET 9 Web API for managing recipes with Entity Framework Core and SQL Server.

## Project Structure

```
RecipeApi/
├── Data/
│   └── RecipeDbContext.cs          # Database context and entity configuration
├── DTOs/
│   └── RecipeDto.cs                # Data Transfer Objects for API requests/responses
├── Models/
│   └── Recipe.cs                   # Recipe entity model
├── Migrations/                     # Database migration files
├── Program.cs                      # Application entry point and configuration
├── appsettings.json               # Configuration including database connection string
└── RecipeApi.http                 # HTTP requests for testing API endpoints
```

## Database Setup

### Connection String
Located in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RecipeApiDb;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### Database Context
The `RecipeDbContext` class extends `DbContext` and is database-agnostic. The actual database type is determined by:
1. **NuGet Package**: Currently using `Microsoft.EntityFrameworkCore.SqlServer`
2. **Service Registration**: Configured in `Program.cs`
3. **Connection String**: Points to SQL Server instance

### Entity Framework Core - Database Agnostic
The `DbContext` can work with any database that has an EF Core provider:

- **SQL Server**: `Microsoft.EntityFrameworkCore.SqlServer`
- **PostgreSQL**: `Npgsql.EntityFrameworkCore.PostgreSQL`
- **SQLite**: `Microsoft.EntityFrameworkCore.Sqlite`
- **MySQL**: `Pomelo.EntityFrameworkCore.MySql`
- **In-Memory**: `Microsoft.EntityFrameworkCore.InMemory` (for testing)

## Database Migrations

### How Migrations Work
'dotnet ef migrations add InitialCreate' is the command that will generate an InitialCreate.cs
EF Core scans the DBContext and finds all DbSet<T> properties to build tables off of.
Since we only have DbSet<Recipe>, only the Recipes table is created.
'dotnet ef database update' executes the SQL commands in the migration.
When we ran that in this project, the Recipes table was created in the RecipeApiDb db because that's what we configured in our connectionString, RecipeDbContext, and Program.cs
1. **Model Changes**: Modify your `Recipe` model or `RecipeDbContext` configuration
2. **Generate Migration**: `dotnet ef migrations add MigrationName`
3. **Apply to Database**: `dotnet ef database update`

### Migration Flow
```
C# Recipe Model → RecipeDbContext Config → Migration Generation → SQL Commands → Database Table
```

### Current Migration: InitialCreate
Creates the `Recipes` table with columns:
- `Id` (int, primary key, auto-increment)
- `Title` (nvarchar(200), required)
- `Description` (nvarchar(1000), optional)
- `Ingredients` (nvarchar(max), required)
- `Instructions` (nvarchar(max), required)
- `CookingTimeMinutes` (int, optional)
- `Servings` (int, optional)
- `Difficulty` (nvarchar(max), optional)
- `Cuisine` (nvarchar(max), optional)
- `CreatedAt` (datetime2, required, default: GETUTCDATE())
- `UpdatedAt` (datetime2, optional)

## API Endpoints (Planned)

Based on `RecipeApi.http`, the following endpoints will be implemented:

- `GET /api/recipes` - Get all recipes
- `GET /api/recipes/{id}` - Get recipe by ID
- `POST /api/recipes` - Create new recipe
- `PUT /api/recipes/{id}` - Update existing recipe
- `DELETE /api/recipes/{id}` - Delete recipe

## Setup Instructions

### Prerequisites
- .NET 9 SDK
- SQL Server (local or remote)
- SQL Server Management Studio (SSMS) for database management

### Installation Steps
1. **Clone/Download** the project
2. **Restore Dependencies**: `dotnet restore`
3. **Apply Database Migration**: `dotnet ef database update`
4. **Run the Application**: `dotnet run`

### Database Connection
The application will connect to:
- **Server**: localhost
- **Database**: RecipeApiDb
- **Authentication**: Windows Authentication (Trusted_Connection=true)

## Missing Components

### 1. DbContext Service Registration
Add to `Program.cs`:
```csharp
builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### 2. Recipes Controller
Create `Controllers/RecipesController.cs` to handle HTTP requests for recipe operations.

## Development Tools

### RecipeApi.http
This file contains HTTP requests for testing API endpoints. Benefits:
- **Integrated Testing**: Test endpoints directly from IDE
- **Documentation**: Shows expected API structure
- **Team Collaboration**: Easy to share and use
- **Quick Debugging**: Test individual endpoints without external tools

### Entity Framework Tools
- **Migrations**: `dotnet ef migrations add/remove/update`
- **Database Updates**: `dotnet ef database update`
- **Scaffolding**: `dotnet ef dbcontext scaffold` (for existing databases)

## Key Concepts

### DbContext Constructor
```csharp
public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
```
- Receives configuration via `DbContextOptions`
- Database type determined by service registration in `Program.cs`
- Connection string and provider specified in options

### Migration Process
1. **Generate**: Creates C# migration file with SQL commands
2. **Apply**: Connects to database and executes SQL
3. **Track**: EF Core tracks applied migrations in `__EFMigrationsHistory` table

### Database Provider Pattern
- **Abstraction**: `DbContext` is database-agnostic
- **Configuration**: Database type set via NuGet package and service registration
- **Flexibility**: Same code works with different databases by changing provider

## Troubleshooting

### Common Issues
1. **Connection Failed**: Check SQL Server is running and connection string
2. **Migration Errors**: Ensure database exists and user has permissions
3. **Missing Dependencies**: Run `dotnet restore` to install NuGet packages

### Useful Commands
```bash
# Check EF Core tools
dotnet ef --version

# List migrations
dotnet ef migrations list

# Remove last migration
dotnet ef migrations remove

# Update database to specific migration
dotnet ef database update MigrationName
```

## Next Steps
1. Add DbContext service registration to `Program.cs`
2. Create `RecipesController` with CRUD endpoints
3. Test API endpoints using `RecipeApi.http`
4. Add validation and error handling
5. Implement additional features (search, filtering, etc.) 