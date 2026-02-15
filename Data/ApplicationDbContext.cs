using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomBooking> RoomBookings => Set<RoomBooking>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<BookingGroup> BookingGroups => Set<BookingGroup>();
        public DbSet<BookingGroupDate> BookingGroupDates => Set<BookingGroupDate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filter
            modelBuilder.Entity<Customer>()
                .HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Room>() 
                .HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<RoomBooking>()
                .HasQueryFilter(rb => !rb.IsDeleted);
            modelBuilder.Entity<BookingGroup>()
                .HasQueryFilter(bg => !bg.IsDeleted);

            // Relationships
            modelBuilder.Entity<RoomBooking>()
                .HasOne(rb => rb.BookingGroup)
                .WithMany(bg => bg.RoomBookings)
                .HasForeignKey(rb => rb.BookingGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingGroupDate>()
                .HasOne(bgd => bgd.BookingGroup)
                .WithMany(bg => bg.BookingGroupDates)
                .HasForeignKey(bgd => bgd.BookingGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique email for Customer
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();
            //For Enums
            modelBuilder.Entity<RoomBooking>()
                .Property(rb => rb.Status)
                .HasConversion<string>();
            modelBuilder.Entity<BookingGroup>()
                .Property(bg => bg.Status)
                .HasConversion<string>();

        }
    }
}
