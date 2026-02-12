using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs.Requests;
using backend.DTOs.Responses;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/rooms
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _context.Rooms
                .OrderBy(r => r.Name)
                .Select(r => new RoomResponseDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();

            return Ok(rooms);
        }

        // GET: /api/rooms/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _context.Rooms
                .Where(r => r.Id == id)
                .Select(r => new RoomResponseDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            return Ok(room);
        }
        
        // GET: /api/rooms/unavailable-dates
        [HttpGet("unavailable-dates")]
        public async Task<ActionResult<IEnumerable<string>>> GetUnavailableDates()
        {
            var fullyBookedDates = await _context.RoomBookings
                .Where(rb => !rb.IsDeleted)
                .GroupBy(rb => rb.StartTime.Date)
                .Where(g => g.Count() >= _context.Rooms.Count(r => !r.IsDeleted))
                .Select(g => g.Key.ToString("yyyy-MM-dd"))
                .ToListAsync();

            return Ok(fullyBookedDates);
        }
        // POST: /api/rooms
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool exists = await _context.Rooms
                .AnyAsync(r => r.Name == request.Name);

            if (exists)
                return BadRequest("Room name already exists.");

            var room = new Room
            {
                Name = request.Name,
                Capacity = request.Capacity,
                Description = request.Description,
                IsActive = true
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var response = new RoomResponseDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                Description = room.Description,
                IsActive = room.IsActive,
                CreatedAt = room.CreatedAt
            };

            return CreatedAtAction(nameof(GetById),
                new { id = room.Id }, response);
        }

        // PUT: /api/rooms/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRoomRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            bool exists = await _context.Rooms
                .AnyAsync(r => r.Name == request.Name && r.Id != id);

            if (exists)
                return BadRequest("Room name already exists.");

            room.Name = request.Name;
            room.Capacity = request.Capacity;
            room.Description = request.Description;
            room.IsActive = request.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        

        // DELETE: /api/rooms/{id} (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            room.IsDeleted = true;
            room.IsActive = false;
            room.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
