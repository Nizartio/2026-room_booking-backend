using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class CustomerSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Customers.Any()) return;

            var customers = new List<Customer>
            {
              new Customer { Name = "Nizartio (UKM E2C)", Email = "nizartio@e2c.ac.id", Phone="0812345671", Address="Jalan A", IsActive=true },
              new Customer { Name = "Raka (BEM)", Email = "raka@bem.ac.id", Phone="0812345672", Address="Jalan B", IsActive=true },
              new Customer { Name = "Dina (HIMA Informatika)", Email = "dina@himaif.ac.id", Phone="0812345673", Address="Jalan C", IsActive=true },
              new Customer { Name = "Adit (UKM Robotika)", Email = "adit@robot.ac.id", Phone="0812345674", Address="Jalan D", IsActive=true },
              new Customer { Name = "Salsa (UKM Musik)", Email = "salsa@musik.ac.id", Phone="0812345675", Address="Jalan E", IsActive=true },
              new Customer { Name = "Fajar (UKM Basket)", Email = "fajar@basket.ac.id", Phone="0812345676", Address="Lapangan", IsActive=true },
              new Customer { Name = "Tasya (UKM Tari)", Email = "tasya@tari.ac.id", Phone="0812345677", Address="Jalan Seni", IsActive=true },
              new Customer { Name = "Kevin (HIMA Elektro)", Email = "kevin@elektro.ac.id", Phone="0812345678", Address="Lab Elektro", IsActive=true },
              new Customer { Name = "Maya (HIMA Mesin)", Email = "maya@mesin.ac.id", Phone="0812345679", Address=" Jalan Workshop", IsActive=true },
              new Customer { Name = "Rio (UKM English Club)", Email = "rio@english.ac.id", Phone="0812345680", Address="Jalan Bahasa", IsActive=true }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}