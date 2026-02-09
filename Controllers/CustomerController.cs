using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs.Requests;
using backend.DTOs.Responses;


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
                .Select(c => new CustomerResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }


        // POST: /api/customers
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool emailExists = await _context.Customers
                .AnyAsync(c => c.Email == request.Email);

            if (emailExists)
                return BadRequest("Email already exists.");

            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                IsActive = true
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var response = new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt
            };

            return CreatedAtAction(nameof(GetById),
                new { id = customer.Id }, response);
        }


        // PUT: /api/customers/{id}
       [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            bool emailExists = await _context.Customers
                .AnyAsync(c => c.Email == request.Email && c.Id != id);

            if (emailExists)
                return BadRequest("Email already exists.");

            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.Address = request.Address;
            customer.IsActive = request.IsActive;

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
