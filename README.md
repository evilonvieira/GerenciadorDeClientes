# Gerenciador De Clientes

Welcome! This project was created with the main purpose of managing customers, being designed to be compatible with integrations between systems using RESTful.

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/sytem.png)

## Project Overview

This system allows users to:

- **Clients Manage**: CRUD of Clients.
- **Address Manage**: CRUD Client's address.

## Technologies Used

- **Asp.Net MVC**: A framework for creating structured and scalable web applications..
- **WebApi REST**: Allows you to create lightweight and scalable HTTP services for applications..
- **SqlServer**: Robust relational database management system.
- **Entity Framework**: Speeds up development by reducing repetitive code and complexity.
- **Dapper**: Delivers superior performance and simplicity in straightforward and efficient SQL queries.
- **.NET 8**: Unified platform for developing modern web, desktop and cloud applications.

## 📁 Project Structure

Built in DDD to facilitate understanding, maintenance and scalability of the project.

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/project_structure_2.png)

## :computer: Installation

### Environment Tools

- **Visual Studio 2022 - Comunity**: [download](https://visualstudio.microsoft.com/pt-br/vs/community/)
- **SqlServer 16 - Express**: [download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### Migrations

The project uses migrations, so when downloading the project, configure the connection string in the app.settings file according to your SqlServer installation.

#### Connection String

#### Path ####: GerenciadorDeClientes.WebApi > app.settings

**Example**
```
Server={instance-sql-server};Database=GerenciadorDeClientes;User Id={user-id-sql-server};Password={user-pass-sql-server};TrustServerCertificate=True;
```

<div style="text-align: center;">

### Home page with all the blogs

![Blog Screenshot 5](preview/5.png)

### View detailed blog posts after navigating inside

![Blog Screenshot 2](preview/2.png)

### About Page

![Blog Screenshot 1](preview/1.png)

### Create new blog page

![Blog Screenshot 3](preview/3.png)

### This page shows a 404 error page when a user navigates to a non-existent route

![Blog Screenshot 4](preview/4.png)

</div>