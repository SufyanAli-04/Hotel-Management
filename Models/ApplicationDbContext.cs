using Hotel_Management.Models;
using System.Data.Entity;

namespace HotelManagementSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Update the constructor to use the existing connection string name
        public ApplicationDbContext() : base("name=ClassEntities")
        {
        }

        // Define DbSet properties for each table in your database
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configurations can go here
        }
    }
}
