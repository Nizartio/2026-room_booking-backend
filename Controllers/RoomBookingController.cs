using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/room-bookings")]
    public class RoomBookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/room-bookings
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _context.RoomBookings.ToListAsync();
            return Ok(bookings);
        }

        // GET: api/room-bookings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // POST: api/room-bookings
        [HttpPost]
        public async Task<IActionResult> Create(RoomBooking booking)
        {
            _context.RoomBookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        // PUT: api/room-bookings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoomBooking booking)
        {
            if (id != booking.Id)
                return BadRequest();

            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/room-bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            _context.RoomBookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
