# FinalAssetManagement 🚀

![.NET](https://img.shields.io/badge/.NET-10-blue)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-green)
![License](https://img.shields.io/badge/License-MIT-yellow)

An **Asset Management RESTful API** built with **ASP.NET Core (.NET 10)**.  
This project follows **Clean Architecture (Onion Architecture)** to ensure high maintainability, scalability, and separation of concerns.

---

# ✨ Key Features

- 🏗 **Clean Architecture** — Strict separation between Domain, Application, Infrastructure, and API layers  
- 🔁 **Repository & Unit of Work Pattern** — Structured data access management  
- 🛡 **JWT Authentication** — Secure role‑based authorization  
- 📦 **Entity Framework Core (Code‑First)** — Database schema managed with migrations  
- 🎨 **AutoMapper** — Simplified object mapping between layers  
- ✅ **FluentValidation** — Structured request validation  
- ⚡ **Standardized API Responses** — Unified response structure for all endpoints  

---

# 🏗 Architecture Overview

This project follows **Onion Architecture**, where dependencies always point inward toward the domain layer.

![Architecture Diagram](images/architecture-diagram.png)

📖 Detailed explanation:  
[Architecture Documentation](docs/architecture.md)

---

# 🗄 Database Design

The database schema is designed to support asset tracking, user management, and system configuration.

![Database Diagram](images/DatabaseDiagram.png)

📖 Detailed documentation:  
[Database Documentation](docs/database.md)

---

# ⚙️ API Usage

The API uses **JWT authentication** for secure access and can be tested using **Postman or Swagger**.

## Authentication Example

| Login API | Unauthorized Response |
| :---: | :---: |
| ![Login API](images/login-api.png) | ![Unauthorized](images/unauthorized.PNG) |

## Asset Endpoint Example

![Get Assets](images/get-assets.png)

📖 Full guide:  
[API Usage Documentation](docs/api-usage.md)

---

# 📂 Project Structure
