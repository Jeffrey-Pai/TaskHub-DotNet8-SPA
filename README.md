# TaskHub-DotNet8-SPA

A fullstack task management SPA built with .NET 8 (ASP.NET Core Web API) on the backend and Vue 3 + TypeScript + Vite + TailwindCSS on the frontend.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18 or above recommended)
- npm (comes with Node.js)

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/Jeffrey-Pai/TaskHub-DotNet8-SPA.git
cd TaskHub-DotNet8-SPA
```

### 2. Run the Backend (.NET 8 API)

```bash
cd DotNet-FullStack-Kickstart/backend
dotnet run
```

The API will start on `http://localhost:5214` and `https://localhost:7152` by default (or the port shown in your terminal).  
Swagger UI is available at `https://localhost:7152/swagger` in development mode.

### 3. Run the Frontend (Vue 3 + Vite)

Open a **new terminal**, then:

```bash
cd DotNet-FullStack-Kickstart/frontend
npm install
npm run dev
```

The frontend dev server will start at `http://localhost:5173`.  
Open that URL in your browser to use the app.

---

## Available Scripts

### Backend

| Command | Description |
|---------|-------------|
| `dotnet run` | Start the development server |
| `dotnet build` | Build the project |

### Frontend

| Command | Description |
|---------|-------------|
| `npm run dev` | Start the Vite dev server |
| `npm run build` | Build for production |
| `npm run preview` | Preview the production build locally |

---

## Notes

- The backend now uses **Microsoft SQL Server** via the `DefaultConnection` connection string in `DotNet-FullStack-Kickstart/backend/appsettings*.json`.
- If your SQL Server instance is different, update `ConnectionStrings:DefaultConnection` before running the backend.
- You can override the frontend API target with `VITE_API_BASE_URL` (default is `http://localhost:5214/api/todo`).
- CORS is configured to allow requests from `http://localhost:5173` (the Vite dev server).