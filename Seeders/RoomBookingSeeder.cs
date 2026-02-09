using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class RoomBookingSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Idempotent
            if (context.RoomBookings.Any())
                return;

            // Ambil master data (FK safety)
            var labMultimedia = context.Rooms
                .First(r => r.Name == "Lab Multimedia" && r.IsActive);

            var ruangRapat = context.Rooms
                .First(r => r.Name == "Ruang Rapat A" && r.IsActive);

            var customer1 = context.Customers
                .First(c => c.Email == "nizartio@example.com" && c.IsActive);

            var customer2 = context.Customers
                .First(c => c.Email == "bem@pens.ac.id" && c.IsActive);

            var bookings = new List<RoomBooking>
            {
                new RoomBooking
                {
                    RoomId = labMultimedia.Id,
                    CustomerId = customer1.Id,
                    StartTime = DateTime.Today.AddHours(8),
                    EndTime = DateTime.Today.AddHours(10),
                    Status = "Pending"
                },
                new RoomBooking
                {
                    RoomId = ruangRapat.Id,
                    CustomerId = customer2.Id,
                    StartTime = DateTime.Today.AddHours(13),
                    EndTime = DateTime.Today.AddHours(15),
                    Status = "Approved"
                }
            };

            context.RoomBookings.AddRange(bookings);
            context.SaveChanges();
        }
    }
}
