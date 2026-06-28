FinalAssetManagement 🚀

An Asset Management RESTful API built with **ASP.NET Core (.NET 10)**. This project follows **Clean Architecture** and **Separation of Concerns** to ensure high maintainability and scalability.

🏗️ Project Architecture
The project follows **Onion Architecture** principles.

![Architecture Diagram](images/architecture-diagram.png)

✨ Key Features
- 🏗️ **Clean Architecture:** Strict separation of layers.
- 🔁 **Repository & Unit of Work:** Efficient data management patterns.
- 🛡️ **JWT Authentication:** Secure role-based authorization.
- 📦 **Entity Framework Core:** Code-First approach for database management.
- 🎨 **AutoMapper:** Automated object mapping.
- ✅ **FluentValidation:** Robust request validation.
- ⚡ **Standardized API Responses:** Unified wrapper for all endpoints.


🗄️ Database Schema
Detailed design for asset tracking.
![Database Diagram](images/DatabaseDiagram.png)


⚙️ API Usage & Testing
🔑 Authentication Flow
The API enforces secure access.

| Login API Endpoint | Handling Unauthorized Access |
| :---: | :---: |
| ![Login API](images/login-api.png) | ![Unauthorized](images/unauthorized.PNG) |

📊 Asset Management
Example of standard response implementation.
![Get Assets](images/get-assets.png)


🛠️ Getting Started
📋 Prerequisites
- .NET SDK 10
- SQL Server

📥 Installation
1. Clone the repository:
   git clone https://github.com/ensiehyousefi14/FinalAssetManagement.git
   cd FinalAssetManagement

2. Configure Database:
   Open src/API/appsettings.json and update your connection string.

3. Apply Migrations:
   dotnet ef database update --project src/Infrastructure --startup-project src/API

---

🤝 Support & Contribution
Issues: If you find any bugs or have feature requests, please open an Issue.
Contributions: Pull requests are welcome. Please ensure your code follows the existing clean architecture style.

⚖️ License
This project is licensed under the MIT License. Feel free to use and modify it for your needs.

Developed with ❤️ by Ensieh Yousefi
