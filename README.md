# MyProject – .NET 8 Web API (Dapper + 3-Layer Architecture)

## 📌 Overview

This project is a production-ready .NET 8 Web API starter template built using a clean 3-layer architecture.
It is designed for enterprise applications that rely on:

* Dapper (high-performance data access)
* Stored Procedures
* JWT Authentication
* Role-based Authorization
* Unit of Work + Generic Repository
* Service (Business) Layer
* Audit Tracking
* Global Exception Handling
* Serilog Logging
* Model Validation
* CORS support
* Swagger documentation

The goal is to keep controllers thin, move business logic to Core services, and keep database operations inside Infrastructure.

---

# 🏗️ Architecture

This solution follows a layered MVC-style structure:

```
API Layer        → Controllers + Middleware
Core Layer       → Business Logic + DTOs + Interfaces + Security
Infrastructure   → Dapper + SQL + Repositories + UnitOfWork
```

Request flow:

```
Client
  ↓
Controller (API)
  ↓
Service (Core)
  ↓
UnitOfWork / Repository (Infrastructure)
  ↓
Stored Procedure
  ↓
SQL Server
```

---

# 📁 Folder Structure

```
MyProject.sln
│
├── MyProject.API
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   └── UsersController.cs
│   │
│   ├── Middlewares
│   │   ├── ExceptionMiddleware.cs
│   │   └── AuditMiddleware.cs
│   │
│   ├── Program.cs
│   └── appsettings.json
│
├── MyProject.Core
│   ├── DTOs
│   │   ├── LoginDto.cs
│   │   ├── CreateUserDto.cs
│   │   └── UpdateUserDto.cs
│   │
│   ├── Entities
│   │   ├── BaseEntity.cs
│   │   ├── User.cs
│   │   └── RefreshToken.cs
│   │
│   ├── Enums
│   │   └── UserRole.cs
│   │
│   ├── Interfaces
│   │   ├── IUserService.cs
│   │   ├── IAuthService.cs
│   │   ├── IUnitOfWork.cs
│   │   └── IGenericRepository.cs
│   │
│   ├── Services
│   │   ├── UserService.cs
│   │   └── AuthService.cs
│   │
│   ├── Security
│   │   ├── JwtTokenGenerator.cs
│   │   ├── PasswordHasher.cs
│   │   └── RefreshTokenService.cs
│   │
│   └── Shared
│       ├── ApiResponse.cs
│       ├── PagedRequest.cs
│       └── PagedResult.cs
│
└── MyProject.Infrastructure
    ├── Data
    │   ├── IDbConnectionFactory.cs
    │   └── SqlConnectionFactory.cs
    │
    ├── Repositories
    │   └── GenericRepository.cs
    │
    └── UnitOfWork
        └── UnitOfWork.cs
```

---

# 🧰 Technologies Used

### Backend

* .NET 8 Web API
* C#
* ASP.NET Core

### Data Access

* Dapper
* SQL Server
* Stored Procedures
* Unit Of Work Pattern
* Generic Repository

### Security

* JWT Authentication
* Role-based Authorization
* BCrypt Password Hashing
* Refresh Token Flow
* Audit Tracking (CreatedByUserId, UpdatedByUserId)

### API Features

* Swagger (OpenAPI)
* Model Validation (DataAnnotations)
* CORS Support
* Global Exception Handling

### Logging

* Serilog
* File logging (daily rolling logs)

---

# 🔐 Security Features

* JWT token validation (Issuer, Audience, Lifetime)
* Role-based access control
* Password hashing using BCrypt
* Audit middleware capturing UserId
* Short-lived access tokens

---

# 📊 Logging

Serilog logs:

* Exceptions
* Login attempts
* API activity
* Service events

Logs stored in:

```
/logs/log-YYYY-MM-DD.txt
```

---

# 🧾 Audit System

The system automatically captures:

* CreatedOn
* CreatedByUserId
* UpdatedOn
* UpdatedByUserId

UserId is extracted from JWT claims via AuditMiddleware.

---

# 🧩 Core Design Principles

* Controllers contain NO business logic
* Services contain business rules
* Infrastructure handles DB access only
* DTOs protect the domain model
* UnitOfWork manages transactions
* Stored Procedures handle SQL logic

---

# 🔑 Authentication Flow

1. User logs in
2. Password verified via BCrypt
3. JWT token generated
4. Refresh token saved in DB
5. Token used for secured endpoints

---

# 📡 Available Endpoints (Sample)

### Auth

```
POST /api/auth/login
POST /api/auth/refresh
```

### Users (Admin only)

```
POST   /api/users       → Create
GET    /api/users       → Get all
PUT    /api/users       → Update
DELETE /api/users/{id}  → Delete
```

---

# 🌍 CORS

Configured to allow frontend apps (React/Angular) to call the API.

Development mode allows all origins.
Production should restrict to specific domains.

---

# 🧪 Swagger

Swagger is enabled as the default landing page.

```
https://localhost:xxxx/swagger
```

Use **Authorize** button to paste JWT:

```
Bearer <token>
```

---

# 🗄️ Database

Main tables:

* Users
* RefreshTokens

Key Stored Procedures:

* sp_User_Create
* sp_User_Update
* sp_User_Delete
* sp_User_GetAll
* sp_User_GetByUsername
* sp_SaveRefreshToken
* sp_GetRefreshToken
* sp_RevokeRefreshToken

---

# 🧱 Design Decisions

Why Dapper?

* Faster than EF Core
* Better for stored procedure-heavy systems
* Full SQL control

Why 3-layer architecture?

* Clean separation
* Easier testing
* Enterprise maintainability
* Scalable

Why Service layer?

* Keeps controllers thin
* Centralizes business rules

---

# 🚀 Future Improvements (Optional)

* Permission-based authorization
* Account lockout after failed attempts
* Email verification
* Password reset flow
* Soft delete across all entities
* Multi-tenant support
* API versioning
* Rate limiting

---

# 👨‍💻 How to Run

1. Restore NuGet packages
2. Update connection string in `appsettings.json`
3. Run SQL scripts to create DB + SPs
4. Start API
5. Open Swagger
6. Login → Use token

---

# 📌 Notes

This template is designed as a reusable enterprise starter for new .NET API projects.
It provides a secure, scalable base with strong architectural separation.

---
