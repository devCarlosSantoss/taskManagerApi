# Task Manager API

API REST desenvolvida em ASP.NET Core + Entity Framework Core + PostgreSQL para gerenciamento de tarefas com autenticação JWT.

## Tecnologias utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* Swagger / OpenAPI
* Docker Compose
* BCrypt.Net

---

## Funcionalidades

* Cadastro de usuário
* Login com geração de token JWT
* CRUD completo de tarefas
* Proteção de rotas com `[Authorize]`
* Swagger com botão `Authorize`
* Banco PostgreSQL via Docker

---

## Como rodar o projeto

## 1. Clonar o projeto

```bash
git clone https://github.com/devCarlosSantoss/taskManagerApi.git
cd taskManagerApi
```

---

## 2. Subir o banco PostgreSQL com Docker

```bash
docker compose up -d
```

Isso criará um container PostgreSQL com:

* Host: localhost
* Port: 5433
* Database: taskdb
* Username: postgres
* Password: admin

---

## 3. Configurar JWT Secrets

Inicialize o User Secrets:

```bash
dotnet user-secrets init
```

Depois configure:

```bash
dotnet user-secrets set "Jwt:SecretKey" 'SuaChaveSuperSecretaAqui123456'
dotnet user-secrets set "Jwt:Issuer" "taskmanager"
dotnet user-secrets set "Jwt:Audience" "taskmanager"
```

---

## 4. Rodar as migrations

```bash
dotnet ef database update
```

---

## 5. Executar a aplicação

```bash
dotnet run
```

Swagger estará disponível em:

```text
http://localhost:5289/swagger
```

---

# Autenticação JWT

Após fazer login, copie o token retornado e clique em:

## 🔓 Authorize

no Swagger.

Cole apenas o token JWT.

---

# Endpoints

---

# Auth

Base route:

```text
/api/Auth
```

---

## POST /api/Auth/register

Cria um novo usuário.

### Request

```json
{
  "username": "usuarioteste",
  "password": "123456",
  "fullName": "Usuario Teste"
}
```

### Response

```json
"Usuário criado com sucesso"
```

---

## POST /api/Auth/login

Realiza login e retorna o token JWT.

### Request

```json
{
  "username": "usuarioteste",
  "password": "123456"
}
```

### Response

```json
{
  "token": "jwt_token_aqui",
  "user": {
    "id": 1,
    "username": "usuarioteste",
    "fullName": "Usuario Teste"
  }
}
```

---

# Tasks

Base route:

```text
/api/Task
```

Todas as rotas abaixo exigem autenticação JWT.

---

## GET /api/Task

Lista todas as tarefas do usuário autenticado.

### Response

```json
[
  {
    "code": 1,
    "description": "Tarefa teste",
    "status": 80,
    "createAt": "2026-04-28T12:45:53Z",
    "userId": 1
  }
]
```

---

## GET /api/Task/{id}

Busca uma tarefa específica.

### Response

```json
{
  "code": 1,
  "description": "Tarefa teste",
  "status": 80,
  "createAt": "2026-04-28T12:45:53Z",
  "userId": 1
}
```

---

## POST /api/Task

Cria uma nova tarefa.

### Request

```json
{
  "description": "Nova tarefa"
}
```

### Response

```json
{
  "code": 2,
  "description": "Nova tarefa",
  "status": 80,
  "createAt": "2026-04-28T12:45:53Z",
  "userId": 1
}
```

---

## PUT /api/Task/{id}

Atualiza uma tarefa existente.

### Request

```json
{
  "description": "Tarefa atualizada",
  "status": 67
}
```

### Response

```text
204 No Content
```

---

## DELETE /api/Task/{id}

Remove uma tarefa.

### Response

```text
204 No Content
```

---

# Observação sobre status

Atualmente o enum está salvo como ASCII:

* Pending = 80 (`P`)
* Completed = 67 (`C`)

Por isso o retorno aparece como número.

---

# Melhorias futuras

* DTOs de resposta
* Refresh Token
* Paginação
* Filtros de tarefas
* Logs estruturados
* Testes automatizados
* Deploy em nuvem

---

# Autor

Projeto desenvolvido para fins de estudo e prática com ASP.NET Core + JWT + PostgreSQL.
