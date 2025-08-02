
# Wedding rental web

A web API built with ASP.NET Core to manage a wedding decoration and item rental service.

## How to use

```sh
# Clone the repo

git clone https://github.com/ppkhanhs/wedding-rental-web
cd wedding-rental-web

# Run the project locally
$ dotnet run

```

## Objective

* Built a wedding rental API system using ASP.NET Core.
* Implement Entity Framework Core with database context.
* Scaffold controllers to support CRUD operations.
* Manage different entities like items, orders, customers, etc.


## Overview

The API supports managing wedding rental items and services. Below is an example structure of what the endpoints might include (based on standard ASP.NET patterns):

API | Endpoints | Description | Request body | Response body 
---|---|---|---|---
GET    | `	/api/Items`      | Get all rental items     | None       | List of items
GET    | `/api/Items/{id}` | Get a specific item by ID       | None       | Item
POST   | `/api/Items`      | 	Add a new rental item          | Item object | Created item
PUT    | `	/api/Items/{id}` | Update an existing item | Item object | Updated item
DELETE | `	/api/Items/{id}` | Delete an item          | None       | None 

## The Architecture

This project follows a basic layered architecture:

```sh
Controllers --> Services / Logic --> Data Access (DbContext)  -->  Database

```
* ASP.NET Core Web API
* Entity Framework Core for data access
* SQL Server as the backend database
* RESTful API design
* DTOs and ViewModels (optional, for cleaner data handling)
