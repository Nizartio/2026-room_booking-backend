using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class RoomBookingSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Kalau sudah ada data, jangan seed lagi
            if (context.RoomBookings.Any())
                return;

            var bookings = new List<RoomBooking>
            {
                new RoomBooking
                {
                    RoomName = "Lab Multimedia",
                    BorrowerName = "Nizartio",
                    StartTime = DateTime.Today.AddHours(8),
                    EndTime = DateTime.Today.AddHours(10),
                    Status = "Pending"
                },
                new RoomBooking
                {
                    RoomName = "Ruang Rapat A",
                    BorrowerName = "BEM PENS",
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
