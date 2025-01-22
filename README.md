# Clean Architecture Country Management API

This project is a **Country Management API** built with **.NET Core** using **Clean Architecture**. It follows best practices of **Clean Architecture**, ensuring a modular, maintainable, and scalable solution. This API is designed for managing country data, with features such as CRUD operations, secure JWT-based authentication, and **multi-language support** for localization.

## Key Features

- **Clean Architecture**: Separation of concerns with layers for API, Application, Core, Infrastructure, and more.
- **CQRS** (Command Query Responsibility Segregation): Clear separation of read (queries) and write (commands) operations.
- **JWT Authentication**: Secure user authentication and authorization via JWT tokens.
- **Generic Repository & UnitOfWork**: Implements a generic repository pattern to handle CRUD operations and the UnitOfWork pattern to manage database transactions.
- **Reflection-based Dependency Injection**: Automatic addition of scoped services and dependencies through reflection for streamlined service management.
- **Middleware**: Custom middleware for logging requests and handling exceptions.
- **Multi-Language Support**: The API is capable of handling multiple languages for localization, allowing users to access responses in their preferred language.

## Technologies Used

- **.NET Core**: The core framework used for building the API.
- **Clean Architecture**: Divides the application into independent layers (API, Application, Core, Infrastructure, etc.) for better maintainability and scalability.
- **Swagger**: Interactive API documentation and UI to test endpoints.
- **FluentValidation**: For data validation in requests.
- **Serilog**: Logging framework to capture detailed logs of events and exceptions.
- **MediatR**: For implementing CQRS and handling commands and queries.
- **Localization (Multi-Language Support)**: Handles multiple languages through resource files for different locales.

## Multi-Language Support

This project supports **multi-language** functionality by using the built-in **.NET Core localization** features. It allows the API to serve responses in multiple languages based on user preferences or request headers.

### How Multi-Language Works:
- **Resource Files**: Language-specific resource files (`.resx`) are created to store translations for different languages.
- **Culture Selection**: The API allows clients to specify their preferred language in the `Accept-Language` header, and the appropriate translations are returned in the response.
- **Supported Languages**: Currently, the application supports **English** and **Arabic**. Additional languages can be added easily by creating corresponding resource files.
