# ProductCatalog_API Demo

# Description
This is a small solution to manage product catalog using API Core as Restful API for Backend with .net core and swagger , 
.net core mvc and razor pages , JQuery Ajax to frontend , EF Core (Code First) for ORM
with Clean Archictecutre for all the solution

# Table of Contents
This solution contain 7 layers

1 - CleanArch.Models : this layer contain the product entity that is represent the product table in database
2 - CleanArch.Context : this layer contain the DbContext class that has all DbSets<T> of All Project 
and have the configuration for the entityframework core and dbContext to make the migration 
3 - CleanArch.Repository : this layer contain the base implementation for generic repository with it's interface
4 - CleanArch.Service : this layer contain the actual logic of the product entity that do all operation 
by define the required service interface and implementation for the prodcut operation
5 - CleanArch.API : this is a Restful api layer that responsible for consumeing the product entity with the installation for swagger for API Documentation
6 - CleanArch.Web : this is the UI layer (.NET Core MVC) for working with product it contain one controller (HomeController) that have all action methods to work with Product
7 - CleanArch.Common : this layer have common Dto's for communicate with API and UI project and contain helper methods and constants that is used accross all solution

# Installation
To install solution make sure you have 
1 - .NET Core installed in your machine
2 - clone the repository
3 - open package manager console and in default project dropdown select CleanArc.Context and run command update-database to add database in your sql server
4 - make the solution run multiple projects and select CleanArch.API and CleanArch.Web to run both project and this is final step

# Usage
the applaction contain one Home Page accoring to home controller and list all products and different buttons 
to make the operation required for working with products adding , editing , removing , searching and export to excel sheet

# Refrences
Clean Arch :
1 - https://dev.to/joxiah/clean-architecture-in-asp-net-core-47o2
 
Swagger Implementation : 
1 - https://www.infoworld.com/article/3400084/how-to-use-swagger-in-aspnet-core.html#:~:text=Install%20Swagger%20middleware%20in%20ASP.Net%20Core&text=To%20do%20this%2C%20select%20the,AspNetCore%20and%20install%20it.
2 - https://www.c-sharpcorner.com/article/swagger-in-dotnet-core/ 

3 - Consume API's :
1 - https://www.yogihosting.com/aspnet-core-consume-api/#delete
