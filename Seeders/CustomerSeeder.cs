using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class CustomerSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Customers.Any())
                return;

            var customers = new List<Customer>
            {
                new() { Name = "Nizartio (UKM E2C)", Email = "nizartio@campus.ac.id", Phone = "081234567890", Address = "PENS Surabaya" },
                new() { Name = "Raka (HIMA IF)", Email = "raka@campus.ac.id", Phone = "081111111111", Address = "Gedung D3" },
                new() { Name = "Salsa (BEM)", Email = "salsa@campus.ac.id", Phone = "082222222222", Address = "Gedung Pusat" },
                new() { Name = "Dimas (UKM Robotik)", Email = "dimas@campus.ac.id", Phone = "083333333333", Address = "Lab Robotik" },
                new() { Name = "Alya (UKM Musik)", Email = "alya@campus.ac.id", Phone = "084444444444", Address = "Aula Kampus" },
                new() { Name = "Fajar (HIMA Elektro)", Email = "fajar@campus.ac.id", Phone = "085555555555", Address = "Gedung Elektro" },
                new() { Name = "Putri (UKM Tari)", Email = "putri@campus.ac.id", Phone = "086666666666", Address = "Gedung Seni" },
                new() { Name = "Ardi (UKM Futsal)", Email = "ardi@campus.ac.id", Phone = "087777777777", Address = "Lapangan Indoor" },
                new() { Name = "Lina (HIMA Kominfo)", Email = "lina@campus.ac.id", Phone = "088888888888", Address = "Gedung IT" },
                new() { Name = "Bayu (UKM English)", Email = "bayu@campus.ac.id", Phone = "089999999999", Address = "Ruang Bahasa" }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}