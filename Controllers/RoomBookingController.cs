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
            // 1️⃣ Model validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2️⃣ Customer harus ada & aktif
            var customer = await _context.Customers.FindAsync(booking.CustomerId);
            if (customer == null || !customer.IsActive)
                return BadRequest("Customer not found or inactive.");

            // 3️⃣ Room harus ada & aktif
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null || !room.IsActive)
                return BadRequest("Room not found or inactive.");

            // 4️⃣ Validasi waktu
            if (booking.EndTime <= booking.StartTime)
                return BadRequest("End time must be after start time.");

            // 5️⃣ Cek bentrok jadwal (ignore yang Rejected)
            bool isOverlapping = await _context.RoomBookings.AnyAsync(rb =>
                rb.RoomId == booking.RoomId &&
                rb.Status != "Rejected" &&
                booking.StartTime < rb.EndTime &&
                booking.EndTime > rb.StartTime
            );

            if (isOverlapping)
                return BadRequest("Room is already booked at the selected time.");

            // 6️⃣ Force status (user submit → Pending)
            booking.Status = "Pending";

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

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            booking.Status = "Approved";
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            booking.Status = "Rejected";
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

            booking.IsDeleted = true;
            booking.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        } 
    }
}
