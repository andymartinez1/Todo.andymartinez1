# Todo Minimal API

A simple Todo web app using ASP.NET Minimal API for the backend and Fetch API for asynchronous CRUD operations on the frontend.

## Features

- Minimal API backend with endpoints for Create, Read, Update, Delete (CRUD)
- Frontend uses Fetch API for async data manipulation
- Lightweight and easy to extend

## Getting Started

1. **Clone the repository:**

   ```bash
   git clone https://github.com/andymartinez1/Todo-Minimal-API.git
   ```

2. **Navigate to the project directory:**

   ```bash
   cd TodoApi
   ```

3. **Update the database:**

   ```bash
   dotnet ef database update
   ```

4. **Restore dependencies:**

   ```bash
   dotnet restore
   ```

5. Build and run the app:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7118` (or as configured).

### API Endpoints

| Method | Endpoint            | Description          |
| ------ | ------------------- | -------------------- |
| GET    | /api/todoitems/     | Get all todos        |
| POST   | /api/todoitems/     | Create new todo      |
| PUT    | /api/todoitems/{id} | Update existing todo |
| DELETE | /api/todoitems/{id} | Delete todo          |
