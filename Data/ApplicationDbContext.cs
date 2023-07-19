using Airport_Food_Court_App__Vendor_Side_.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Airport_Food_Court_App__Vendor_Side_.Data
{
    public class ApplicationDbContext : IdentityDbContext<Vendor>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vendor>()
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder.Entity<Vendor>()
                .Property(e => e.IsOpen);
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}