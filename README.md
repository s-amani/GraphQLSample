# GraphQL API to Create, Update, Delete and List entities from the Database.

## Requirements
* C# 8 .NET Core 3.x or higher
* Publish the code in your Git repository (private repository)
* The API should be exposed via GraphQL
* The entity should have the structure as shown in the section below
* Cover your implementation with tests
* Document how to setup and run the solution
* Use Entity Framework
* Use the HotChocolate library (https://chillicream.com/) v10.5 or higher to build the GraphQL API
* Use SQL Server as database
* Add validations for input types
* Add error handling

## Entity “Customer”
* Id (long auto-generated)
* Email (max 128)
* Name (max 128)
* Code (nullable int)
* Status (Enum: Active, Inactive)
* CreatedAt (Date and Time)
* IsBlocked (boolean)
