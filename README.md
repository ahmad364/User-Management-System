# User-Management-System

## Clone the Repository
To get started, clone this repository to your local machine:
git clone <repository_url>
cd User-Management-System

## Prerequisites
Ensure the following packages are included in the application:
- **EF Core**
- **EF Core SQL Server**
- **EF Core Tools**
- **FluentValidation**
- **Swagger**
- **AutoMapper**

## Steps to Run the Application

1. **Update Connection String**
   - Open the `appsettings.json` file.
   - Update the connection string with your database details.

2. **Verify EF Core Tool Installation**
   - Ensure EF Core tools are installed by running:
     dotnet tool list -g
   - If not installed, you can add them with:
     dotnet tool install --global dotnet-ef

3. **Apply Initial Migration**
   - Open the Package Manager Console or a terminal.
   - Run the command to create the initial migration:

4. **Update the Database**
   - Run the command to apply the migration to the database:

5. **Run the Application**
   - Navigate to the project directory:
     cd UserManagementSystem.WebAPI
    
   - Start the application with:
     dotnet run
     

6. **Access Swagger for API Documentation**
   - Open your web browser.
   - Navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger) to view the API documentation.

## Usage

### API Endpoints

- **GET /api/users/GetUsersList:** Retrieve a list of all users.
- **GET /api/users/GetUserById/{id}:** Retrieve a user by their ID.
- **POST /api/users/CreateUser:** Create a new user.
- **PUT /api/users/UpdateUser/{id}:** Update an existing user.
- **DELETE /api/users/DeleteUser/{id}:** Delete a user by their ID.

