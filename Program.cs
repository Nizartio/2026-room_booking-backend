using backend.Data;
using backend.Seeders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DbContext (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=room_booking.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    RoomSeeder.Seed(context);          // 1️⃣ master rooms
    CustomerSeeder.Seed(context);      // 2️⃣ master customers
    RoomBookingSeeder.Seed(context);
}

app.MapControllers();

app.Run();
