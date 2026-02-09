using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class RoomSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Idempotent: jangan seed ulang kalau sudah ada data
            if (context.Rooms.Any())
                return;

            var rooms = new List<Room>
            {
                new Room
                {
                    Name = "Lab Multimedia",
                    Capacity = 30,
                    Description = "Laboratorium multimedia untuk praktikum dan presentasi",
                    IsActive = true
                },
                new Room
                {
                    Name = "Ruang Rapat A",
                    Capacity = 15,
                    Description = "Ruang rapat internal dan diskusi",
                    IsActive = true
                },
                new Room
                {
                    Name = "Aula Utama",
                    Capacity = 100,
                    Description = "Aula besar untuk seminar dan acara kampus",
                    IsActive = true
                },
                new Room
                {
                    Name = "Lab Komputer 1",
                    Capacity = 40,
                    Description = "Laboratorium komputer untuk praktikum",
                    IsActive = false // contoh room non-aktif
                }
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    }
}
