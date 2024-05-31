# Introduction
Agri-Energy Connect Platform is a web application that connects farmers and employees in the agricultural energy sector. It allows farmers to add and manage their products, and employees to view and manage these products.
# Setting Up the Development Environment
1.	**Install Visual Studio:** Download and install Visual Studio from the official website. The Community version is free and sufficient for this project.
2.	**Install .NET Core SDK:** Download and install the .NET Core SDK from the .NET download page.
3.	**Clone the Repository:** Clone the project repository to your local machine using Git.
4.	**Open the Project:** Open the Agri-Energy Connect Platform.sln file in Visual Studio.
5.	**Install NuGet Packages:** Right-click on the solution in the Solution Explorer and select "Restore NuGet Packages".
6.  **Update the Connection String:** Open the appsettings.Development.json file and replace the DefaultConnection string in the ConnectionStrings section with your database connection string.
7.	**Set up the Database:** Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console) and run the command Update-Database. This will create the database and apply any existing migrations. 
# Building and Running the Prototype
1.	**Build the Project:** Click on "Build > Build Solution" in the menu, or press Ctrl+Shift+B.
2.	**Run the Project:** Press F5 to start debugging, or Ctrl+F5 to start without debugging.
# System Functionalities and User Roles
The system has two main user roles: Farmers and Employees.
**Farmers can:**
•	Log in to the system.
•	Add their products.
**Employees can:**
•	Log in to the system
• Add new Farmers
•	View all products added by farmers.
# Conclusion
This README provides a basic guide to setting up the development environment, building and running the prototype, and understanding the system functionalities and user roles.
