
# WebDevProject

This is a simple ASP.NET Core application that implements the Department management system. The project demonstrates the use of MVC architecture, Repository Pattern, Entity Framework, and SQL Server for managing and interacting with department data.

## Architecture Overview

This project follows a clean architecture with clear separation of concerns. Here's a breakdown of the structure and flow:

### 1. Repository Pattern:
The **Repository Pattern** is used to abstract data access logic. It decouples the application's business logic from the underlying data storage and provides a simple interface for interacting with the data layer.

### 2. Interfaces:
The `IDepartmentRepo` interface defines the methods that the repository will implement for interacting with the `Department` data. It provides a contract that ensures the repository class will have consistent data operations.

**File: `WebDevProject\BLL\interfaces\IDepartmentRepo.cs`**

```csharp
public interface IDepartmentRepo
{
    IEnumerable<Department> GetAll();
    int Add(Department d);
    int Update(Department d);
    int Delete(int id);
}
```

### 3. Repository Implementation:
The `DepartmentRepo` class implements the `IDepartmentRepo` interface. It contains the logic for interacting with the database. This is where the CRUD operations (Create, Read, Update, Delete) are defined, and the repository directly communicates with the `ApplicationDbContext` to fetch, add, update, or delete departments.

**File: `WebDevProject\BLL
epo\DepartmentRepo.cs`**

```csharp
public class DepartmentRepo : IDepartmentRepo
{
    private readonly ApplicationDbContext dbContext;

    public DepartmentRepo()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=NOURLAPTOP;Database=webdevproject;Trusted_Connection=True;TrustServerCertificate=True;")
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information);
        dbContext = new ApplicationDbContext(optionsBuilder.Options);
    }

    public IEnumerable<Department> GetAll()
    {
        return dbContext.Departments.ToList();
    }

    public int Add(Department d)
    {
        dbContext.Departments.Add(d);
        return dbContext.SaveChanges();
    }

    public int Update(Department d)
    {
        dbContext.Departments.Update(d);
        return dbContext.SaveChanges();
    }

    public int Delete(int currentdept)
    {
        var employeesToMove = dbContext.Employees.Where(e => e.Dep_ID == currentdept).ToList();
        foreach (var Employee in employeesToMove)
        {
            Employee.Dep_ID = 15; // Moving employees to another department (ID 15)
        }

        dbContext.SaveChanges();
        dbContext.Departments.Remove(dbContext.Departments.Find(currentdept));
        return dbContext.SaveChanges();
    }
}
```

### 4. Controller:
The `DepartmentController` serves as the middleman between the **View** and the **Repository**. It retrieves data from the repository and passes it to the views. It also handles user interactions (like deleting departments) by calling repository methods.

**File: `WebDevProject\Controllers\DepartmentController.cs`**

```csharp
public class DepartmentController : Controller
{
    private readonly DepartmentRepo depRepo;

    public DepartmentController()
    {
        depRepo = new DepartmentRepo();
    }

    public IActionResult getallview()
    {
        var departments = depRepo.GetAll();
        return View(departments);
    }

    public IActionResult Delete(int id)
    {
        depRepo.Delete(id);
        return RedirectToAction("getallview");
    }
}
```

### 5. Folder Structure:

The project's folder structure is as follows, providing a clear separation of layers:

```plaintext
WebDevProject/
├── Data/
│   └── ApplicationDbContext.cs (handles database context configuration)
├── BLL/
│   ├── repo/
│   │   └── DepartmentRepo.cs (implements the business logic for the department)
│   └── interfaces/
│       └── IDepartmentRepo.cs (defines repository contract)
├── Controllers/
│   └── DepartmentController.cs (interacts with repository and views)
└── Program.cs (configures services and database context)
```

---

## Getting Started

1. **Clone the Repository**

   To get started, clone the repository:

   ```bash
   git clone https://github.com/yourusername/WebDevProject.git
   ```

2. **Install Required Packages**

   Make sure to have the following packages installed:

   - **Entity Framework Core**:
     ```bash
     dotnet add package Microsoft.EntityFrameworkCore.SqlServer
     dotnet add package Microsoft.EntityFrameworkCore.Tools
     ```

3. **Update Database Connection String**

   Change the connection string in the `DepartmentRepo.cs` and `appsettings.json` to point to your local SQL Server instance.

   In `WebDevProject/BLL/repo/DepartmentRepo.cs`, update:
   ```csharp
   optionsBuilder.UseSqlServer("Server=YOUR_LOCALHOST;Database=webdevproject;Trusted_Connection=True;TrustServerCertificate=True;");
   ```

   In `appsettings.json`, update the connection string similarly.

4. **Migrate the Database**

   The project will automatically create the database. To apply the migrations and update the database, run:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

---

## Folder Structure

- **Data:** Contains the `ApplicationDbContext.cs`, which is responsible for interacting with the database.
- **BLL (Business Logic Layer):** Contains the interfaces and repository implementation for the department data operations.
- **Controllers:** Contains the `DepartmentController`, which handles user interactions and communicates with the repository to retrieve and manipulate data.
- **Program.cs:** Configures services, including adding the `ApplicationDbContext`.

---

## Summary

This application is built using the **Repository Pattern**, where we have a repository interface and an implementation that handles all database operations. The controller manages user interactions and calls the repository methods for data operations. The project follows a clean architecture for maintainability and scalability.
