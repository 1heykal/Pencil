# 📚 Pencil Content Management System

## Overview

Pencil CMS is a powerful web application for managing content, including blogs, comments, and user interactions. Built with **ASP.NET Core** and **Entity Framework Core**, it offers a modular and scalable solution for content management.

---

## 🚀 Features

- **User Management**: Create and manage user profiles with authentication.
- **Blog Management**: CRUD operations for blogs and posts.
- **Comment System**: Add, update, and delete comments on posts.
- **API Documentation**: Integrated Swagger for easy API testing.

---

## 🏗️ Architecture

The Pencil CMS follows a **layered architecture** that separates concerns and promotes maintainability:

- **Presentation Layer**: Utilizes ASP.NET Core MVC for handling HTTP requests and responses, providing a clean API interface.
- **Application Layer**: Implements business logic and application services using the **CQRS (Command Query Responsibility Segregation)** pattern, allowing for clear separation between commands (writes) and queries (reads).
- **Domain Layer**: Contains domain entities and business rules, ensuring that the core logic is independent of external frameworks.
- **Persistence Layer**: Uses **Entity Framework Core** for data access, enabling easy migrations and database management.
- **Infrastructure Layer**: Handles external service integrations, such as email services and logging.

---

## 🌟 Design Patterns

- **Repository Pattern**: Abstracts data access logic, allowing for easier testing and separation of concerns.
- **Mediator Pattern**: Utilizes MediatR to facilitate communication between components, reducing dependencies and promoting a clean architecture.
- **Dependency Injection**: Built-in support for dependency injection in ASP.NET Core, enhancing testability and modularity.

---

## 🛠️ Getting Started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation Steps

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/1heykal/PencilContentManagement.git
   cd PencilContentManagement
   ```

2. **Restore Packages**:

   ```bash
   dotnet restore
   ```

3. **Update Connection String**:
   Modify `appsettings.json` with your database credentials.

4. **Apply Migrations**:

   ```bash
   dotnet ef database update
   ```

5. **Run the Application**:
   ```bash
   dotnet run
   ```

---

## 🧪 Testing

Run tests with:

```bash
dotnet test
```

---

## 🤝 Contributing

1. **Fork the Repository**
2. **Create a New Branch**: `git checkout -b feature/YourFeature`
3. **Make Changes and Commit**: `git commit -m 'Add some feature'`
4. **Push to the Branch**: `git push origin feature/YourFeature`
5. **Open a Pull Request**

---

## 📄 License

This project is licensed under the MIT License.

---

## 🙏 Acknowledgments

Thanks to the open-source community and the ASP.NET Core team for their contributions!
