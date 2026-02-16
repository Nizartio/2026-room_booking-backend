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
                // HH Building Rooms
                new() { Name = "HH103", Capacity = 60, Description = "Ruang kuliah Gedung HH" },
                new() { Name = "HH104", Capacity = 30, Description = "Ruang kuliah Gedung HH" },
                new() { Name = "HH105", Capacity = 90, Description = "Ruang kuliah Gedung HH" },
                new() { Name = "HH 106 B", Capacity = 60, Description = "Ruang kuliah Gedung HH - Bawah" },
                new() { Name = "HH 106 T", Capacity = 60, Description = "Ruang kuliah Gedung HH - Atas" },
                new() { Name = "HH 201", Capacity = 30, Description = "Ruang kuliah Gedung HH" },
                new() { Name = "HH 203", Capacity = 90, Description = "Ruang kuliah Gedung HH" },
                new() { Name = "HH 204 B", Capacity = 60, Description = "Ruang kuliah Gedung HH - Bawah" },
                new() { Name = "HH 204 T", Capacity = 30, Description = "Ruang kuliah Gedung HH - Atas" },
                new() { Name = "HH-202", Capacity = 90, Description = "Ruang kuliah Gedung HH" },
                
                // B Building Rooms
                new() { Name = "B 201", Capacity = 60, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 202", Capacity = 30, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 203", Capacity = 90, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 204", Capacity = 60, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 205", Capacity = 30, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 301", Capacity = 90, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 302", Capacity = 60, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 303", Capacity = 30, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 304", Capacity = 90, Description = "Ruang kuliah Gedung B" },
                new() { Name = "B 305", Capacity = 60, Description = "Ruang kuliah Gedung B" },
                
                // A Building Rooms
                new() { Name = "A 301", Capacity = 30, Description = "Ruang kuliah Gedung A" },
                new() { Name = "A 302", Capacity = 90, Description = "Ruang kuliah Gedung A" },
                new() { Name = "A 303", Capacity = 60, Description = "Ruang kuliah Gedung A" },
                new() { Name = "A 304", Capacity = 30, Description = "Ruang kuliah Gedung A" },
                
                // Sports Fields
                new() { Name = "Lapangan Merah", Capacity = 0, Description = "Lapangan olahraga outdoor" },
                new() { Name = "Lapangan Futsal", Capacity = 0, Description = "Lapangan futsal" },
                new() { Name = "Lapangan Basket", Capacity = 0, Description = "Lapangan basket" },
                
                // Theater
                new() { Name = "Theater D3", Capacity = 90, Description = "Teater D3" }
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    }
}