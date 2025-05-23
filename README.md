# Ecom Client

A frontend project for an e-commerce store built with **Angular 19**. This project is part of a full-stack solution with a backend developed in **ASP.NET Core**.

## 🛠️ Technologies Used

- Angular 19
- TypeScript
- RxJS
- Reactive Forms
- Angular Router
- Angular HTTP Client
- JWT Authentication

## 📁 Folder Structure

```
src/
 └── app/
      ├── core/
      │    ├── interceptors/        # JWT interceptor
      │    ├── services/            # Shared services like AuthService
      │    └── guards/              # Route guards like AuthGuard
      ├── pages/
      │    ├── login/               # Login page components
      │    ├── home/                # Home page (WIP)
      │    └── products/            # Products (WIP)
      ├── shared/                   # Shared components, directives, pipes
      ├── app.module.ts
      ├── app.component.ts
      └── app-routing.module.ts     # Application routing module
```

## 🚀 Getting Started

### Prerequisites

- Node.js v18+
- Angular CLI (v16+)

### Installation

```bash
# Clone the repository
git clone https://github.com/your-username/ecom-client.git
cd ecom-client

# Checkout the Angular branch
git checkout angular

# Install dependencies
npm install
```

### Run the Project

```bash
ng serve
```

### Build for Production

```bash
ng build
```

## 🔐 Authentication

- Uses JWT (JSON Web Tokens) for authentication.
- The JWT token is stored locally and attached to requests via an HTTP interceptor.

## 🌐 Environment Configuration

API base URLs and other environment-specific settings can be configured in:

- `src/environments/environment.ts`

## 📌 Notes

- This project is designed to work with an ASP.NET Core backend.
- Ensure the API is running and the base URL is correctly set in your environment files.

## 👤 Author

Developed by Fatma Abdelhameed 

