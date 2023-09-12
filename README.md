# WebShop
This project is an ASP.NET MVC Core 6 solution that utilizes Microsoft SQL Server Express Edition for its database. It aims to provide simple webshop solution where you can offer your products and also track orders, editing product category and have basic administration on users. This project utilizes the Code-First approach with Entity Framework for database management. Code-First is a database development approach where the database schema is generated from your C# code and model classes. Entity Framework, a popular Object-Relational Mapping (ORM) framework, is used to facilitate database operations, including creating, reading, updating, and deleting records.The project is useful for small businesses but it also has the great capability of scaling according to business needs.
1. Ensure that you have the following prerequisites installed on your computer:
   - [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
   - [Microsoft SQL Server Express Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

2. Clone this repository to your local machine:
3. git clone: https://github.com/smedic7/WebShop.git
4. Navigate to the project directory
5. Open the solution in your preferred development environment (e.g., Visual Studio, Visual Studio Code)
6. Configure your database connection string in the `appsettings.json` file.
7. ### Database Configuration

To configure the database connection and initialize the database, please follow these steps:

8. Open the `appsettings.json` file in the project.
9. Locate the `ConnectionStrings` section and ensure that the connection string is correctly set up for your database server.

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=your-database;User=your-username;Password=your-password;"
   }

10. Build and run the project
11. Open package manager console
12. enter command "update-database" and press enter
13. after that DB will be created
14. in VSE run project and press login on web browser to enter admin credentials
15. admin user is uid:mirko@mirko.hr and password is mirkomiric
16. on admin dropdown list please choose "Product" and afterwards "Kreiraj novi produkt"
17. now you are able to create new product with product details
## Where to get help with your project

If you encounter any issues or need assistance with this project, you can send email to me:
sasa.dedic@gmail.com




