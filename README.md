# .NET 8 DDD Template

A comprehensive Domain-Driven Design (DDD) template for .NET 8 applications following Clean Architecture principles.

## Project Structure

```
DDDTemplate/
├── src/
│   ├── DDDTemplate.Domain/           # Domain Layer (Core Business Logic)
│   │   ├── Entities/                 # Domain Entities
│   │   │   ├── Entity.cs            # Base Entity Class
│   │   │   └── Product.cs           # Sample Product Entity
│   │   ├── ValueObjects/            # Value Objects
│   │   │   ├── ValueObject.cs       # Base Value Object
│   │   │   └── Money.cs             # Money Value Object
│   │   ├── Events/                  # Domain Events
│   │   │   ├── IDomainEvent.cs      # Domain Event Interface
│   │   │   ├── ProductCreatedEvent.cs
│   │   │   └── ProductPriceChangedEvent.cs
│   │   ├── Exceptions/              # Domain Exceptions
│   │   │   ├── DomainException.cs   # Base Domain Exception
│   │   │   └── ProductDomainException.cs
│   │   └── Interfaces/              # Domain Interfaces
│   │       ├── IEntity.cs           # Entity Interface
│   │       └── IRepository.cs       # Repository Interface
│   │
│   ├── DDDTemplate.Application/      # Application Layer (Use Cases)
│   │   ├── Commands/                # Command Pattern (CQRS)
│   │   │   ├── ICommand.cs
│   │   │   ├── ICommandHandler.cs
│   │   │   ├── CreateProductCommand.cs
│   │   │   └── CreateProductCommandHandler.cs
│   │   ├── Queries/                 # Query Pattern (CQRS)
│   │   │   ├── IQuery.cs
│   │   │   ├── IQueryHandler.cs
│   │   │   ├── GetProductByIdQuery.cs
│   │   │   └── GetProductByIdQueryHandler.cs
│   │   ├── DTOs/                    # Data Transfer Objects
│   │   │   └── ProductDto.cs
│   │   ├── Services/                # Application Services
│   │   └── Interfaces/              # Application Interfaces
│   │       └── IApplicationService.cs
│   │
│   ├── DDDTemplate.Infrastructure/   # Infrastructure Layer (External Concerns)
│   │   ├── Data/                    # Database Context
│   │   │   └── ApplicationDbContext.cs
│   │   ├── Repositories/            # Repository Implementations
│   │   │   └── Repository.cs        # Generic Repository Implementation
│   │   └── Services/                # Infrastructure Services
│   │
│   └── DDDTemplate.API/             # Presentation Layer (Web API)
│       ├── Controllers/             # API Controllers
│       │   └── ProductsController.cs
│       └── Program.cs               # Application Entry Point
│
└── DDDTemplate.sln                  # Solution File
```

## Architecture Layers

### 1. Domain Layer (`DDDTemplate.Domain`)
- **Purpose**: Contains the core business logic and rules
- **Dependencies**: None (pure business logic)
- **Key Components**:
  - **Entities**: Business objects with identity (e.g., Product)
  - **Value Objects**: Objects without identity (e.g., Money)
  - **Domain Events**: Events that occur within the domain
  - **Domain Exceptions**: Business rule violations
  - **Interfaces**: Contracts for repositories and services

### 2. Application Layer (`DDDTemplate.Application`)
- **Purpose**: Orchestrates domain objects to fulfill use cases
- **Dependencies**: Domain Layer
- **Key Components**:
  - **Commands**: Write operations (CQRS pattern)
  - **Queries**: Read operations (CQRS pattern)
  - **DTOs**: Data transfer objects for API communication
  - **Application Services**: Coordinate domain objects

### 3. Infrastructure Layer (`DDDTemplate.Infrastructure`)
- **Purpose**: Implements external concerns (database, external APIs, etc.)
- **Dependencies**: Application Layer, Domain Layer
- **Key Components**:
  - **DbContext**: Entity Framework database context
  - **Repositories**: Data access implementations
  - **External Services**: Third-party integrations

### 4. API Layer (`DDDTemplate.API`)
- **Purpose**: HTTP API endpoints and presentation logic
- **Dependencies**: Application Layer, Infrastructure Layer
- **Key Components**:
  - **Controllers**: HTTP endpoints
  - **Program.cs**: Dependency injection and middleware configuration

## Key Features

### Domain-Driven Design Patterns
- **Entities**: Base entity class with domain events support
- **Value Objects**: Immutable objects with equality based on values
- **Domain Events**: Events raised by domain entities
- **Repository Pattern**: Abstraction for data access
- **Domain Exceptions**: Specific exceptions for business rule violations

### CQRS (Command Query Responsibility Segregation)
- **Commands**: Handle write operations
- **Queries**: Handle read operations
- **Handlers**: Process commands and queries

### Clean Architecture
- **Dependency Inversion**: Dependencies point inward toward the domain
- **Separation of Concerns**: Each layer has a specific responsibility
- **Testability**: Easy to unit test business logic

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. **Restore packages**:
   ```bash
   dotnet restore
   ```

2. **Build the solution**:
   ```bash
   dotnet build
   ```

3. **Run the API**:
   ```bash
   dotnet run --project src/DDDTemplate.API
   ```

4. **Access Swagger UI**:
   Navigate to `https://localhost:7xxx/swagger` (port may vary)

### Sample API Endpoints

#### Create a Product
```http
POST /api/products
Content-Type: application/json

{
  "name": "Sample Product",
  "description": "A sample product for testing",
  "price": 29.99,
  "currency": "USD",
  "stockQuantity": 100
}
```

#### Get a Product
```http
GET /api/products/{id}
```

## Extending the Template

### Adding a New Entity

1. **Create the entity** in `DDDTemplate.Domain/Entities/`
2. **Add domain events** in `DDDTemplate.Domain/Events/`
3. **Create DTOs** in `DDDTemplate.Application/DTOs/`
4. **Implement commands/queries** in respective folders
5. **Add controller** in `DDDTemplate.API/Controllers/`
6. **Register dependencies** in `Program.cs`

### Adding Database Support

Replace the in-memory database with a real database:

1. **Install EF Core provider** (e.g., SQL Server):
   ```bash
   dotnet add src/DDDTemplate.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
   ```

2. **Update connection string** in `appsettings.json`

3. **Update Program.cs** to use the database provider:
   ```csharp
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(connectionString));
   ```

4. **Add migrations**:
   ```bash
   dotnet ef migrations add InitialCreate --project src/DDDTemplate.Infrastructure --startup-project src/DDDTemplate.API
   ```

## Best Practices Implemented

- **Single Responsibility Principle**: Each class has one reason to change
- **Dependency Inversion**: High-level modules don't depend on low-level modules
- **Domain Events**: Decouple domain logic from side effects
- **Value Objects**: Encapsulate related data and behavior
- **Repository Pattern**: Abstract data access
- **CQRS**: Separate read and write operations
- **Clean Architecture**: Organize code by business concerns

## Technologies Used

- **.NET 8**: Latest .NET framework
- **Entity Framework Core**: ORM for data access
- **ASP.NET Core**: Web API framework
- **Swagger/OpenAPI**: API documentation
- **In-Memory Database**: For development and testing

## License

This template is provided as-is for educational and development purposes.
