using backend.Data;
using backend.Models;

namespace backend.Seeders
{
    public static class CustomerSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Idempotent: kalau sudah ada customer, stop
            if (context.Customers.Any())
                return;

            var customers = new List<Customer>
            {
                new Customer
                {
                    Name = "Nizartio (UKM E2C)",
                    Email = "nizartiochandraadinata@gmail.com",
                    Phone = "089530471856",
                    Address = "Surabaya",
                    IsActive = true
                },
                new Customer
                {
                    Name = "BEM PENS",
                    Email = "bem@pens.ac.id",
                    Phone = "031-123456",
                    Address = "PENS, Surabaya",
                    IsActive = true
                }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
