# 4Create Tech Assessment v3.1

- [4Create Tech Assessment v3.1](#4create-tech-assessment-v31)
  - [Notice about projects](#notice-about-projects)
    - [Possible improvements](#possible-improvements)
  - [How to run this project](#how-to-run-this-project)
  - [Api documentation](#api-documentation)
    - [Authentication](#authentication)
      - [POST /api/authentication/user/authorize](#post-apiauthenticationuserauthorize)
        - [Description](#description)
        - [Available parameters](#available-parameters)
        - [Usage example](#usage-example)
    - [Company](#company)
      - [POST /api/companies](#post-apicompanies)
        - [Description](#description-1)
        - [Available parameters](#available-parameters-1)
        - [Usage example](#usage-example-1)
    - [Employee](#employee)
      - [POST /api/employees](#post-apiemployees)
        - [Description](#description-2)
        - [Available parameters](#available-parameters-2)
        - [Usage example](#usage-example-2)

## Notice about projects

### Possible improvements

- Separate Employee and Company for better context bounding (delete possibility to create Employee via Company creation)
- Add event to rollback users if company didn't created
- Use Outbox pattern instead of SystemLog creating in repository method
- Add versioning in API to allow for future changes to the contract and a smooth transition of services to the new contract.
- Using SSO authentication
- Make RFC7807 compliant realization of exception views

## How to run this project

- Clone this project and open it in terminal
- Open `/devTools` directory and run MySQL docker container with next command: `docker compose up -d`
- Open `/src/4Create` directory and run migration with next command: `"dotnet ef database update --project 4Create.Infrastructure/4Create.Infrastructure.csproj --startup-project 4Create.Api/4Create.Api.csproj --context _4Create.Infrastructure.MySqlDbContext --configuration Release`
- Open `/src/4Create/4Create.Api` directory and start project with `dotnet run` command.
- Open Swagger page on `https://localhost:7058/swagger/index.html`

## Api documentation

### Authentication

#### POST /api/authentication/user/authorize

##### Description

You can use this endpoint to get access token to access to other endpoints.

**Default credentials**:
- **username: root**
- **password: root**

##### Available parameters

| Name       | Type     | Description |
| ---------- | -------- | ----------- |
| *username* | `string` | Username    |
| *password* | `string` | Password    |

##### Usage example

Request:

```
POST /api/authentication/user/authorize
{
    "username": "username",
    "password": "password"
}
```

Response:

```
{
  "accessToken": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjJlMDlhYjI0LTI2MmYtNGYxYS1hMDQyLTMwY2M4OGQ3MWQ4MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJSb290IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2OTQ0NDQ2OTIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNTgvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA1OC8ifQ.6SqevxVFlFyRc5FgBrpSYpFUVVANky3JUbDrGfJIVdY7AVxRvycpA0OFLQNY_BOFCv6FW8VKQ8oB_k12Y239Mg"
}
```

### Company

#### POST /api/companies

##### Description

Allows you create company. Returns Id of new company

##### Available parameters

| Name               | Type               | Description                                            |
| ------------------ | ------------------ | ------------------------------------------------------ |
| *name*             | `string`           | Company name (unique)                                  |
| *employees*        | `array of objects` | List of employee of company                            |
| *employees[].email | `string`           | Email of employee (required if employee doesn't exist) |
| *employees[].title | `enum`             | Title of employee (required if employee doesn't exist) |
| *employees[].id    | `guid`             | Id of employee (required if employee exist)            |

Available titles:

| Name        | Value |
| ----------- | ----- |
| *Developer* | `1`   |
| *Manager*   | `2`   |
| *Tester*    | `3`   |

##### Usage example

Request:

```
GET /api/companies
{
  "name": "Company LTD",
  "employees": [
    {
      "email": "employee1@companyltd.com",
      "title": 1
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c123f66afa6"
    }
  ]
}
```

Response:
```
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### Employee

#### POST /api/employees

##### Description

Allows you to create new employee. Returns Id of new employee

##### Available parameters

| Name         | Type             | Description                                 |
| ------------ | ---------------- | ------------------------------------------- |
| *email*      | `string`         | Employee email (unique)                     |
| *title*      | `enum`           | Title of employee                           |
| *companyIds* | `array of guids` | Array of company ids employee is working on |

##### Usage example

Request:

```
POST /api/employees
{
  "email": "employee1@companyltd.com",
  "title": 1,
  "companyIds": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ]
}
```

Response:

```
{
  "id" : "9a7de28f-1d9c-4795-99e1-c6764a656244"
}
```