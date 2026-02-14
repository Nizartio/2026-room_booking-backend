using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class RoomBookingSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.RoomBookings.Any())
                return;

            var firstCustomer = context.Customers.First();
            var firstRoom = context.Rooms.First();

            var bookings = new List<RoomBooking>
            {
                new()
                {
                    RoomId = firstRoom.Id,
                    CustomerId = firstCustomer.Id,
                    StartTime = DateTime.UtcNow.AddDays(1),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(2),
                    Status = BookingStatus.Pending
                },
                new()
                {
                    RoomId = firstRoom.Id,
                    CustomerId = firstCustomer.Id,
                    StartTime = DateTime.UtcNow.AddDays(2),
                    EndTime = DateTime.UtcNow.AddDays(2).AddHours(3),
                    Status = BookingStatus.Approved
                },
                new()
                {
                    RoomId = firstRoom.Id,
                    CustomerId = firstCustomer.Id,
                    StartTime = DateTime.UtcNow.AddDays(3),
                    EndTime = DateTime.UtcNow.AddDays(3).AddHours(1),
                    Status = BookingStatus.Rejected
                }
            };

            context.RoomBookings.AddRange(bookings);
            context.SaveChanges();
        }
    }
}