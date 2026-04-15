# Gymnasiearbete Frontend

## Overview

This project is a school thesis application built to explore a modern travel search experience for SkaneTrafiken-related data. It combines a Svelte frontend with an ASP.NET Core backend API and a MySQL database connection.

The purpose of the application is to let a user search for public transport stops and present route information in a clear interface. In its current state, the stop search is connected to the backend and database, while the route cards shown in the interface are still based on placeholder data for presentation and layout purposes.

## Repository

GitHub repository:

`https://github.com/Agno123/Gymnasiearbete-Frontend`

## Tech Stack

- Frontend: SvelteKit + Vite + Svelte 5
- Backend: ASP.NET Core Web API on .NET 9
- Database access: MySqlConnector
- Database: MySQL

## Project Structure

```text
.
|-- src/
|   |-- routes/
|   |   |-- +page.svelte               # Main page
|   |-- lib/
|       |-- components/                # UI components
|       |-- stores/                    # Shared Svelte state
|
|-- SkaneTrafik.Api/
|   |-- controllers/
|   |   |-- stopsController.cs         # Stop search endpoint
|   |   |-- tripsController.cs         # Trip-related endpoint
|   |-- Program.cs                     # API startup and CORS configuration
|   |-- appsettings.json               # Connection string configuration
|
|-- package.json                       # Frontend scripts and dependencies
|-- GySvelteTest.sln                   # .NET solution file
```

## How It Works

### Frontend

The frontend is built with SvelteKit and provides the main user interface. The user enters a departure stop and a destination stop in the search form.

As the user types, the frontend sends requests to the backend API:

- `GET http://localhost:5113/api/stops?query=...`

The returned stop suggestions are stored in Svelte state and used in the UI. The route result section currently displays example journey cards, using the selected stop names together with mock departure, arrival, and delay values.

### Backend

The backend is an ASP.NET Core API located in the `SkaneTrafik.Api` folder. It exposes endpoints that query a MySQL database.

Current API endpoints:

- `GET /api/stops`
- `GET /api/trips`

The stop search endpoint:

- accepts a `query` parameter,
- searches the `Stops` table,
- filters on a fixed date,
- prioritizes exact and prefix matches,
- returns stop ID, stop name, date, and platform code.

The API also enables CORS for the Svelte development server, allowing requests from:

- `http://localhost:5173`
- `http://localhost`

### Database

The backend expects a MySQL connection string under the key:

`ConnectionStrings:SkaneTrafikDb`

This means the project requires access to a MySQL database containing a `Stops` table with the fields used by the controllers.

## Prerequisites

Before running the project locally, make sure the following are installed:

- Node.js and npm
- .NET 9 SDK
- MySQL Server
- Git

## Installation and Local Setup

### 1. Clone the Repository

```bash
git clone https://github.com/Agno123/Gymnasiearbete-Frontend.git
cd Gymnasiearbete-Frontend
```

### 2. Install Frontend Dependencies

```bash
npm install
```

### 3. Configure the Backend Connection String

Open:

`SkaneTrafik.Api/appsettings.json`

Add your MySQL connection string:

```json
{
  "ConnectionStrings": {
    "SkaneTrafikDb": "Server=localhost;Port=3306;Database=YOUR_DATABASE_NAME;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD;"
  }
}
```

Replace:

- `YOUR_DATABASE_NAME`
- `YOUR_USERNAME`
- `YOUR_PASSWORD`

with your own local MySQL credentials and database name.

### 4. Ensure the Database Schema Exists

The backend expects a table named `Stops` containing at least the following columns:

- `stopId`
- `stopName`
- `date`
- `StopPlatformCode`

If this schema or data is missing, the API will not be able to return search results.

### 5. Run the Backend API

From the repository root:

```bash
dotnet run --project SkaneTrafik.Api
```

By default, the backend runs on:

- `http://localhost:5113`
- `https://localhost:7053`

### 6. Run the Frontend

In a separate terminal:

```bash
npm run dev
```

The frontend development server normally runs on:

- `http://localhost:5173`

## Development Workflow

To work on the project locally:

1. Start the ASP.NET Core backend.
2. Start the Svelte frontend.
3. Open `http://localhost:5173` in your browser.
4. Type into the search fields to test stop lookups against your local API and database.

## Current Status

This project is functional as a full-stack prototype, but it is not yet a complete journey planner.

Implemented:

- Svelte-based search interface
- ASP.NET Core API
- MySQL-backed stop lookup
- Cross-origin communication between frontend and backend

Not yet fully implemented:

- Real trip calculation logic
- Fully dynamic journey results
- Production deployment configuration
- Automated tests

## Available Scripts

Frontend:

```bash
npm run dev
npm run build
npm run preview
```

Backend:

```bash
dotnet run --project SkaneTrafik.Api
```

## Notes for Other Developers

- The frontend depends on the backend being available on `http://localhost:5113`.
- The backend depends on a valid MySQL connection string in `SkaneTrafik.Api/appsettings.json`.
- The current stop search uses a fixed date in the controller logic, so results depend on matching data being available for that date in the database.

## License

No license has been specified in this repository at the time of writing. If the project is intended for public reuse, adding a license file is recommended.
