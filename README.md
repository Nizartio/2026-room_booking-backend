# PinjamIn Backend

Backend API for the Room Booking System. Built with ASP.NET Core and Entity Framework Core using SQLite.

## Tech Stack
- ASP.NET Core 8.0
- Entity Framework Core
- SQLite
- Swagger UI

## Requirements
- .NET 8.0 SDK

## Installation
```bash
cd room_booking/backend
dotnet restore
```

## Database Setup
Apply migrations:
```bash
dotnet ef database update
```

Seed data (optional, runs on app start):
```bash
dotnet run
```

The API will start at `http://localhost:5242`.

## Configuration
Edit `appsettings.json` or `appsettings.Development.json` if needed:
- Connection string for SQLite
- Logging levels

## Key Endpoints
Base URL: `http://localhost:5242/api`

### Booking Groups
- `GET /room-bookings/groups/customer/{customerId}`
- `GET /room-bookings/groups/admin?page=1&pageSize=10&status=&search=`
- `POST /room-bookings/groups/bulk-submit`

### Bookings
- `PUT /room-bookings/{id}/approve`
- `PUT /room-bookings/{id}/reject`
- `PUT /room-bookings/{id}/resubmit`
- `DELETE /room-bookings/{id}`

### Rooms
- `GET /rooms`
- `GET /rooms/{id}`

## Behavior Notes
- Booking groups are soft-deleted when all room bookings are deleted.
- Status filters support separate values for full and partial approval/rejection.
- Date selection supports explicit multi-date arrays via `BookingGroupDates`.

## Related Docs
- Root README: full project overview and frontend setup
