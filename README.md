# Student Event Management System — ASP.NET Core Web API

**Course:** Web Engineering | **Student:** Minam Khan (ID: 61735) | **Program:** BS(SE) – FEST, Iqra University

---

## Project Overview

A complete backend RESTful API for managing student events, participant registrations, and feedback. Built using ASP.NET Core 9.0 and Entity Framework Core 8.0 with SQL Server.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core 9.0 |
| ORM | Entity Framework Core 8.0 |
| Database | SQL Server (LocalDB for dev) |
| Documentation | Swagger / OpenAPI |
| Language | C# 12 |

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/events` | List all upcoming events |
| GET | `/api/events/{id}` | Get event by ID |
| POST | `/api/events` | Create a new event |
| PUT | `/api/events/{id}` | Update an event |
| DELETE | `/api/events/{id}` | Delete an event |
| GET | `/api/events/search?query=xyz` | Search by name or venue |
| GET | `/api/events/filter?sort=date` | Filter by date or venue |
| POST | `/api/registrations` | Register student for event |
| GET | `/api/registrations/event/{eventId}` | Get registrations for event |
| POST | `/api/feedback` | Submit feedback |
| GET | `/api/feedback/event/{eventId}` | Get feedback for event |

## Business Rules

- Feedback is only allowed **after** the event date.
- Duplicate registrations are rejected with HTTP 409 Conflict.
- Feedback can only be submitted by registered participants.
