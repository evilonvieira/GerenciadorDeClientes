# Gerenciador De Clientes

Bem-vindo! Este projeto foi criado com o objetivo principal de gerenciar clientes, sendo desenhado para ser compatível com integrações entre sistemas utilizando RESTful.

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/sytem.png)

## :eye: Overview do projeto

Este sistema permite aos usuários:

- **Gerenciamento de clientes**: CRUD de clientes.
- **Gerenciamento de endereço**: CRUD de endereços dos clientes.

## :computer: Tecnologias Utilizadas

- **Asp.Net MVC**: Uma estrutura para a criação de aplicações web estruturadas e escaláveis.
- **WebApi REST**: Permite criar serviços HTTP leves e escaláveis ​​para aplicativos.
- **SqlServer**: Sistema robusto de gerenciamento de banco de dados relacional.
- **Entity Framework**: Acelera o desenvolvimento reduzindo código repetitivo e complexidade.
- **.NET 8**: Plataforma unificada para desenvolvimento de aplicativos modernos para web, desktop e nuvem.

## 📁 Estrutura do Projeto

Construído em DDD para facilitar o entendimento, manutenção e escalabilidade do projeto.

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/project_structure_2.png)

## :computer: Instalação

### Ferramentas

- **Visual Studio 2022 - Comunity**: [download](https://visualstudio.microsoft.com/pt-br/vs/community/)
- **SqlServer 16 - Express**: [download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- **.NET 8.0**: [download](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

### Visual Studio

Abra as propriedades da solução no visual studio e configure a opção “Multiple startup projects”, conforme abaixo:

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/solution-properties.png)

## :floppy_disk: Migrations

O projeto utiliza migration, portanto ao baixar o projeto configure a string de conexão no arquivo app.settings de acordo com a instalação do seu SqlServer.

### Connection String

GerenciadorDeClientes.WebApi > app.settings

**Example**
```
Server={instance-sql-server};Database=GerenciadorDeClientes;User Id={user-id-sql-server};Password={user-pass-sql-server};TrustServerCertificate=True;
```

Faça as seguintes alterações na string de conexão
- **{instance-sql-server}**: nome da sua instância do SQL Server
- **{user-id-sql-server}**: ID do usuário da sua instância do SQL Server
- **{user-pass-sql-server}**: Senha de usuário da sua instância do SQL Server

### Startup Project

Defina o projeto "GerenciadorDeClientes.WebApi" como projeto inicial

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/startup-project.png)

### Command

Abra "Package Manager Console" e selecione o projeto "Infra\GerenciadorDeClientes.Infra.Data"

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/package-manager-console.png)

Execute o comando abaixo

```
Update-Database
```

## :computer: Executando o Projeto

Neste momento as migrações já criaram o usuário "Administrador", utilize as credenciais abaixo para acessar o sistema.

- **email**: admin@admin.com
- **password**: 123456

> [!WARNING]
> Revise a propriedade da url da webapi no app.settings do projeto "GerenciadorDeClientes.Web" e faça alterações se necessário.

![image](https://github.com/evilonvieira/GerenciadorDeClientes/blob/feature/estruturacao/docs/images/login4.png)

## :telephone_receiver: Suporte

Se você tiver alguma dúvida sobre o Gerenciador de Clientes, quiser discutir um relatório de bug ou tiver dúvidas sobre novas integrações, sinta-se à vontade para entrar em contato comigo em.

[linkedin](https://www.linkedin.com/in/evilon-do-nascimento-vieira-0924082a/)

## ⚖️ Licença

Gerenciador De Clientes é [MIT licensed](./LICENSE).