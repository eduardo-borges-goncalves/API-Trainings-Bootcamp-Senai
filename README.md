# API Trainings

This project is an API for a trainings management system, developed during a bootcamp at Senai using C# as the main technology.

## Technologies Used

- C#
- ASP.NET Core 3.1
- Entity Framework Core
- AutoMapper
- JWT Authentication
- Swagger

## How to run the project

1. Clone the repository
2. Open the solution in Visual Studio
3. Open the Package Manager Console and run the following command to create the database:

```bash
Update-Database
```

4. Run the project in Visual Studio
5. Open your browser and go to [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html) to access the Swagger documentation

## Endpoints

The API has the following endpoints:

### GET /api/trainings

Returns a list of all trainings in the database.

### GET /api/trainings/{id}

Returns a training by its id.

### POST /api/trainings

Creates a new training.

### PUT /api/trainings/{id}

Updates a training by its id.

### DELETE /api/trainings/{id}

Deletes a training by its id.

### POST /api/users/register

Registers a new user.

### POST /api/users/login

Logs in a user and returns a JWT token.

## Database

The database used in this project is a Microsoft SQL Server database, created using Entity Framework Core. The database has two tables, called "Trainings" and "Users", which contains the following fields:

### Trainings

- Id (int, primary key)
- Name (nvarchar(50), not null)
- Description (nvarchar(500), not null)
- Date (datetime2, not null)
- Duration (int, not null)

### Users

- Id (int, primary key)
- Name (nvarchar(50), not null)
- Email (nvarchar(50), not null)
- Password (nvarchar(100), not null)

## Contributors

- [Eduardo Borges Gonçalves](https://github.com/eduardo-borges-goncalves) - Developer

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

# API Trainings

Este projeto é uma API para um sistema de gerenciamento de treinamentos, desenvolvido durante um bootcamp no Senai usando C# como principal tecnologia.

## Tecnologias Usadas

- C#
- ASP.NET Core 3.1
- Entity Framework Core
- AutoMapper
- Autenticação JWT
- Swagger

## Como executar o projeto

1. Clone o repositório
2. Abra a solução no Visual Studio
3. Abra o Package Manager Console e execute o seguinte comando para criar o banco de dados:

```bash
Update-Database
```

4. Execute o projeto no Visual Studio
5. Abra o seu navegador e vá para [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html) para acessar a documentação Swagger

## Endpoints

A API possui os seguintes endpoints:

### GET /api/trainings

Retorna uma lista de todos os treinamentos no banco de dados.

### GET /api/trainings/{id}

Retorna um treinamento pelo seu id.

### POST /api/trainings

Cria um novo treinamento.

### PUT /api/trainings/{id}

Atualiza um treinamento pelo seu id.

### DELETE /api/trainings/{id}

Exclui um treinamento pelo seu id.

### POST /api/users/register

Registra um novo usuário.

### POST /

api/users/login

Efetua o login de um usuário e retorna um token JWT.

## Banco de dados

O banco de dados utilizado neste projeto é um banco de dados Microsoft SQL Server, criado usando o Entity Framework Core. O banco de dados possui duas tabelas, chamadas "Trainings" e "Users", que contêm os seguintes campos:

### Trainings

- Id (int, chave primária)
- Name (nvarchar(50), not null)
- Description (nvarchar(500), not null)
- Date (datetime2, not null)
- Duration (int, not null)

### Users

- Id (int, chave primária)
- Name (nvarchar(50), not null)
- Email (nvarchar(50), not null)
- Password (nvarchar(100), not null)

## Contribuidores

- [Eduardo Borges Gonçalves](https://github.com/eduardo-borges-goncalves) - Desenvolvedor

## Licença

Este projeto está licenciado sob a Licença MIT - consulte o arquivo [LICENSE](LICENSE) para obter detalhes.
