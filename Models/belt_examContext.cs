using Microsoft.EntityFrameworkCore;

namespace belt_exam.Models
{
    public class belt_examContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public belt_examContext(DbContextOptions<belt_examContext> options) : base(options) { }

        public DbSet<users> users { get; set; }
        public DbSet<activities> activities { get; set; }
        public DbSet<attendees> attendees { get; set; }
    }
}