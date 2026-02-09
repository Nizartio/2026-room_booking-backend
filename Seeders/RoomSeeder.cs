using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class RoomSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Rooms.Any())
                return;

            var rooms = new List<Room>
            {
                new Room
                {
                    Name = "Lab Multimedia",
                    Capacity = 40,
                    Location = "Gedung A - Lt 2",
                    IsActive = true
                },
                new Room
                {
                    Name = "Ruang Rapat A",
                    Capacity = 15,
                    Location = "Gedung B - Lt 1",
                    IsActive = true
                }
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    }
}
