using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/customers?search=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    c.Name.Contains(search) ||
                    c.Email.Contains(search));
            }

            var customers = await query
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return Ok(customers);
        }

        // GET: /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: /api/customers
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Email uniqueness check (extra safety)
            bool emailExists = await _context.Customers
                .AnyAsync(c => c.Email == customer.Email);

            if (emailExists)
                return BadRequest("Email already exists.");

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = customer.Id }, customer);
        }

        // PUT: /api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer updated)
        {
            if (id != updated.Id)
                return BadRequest();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            customer.Name = updated.Name;
            customer.Email = updated.Email;
            customer.Phone = updated.Phone;
            customer.Address = updated.Address;
            customer.IsActive = updated.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/customers/{id} (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            customer.IsDeleted = true;
            customer.IsActive = false;
            customer.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
