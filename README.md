# Gerenciador De Clientes

Welcome! This project was created with the main purpose of managing customers, being designed to be compatible with integrations between systems using RESTful.

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

## 📁 Estrutura do projeto
- #### Apresentation  

  ⭐ Controller: Controladores da API, responsáveis por receber requisições e enviar respostas.

- #### Application

  ⭐ DTOS: Data Transfer Objects usados para passar dados entre camadas.
  
  ⭐ Interfaces: Contratos para os serviços.
         
  ⭐ Mappings: Mapeamento das classes DTOS & Entities.
   
  ⭐ Services: Contém a lógica de negócios de alto nível e chama métodos do repositório.

- #### Domain
   ⭐ Entities: Entidades do domínio.
     
   ⭐ Enums: Enumerações usadas nas entidades e/ou regras de negócio.

   ⭐ Interfaces: Contratos para os repositórios.

   ⭐ Interfaces:

- #### Infrastructure
    ⭐ Repositories: Implementações dos repositórios definidos na camada de Domínio.


## Preview

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