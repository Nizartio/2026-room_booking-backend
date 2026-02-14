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
                new() { Name = "Lab Multimedia", Capacity = 30, Description = "Lab untuk editing dan multimedia" },
                new() { Name = "Ruang Rapat A", Capacity = 15, Description = "Meeting kecil" },
                new() { Name = "Aula Utama", Capacity = 200, Description = "Event besar kampus" },
                new() { Name = "Lab Robotik", Capacity = 25, Description = "Lab robotik dan IoT" },
                new() { Name = "Ruang Diskusi 1", Capacity = 10, Description = "Diskusi kelompok kecil" }
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    }
}