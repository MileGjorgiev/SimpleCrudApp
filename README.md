# Setup Instructions
## Prerequisites
- .NET 8 SDK: Ensure you have the .NET 8 SDK installed. You can download it from here.

- SQL Server LocalDB: This is included with Visual Studio or can be installed separately. Ensure it's installed and running.

- Visual Studio or Visual Studio Code: Recommended for development.

Step 1: Clone the Repository  
Clone the repository to your local machine:
```
git clone [https://github.com/your-repo/crud-app-dotnet.git]
cd SimpleCrudApp
```
Step 2: Set Up the Database  
The application is configured to use SQL Server LocalDB with the following connection string in appsettings.json:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SimpleCrudApp;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
No changes are required.

Run the following command to apply migrations and create the database:
```
dotnet ef database update
```
Step 3: Run the Application
```
dotnet run
```
Step 4: Test the API
You can use Swagger UI to test the API endpoints.
