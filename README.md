# BelanjaYuk – E-Commerce Web Application

A full-stack e-commerce web application built using **ASP.NET Core 8, C#, and SQL Server**. The application uses a separated WebClient and Backend API architecture, where the frontend communicates with the backend through HTTP requests.

## Overview

BelanjaYuk is an e-commerce application developed to simulate a basic online shopping platform. It allows buyers to browse products, manage their shopping cart, and view their order history. Sellers can register their stores, manage products, and access their seller dashboard.

The application separates the frontend and backend to maintain a clear separation of concerns. All business logic and database operations are handled by the Backend API, while the WebClient communicates with the API to retrieve and submit data.

## Features

### Buyer Features
- **User Authentication** – User registration and login.
- **Product Browsing** – Browse available products and view product information.
- **Product Search** – Search for products.
- **Shopping Cart** – Add products to the cart and manage item quantities.
- **Checkout** – Proceed with purchases using available payment methods.
- **Order History** – View previous transactions and purchased products.
- **Product Reviews** – Submit product ratings and reviews.

### Seller Features
- **Seller Registration** – Register a store with its information and details.
- **Seller Dashboard** – Access the seller management dashboard.
- **Product Management** – Add, edit, and manage product information.
- **Product Inventory** – Manage product prices, discounts, and stock quantities.

## Architecture

BelanjaYuk consists of two separate ASP.NET Core projects:

| Component | Description |
|---|---|
| Backend API | Handles business logic, authentication, API endpoints, and database operations. |
| Frontend (WebClient) | Provides the user interface and communicates with the Backend API through HTTP requests. |
| Database | Stores user, seller, product, cart, and transaction data using SQL Server. |

### Application Flow

```text
                 User
                  |
                  v
        Frontend (WebClient)
        ASP.NET Core Razor Pages
                  |
             HTTP Requests
                  |
                  v
           Backend API
          ASP.NET Core 8
                  |
          Entity Framework Core
                  |
                  v
             SQL Server
```

## Tech Stack

| Technology | Purpose |
|---|---|
| C# | Backend programming language |
| ASP.NET Core 8 | Web application framework |
| ASP.NET Core Web API | RESTful API development |
| ASP.NET Core Razor Pages | Frontend development |
| Entity Framework Core | Object-relational mapping (ORM) |
| LINQ | Database querying |
| SQL Server | Relational database management |
| HTML | Web page structure |
| CSS | Styling and layout |
| JavaScript | Client-side functionality |
| Bootstrap | UI components and styling |

## Project Structure

```text
BelanjaYuk_ASPNET8/
│
├── BelanjaYuk_ASPNET8/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Entities/
│   ├── Infrastructure/
│   ├── Migrations/
│   ├── Properties/
│   ├── wwwroot/
│   ├── appsettings.json
│   ├── Program.cs
│   └── BelanjaYuk_ASPNET8.csproj
│
├── BelanjaYuk_ASPNET8_Frontend/
│   ├── Pages/
│   ├── Properties/
│   ├── Services/
│   ├── wwwroot/
│   ├── appsettings.json
│   ├── Program.cs
│   └── BelanjaYuk_ASPNET8_Frontend.csproj
│
├── database/
│   └── SQLQuery_ASPNET.sql
│
├── BelanjaYuk_ASPNET8.sln
├── .gitignore
└── README.md
```

## Getting Started

### Prerequisites

Make sure the following tools are installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the ASP.NET and web development workload
- SQL Server
- SQL Server Management Studio (SSMS)

### 1. Clone the Repository

```bash
git clone https://github.com/Theofit31/belanjayuk-ecommerce.git
```

Navigate to the project directory:

```bash
cd belanjayuk-ecommerce
```

### 2. Restore NuGet Packages

Run the following command from the solution root directory:

```bash
dotnet restore BelanjaYuk_ASPNET8.sln
```

### 3. Configure the Database

The application uses SQL Server with the default database name `BelanjaYukDB`.

The default backend connection string is configured in:

```text
BelanjaYuk_ASPNET8/appsettings.json
```

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BelanjaYukDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Make sure SQL Server LocalDB is installed and available.

To set up the database:

1. Open SQL Server Management Studio.
2. Connect to your SQL Server LocalDB instance.
3. Create a database named `BelanjaYukDB`:

```sql
CREATE DATABASE BelanjaYukDB;
GO
```

4. Open the `database/SQLQuery_ASPNET.sql` file.
5. Execute the SQL script to create the database tables, relationships, and sample data.

**Note:** The SQL script is designed for the `BelanjaYukDB` database. If you use a different SQL Server instance, update the backend connection string accordingly.

### 4. Configure the Backend API

The frontend uses the following API base URL by default:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5283/api/v1/"
  }
}
```

This configuration is located in:

```text
BelanjaYuk_ASPNET8_Frontend/appsettings.json
```

Make sure the Backend API is running on the same address and port. Update the `BaseUrl` if the backend uses a different URL.

### 5. Run the Backend API

Open a terminal in the solution root directory and execute:

```bash
dotnet run --project BelanjaYuk_ASPNET8/BelanjaYuk_ASPNET8.csproj
```

The backend API is configured to use:

```text
http://localhost:5283
```

Keep this terminal running.

### 6. Run the Frontend

Open another terminal in the solution root directory and execute:

```bash
dotnet run --project BelanjaYuk_ASPNET8_Frontend/BelanjaYuk_ASPNET8_Frontend.csproj
```

Open the frontend URL displayed in the terminal.

**Important:** Both the Backend API and Frontend must be running at the same time for the application to work correctly.

## Database

The application uses SQL Server and Entity Framework Core for data persistence.

Main database entities include:

- Users and user passwords
- Seller information
- Product and product categories
- Product images
- Shopping cart
- Buyer transactions
- Transaction details
- Payment methods
- User addresses

The Entity Framework Core database context is located in:

```text
BelanjaYuk_ASPNET8/Infrastructure/AppDbContext.cs
```

Database migrations are available in:

```text
BelanjaYuk_ASPNET8/Migrations/
```

The database setup script and sample data are available in:

```text
database/SQLQuery_ASPNET.sql
```

## Author

**Theo Filus Iglesias Triadi**
