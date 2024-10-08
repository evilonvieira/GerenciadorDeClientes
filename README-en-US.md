# Gerenciador De Clientes

Welcome! This project was created with the main purpose of managing customers, being designed to be compatible with integrations between systems using RESTful.

![image](docs/images/sytem.png)

## :eye: Project Overview

This system allows users to:

- **Clients Manage**: CRUD of Clients.
- **Address Manage**: CRUD Client's address.

## :computer: Technologies Used

- **Asp.Net MVC**: A framework for creating structured and scalable web applications.
- **WebApi REST**: Allows you to create lightweight and scalable HTTP services for applications.
- **SqlServer**: Robust relational database management system.
- **Entity Framework**: Speeds up development by reducing repetitive code and complexity.
- **.NET 8**: Unified platform for developing modern web, desktop and cloud applications.

## 📁 Project Structure

Built in DDD to facilitate understanding, maintenance and scalability of the project.

![image](docs/images/project_structure_2.png)

## :computer: Installation

### Environment Tools

- **Visual Studio 2022 - Comunity**: [download](https://visualstudio.microsoft.com/pt-br/vs/community/)
- **SqlServer 16 - Express**: [download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- **.NET 8.0**: [download](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)


## :floppy_disk: Migrations

The project uses migrations, so when downloading the project, configure the connection string in the app.settings file according to your SqlServer installation.

### Connection String

GerenciadorDeClientes.WebApi > app.settings

**Example**
```
Server={instance-sql-server};Database=GerenciadorDeClientes;User Id={user-id-sql-server};Password={user-pass-sql-server};TrustServerCertificate=True;
```

Make the following changes on connection string
- **{instance-sql-server}**: name of your sql server instance
- **{user-id-sql-server}**: user id of your sql server instance
- **{user-pass-sql-server}**: user pass of your sql server instance

### Startup Project

Set project "GerenciadorDeClientes.WebApi" as startup project

![image](docs/images/startup-project.png)

### Command

Open "Package Manager Console" and select "Infra\GerenciadorDeClientes.Infra.Data" project

![image](docs/images/package-manager-console.png)

Run command below

```
Update-Database
```

## :computer: Run Project

### Visual Studio

Open the solution properties in visual studio and configure the “Multiple startup projects” option, as below:

![image](docs/images/solution-properties.png)

### System

At this point, migrations have already created the "Administrador" user, use the credentials below to access the system.

- **email**: admin@admin.com
- **password**: 123456

> [!WARNING]
> Review the webapi url entry in app.settings of project "GerenciadorDeClientes.Web" and make changes if necessary.

![image](docs/images/login4.png)

## :telephone_receiver: Support

If you have any questions about Gerenciador de Clientes, would like to discuss a bug report, or have questions about new integrations, feel free to contact me at.

[linkedin](https://www.linkedin.com/in/evilon-do-nascimento-vieira-0924082a/)

## ⚖️ License

Gerenciador De Clientes is [MIT licensed](./LICENSE).
