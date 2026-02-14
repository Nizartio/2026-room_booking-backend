using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs.Requests;
using backend.DTOs.Responses;

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
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? status = null,
            [FromQuery] int? roomId = null,
            [FromQuery] int? customerId = null,
            [FromQuery] string? search = null
        )
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var query = _context.RoomBookings
                .Where(rb => !rb.IsDeleted)
                .Include(rb => rb.Room)
                .Include(rb => rb.Customer)
                .AsQueryable();

            // 🔍 Filtering
            if (!string.IsNullOrWhiteSpace(status) &&
                Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
            {
                query = query.Where(rb => rb.Status == parsedStatus);
            }


            if (roomId.HasValue)
                query = query.Where(rb => rb.RoomId == roomId);

            if (customerId.HasValue)
                query = query.Where(rb => rb.CustomerId == customerId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.ToLower();
                query = query.Where(rb => 
                    rb.Room.Name.Contains(keyword) ||
                    rb.Customer.Name.Contains(keyword) ||
                    rb.Customer.Email.Contains(keyword)
                );
            }

            var totalItems = await query.CountAsync();

            var bookings = await query
              .OrderByDescending(rb => rb.StartTime)
              .Skip((page - 1) * pageSize)
              .Take(pageSize)
              .Select(rb => new RoomBookingResponseDto
              {
                  Id = rb.Id,
                  RoomId = rb.RoomId,
                  RoomName = rb.Room.Name,

                  CustomerId = rb.CustomerId,
                  CustomerName = rb.Customer.Name,
                  CustomerEmail = rb.Customer.Email,

                  StartTime = rb.StartTime,
                  EndTime = rb.EndTime,
                  Status = rb.Status.ToString()
              })
              .ToListAsync();

              return Ok(new
              {
                  page,
                  pageSize,
                  totalItems,
                  totalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                  data = bookings
              });
        }

        // GET: api/room-bookings/admin/pending
        [HttpGet("admin")]
        public async Task<IActionResult> GetForAdmin(
            [FromQuery] string? status = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var query = _context.RoomBookings
                .Include(rb => rb.Room)
                .Include(rb => rb.Customer)
                .Where(rb => !rb.IsDeleted)
                .AsQueryable();

            // Filter by status (optional)
            if (!string.IsNullOrWhiteSpace(status) &&
                Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
            {
                query = query.Where(rb => rb.Status == parsedStatus);
            }

            var totalItems = await query.CountAsync();

            var bookings = await query
                .OrderBy(rb => rb.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(rb => new RoomBookingResponseDto
                {
                    Id = rb.Id,
                    RoomId = rb.RoomId,
                    RoomName = rb.Room.Name,

                    CustomerId = rb.CustomerId,
                    CustomerName = rb.Customer.Name,
                    CustomerEmail = rb.Customer.Email,

                    StartTime = rb.StartTime,
                    EndTime = rb.EndTime,
                    Status = rb.Status.ToString()
                })
                .ToListAsync();

            return Ok(new
            {
                page,
                pageSize,
                totalItems,
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                data = bookings
            });
        }
  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _context.RoomBookings
                .Where(rb => rb.Id == id)
                .Select(rb => new RoomBookingDetailResponseDto
                {
                    Id = rb.Id,

                    RoomId = rb.RoomId,
                    RoomName = rb.Room.Name,
                    RoomCapacity = rb.Room.Capacity,

                    CustomerId = rb.CustomerId,
                    CustomerName = rb.Customer.Name,
                    CustomerEmail = rb.Customer.Email,
                    CustomerPhone = rb.Customer.Phone,

                    StartTime = rb.StartTime,
                    EndTime = rb.EndTime,
                    Status = rb.Status.ToString(),
                })
                .FirstOrDefaultAsync();

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }


        // POST: api/room-bookings
       [HttpPost]
        public async Task<IActionResult> Create(CreateRoomBookingRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validasi customer & room (sudah kita buat)
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null || !customer.IsActive)
                return BadRequest("Customer not found or inactive.");

            var room = await _context.Rooms.FindAsync(request.RoomId);
            if (room == null || !room.IsActive)
                return BadRequest("Room not found or inactive.");

            if (request.EndTime <= request.StartTime)
                return BadRequest("End time must be after start time.");

            bool isOverlapping = await _context.RoomBookings.AnyAsync(rb =>
                rb.RoomId == request.RoomId &&
                rb.Status != BookingStatus.Rejected &&
                request.StartTime < rb.EndTime &&
                request.EndTime > rb.StartTime
            );

            if (isOverlapping)
                return BadRequest("Room is already booked at the selected time.");

            var booking = new RoomBooking
            {
                RoomId = request.RoomId,
                CustomerId = request.CustomerId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = BookingStatus.Pending
            };

            _context.RoomBookings.Add(booking);
            await _context.SaveChangesAsync();

            var response = new RoomBookingResponseDto
            {
                Id = booking.Id,
                RoomId = room.Id,
                RoomName = room.Name,
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                CustomerEmail = customer.Email,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Status = booking.Status.ToString()
            };

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, response);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk(
            [FromBody] CreateBulkRoomBookingRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new BulkRoomBookingResponseDto
            {
                TotalRequested = request.Bookings.Count
            };

            foreach (var booking in request.Bookings)
            {
                var result = new BulkBookingResultDto
                {
                    RoomId = booking.RoomId,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime
                };

                // VALIDASI ROOM ADA
                var room = await _context.Rooms
                    .FirstOrDefaultAsync(r => r.Id == booking.RoomId);

                if (room == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Room not found.";
                    response.Results.Add(result);
                    continue;
                }

                if (!room.IsActive)
                {
                    result.Success = false;
                    result.ErrorMessage = "Room is not active.";
                    response.Results.Add(result);
                    continue;
                }

                // VALIDASI BENTROK
                bool isConflict = await _context.RoomBookings
                    .AnyAsync(rb =>
                        rb.RoomId == booking.RoomId &&
                        rb.StartTime < booking.EndTime &&
                        booking.StartTime < rb.EndTime
                    );

                if (isConflict)
                {
                    result.Success = false;
                    result.ErrorMessage = "Room already booked at this time.";
                    response.Results.Add(result);
                    continue;
                }

                // SIMPAN BOOKING
                var newBooking = new RoomBooking
                {
                    RoomId = booking.RoomId,
                    CustomerId = booking.CustomerId,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    Status = BookingStatus.Pending
                };

                _context.RoomBookings.Add(newBooking);
                await _context.SaveChangesAsync();

                result.Success = true;
                response.Results.Add(result);
            }

            response.SuccessCount = response.Results.Count(r => r.Success);
            response.FailedCount = response.Results.Count(r => !r.Success);

            return Ok(response);
        }
        [HttpPost("check-conflicts")]
        public async Task<ActionResult<IEnumerable<object>>> CheckConflicts(
            [FromBody] List<CreateRoomBookingRequestDto> requests)
        {
            var conflicts = new List<object>();

            foreach (var req in requests)
            {
                var hasConflict = await _context.RoomBookings
                    .AnyAsync(rb =>
                        !rb.IsDeleted &&
                        rb.RoomId == req.RoomId &&
                        (req.StartTime < rb.EndTime) &&
                        (req.EndTime > rb.StartTime)
                    );

                if (hasConflict)
                {
                    conflicts.Add(new
                    {
                        req.RoomId,
                        req.StartTime,
                        req.EndTime
                    });
                }
            }

            return Ok(conflicts);
        }

        // PUT: api/room-bookings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateRoomBookingTimeDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _context.RoomBookings
                .FirstOrDefaultAsync(rb => rb.Id == id && !rb.IsDeleted);

            if (booking == null)
                return NotFound();

            if (booking.Status != BookingStatus.Rejected)
                return BadRequest("Only rejected bookings can be edited.");

            if (request.EndTime <= request.StartTime)
                return BadRequest("End time must be after start time.");

            bool isConflict = await _context.RoomBookings
                .AnyAsync(rb =>
                    rb.Id != id &&
                    rb.RoomId == booking.RoomId &&
                    !rb.IsDeleted &&
                    rb.Status != BookingStatus.Rejected &&
                    request.StartTime < rb.EndTime &&
                    request.EndTime > rb.StartTime
                );

            if (isConflict)
                return BadRequest("Room already booked at this time.");

            booking.StartTime = request.StartTime;
            booking.EndTime = request.EndTime;

            // Reset status
            booking.Status = BookingStatus.Pending;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking resubmitted for approval." });
        }


        
        // PUT: api/room-bookings/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            UpdateRoomBookingStatusDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _context.RoomBookings
                .FirstOrDefaultAsync(rb => rb.Id == id && !rb.IsDeleted);

            if (booking == null)
                return NotFound();

            if (booking.Status != BookingStatus.Pending)
                return BadRequest("Only pending bookings can be updated.");

            if (request.Status != BookingStatus.Approved &&
                request.Status != BookingStatus.Rejected)
                return BadRequest("Invalid status transition.");

            // 🔥 If approving, re-check conflict
            if (request.Status == BookingStatus.Approved)
            {
                bool isConflict = await _context.RoomBookings
                    .AnyAsync(rb =>
                        rb.Id != id &&
                        rb.RoomId == booking.RoomId &&
                        rb.Status == BookingStatus.Approved &&
                        !rb.IsDeleted &&
                        booking.StartTime < rb.EndTime &&
                        booking.EndTime > rb.StartTime
                    );

                if (isConflict)
                    return BadRequest("Cannot approve. Time slot already taken.");
            }

            booking.Status = request.Status;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"Booking {request.Status}"
            });
        }


        // PUT: api/room-bookings/{id}/resubmit
        /// <summary>
        /// Customer: Edit and resubmit rejected booking
        /// </summary>
        [HttpPut("{id}/resubmit")]
        public async Task<IActionResult> Resubmit(
            int id,
            UpdateRoomBookingTimeDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _context.RoomBookings
                .Include(rb => rb.Room)
                .FirstOrDefaultAsync(rb => rb.Id == id && !rb.IsDeleted);

            if (booking == null)
                return NotFound();

            if (booking.Status != BookingStatus.Rejected)
                return BadRequest("Only rejected bookings can be resubmitted.");

            if (request.EndTime <= request.StartTime)
                return BadRequest("End time must be after start time.");

            // check conflict
            bool isConflict = await _context.RoomBookings
                .AnyAsync(rb =>
                    rb.Id != id &&
                    rb.RoomId == booking.RoomId &&
                    rb.Status != BookingStatus.Rejected &&
                    request.StartTime < rb.EndTime &&
                    request.EndTime > rb.StartTime
                );

            if (isConflict)
                return BadRequest("Room is already booked at this time.");

            booking.StartTime = request.StartTime;
            booking.EndTime = request.EndTime;
            booking.Status = BookingStatus.Pending;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking resubmitted successfully." });
        }

        // DELETE: api/room-bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.RoomBookings
                .FirstOrDefaultAsync(rb => rb.Id == id);

            if (booking == null || booking.IsDeleted)
                return NotFound();
            
            if (booking.Status != BookingStatus.Rejected)
                return BadRequest("Only rejected bookings can be deleted.");

            booking.IsDeleted = true;
            booking.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Booking deleted successfully."
            });
        }
    }
}
