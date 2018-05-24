using Microsoft.EntityFrameworkCore;

namespace core.Models
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<AkunUser> AkunUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AkunUser>()
                .HasIndex(d =>
                   d.Email
                ).IsUnique(true);
        }

    }

}
