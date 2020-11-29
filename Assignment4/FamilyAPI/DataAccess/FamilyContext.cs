using FamilyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyAPI.DataAccess
{
    public class FamilyContext:DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // name of database: Family.db    -- .db may not be needed..?
            optionsBuilder.UseSqlite("Data Source = Family.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildInterest>()
                .HasKey(ci => new {ci.ChildId, ci.InterestId});

            modelBuilder.Entity<Family>()
                .HasKey(fam => new {fam.StreetName, fam.HouseNumber});

            modelBuilder.Entity<ChildInterest>()
                .HasOne(ci => ci.Child)
                .WithMany(child => child.ChildInterests)
                .HasForeignKey(ci => ci.ChildId);

            modelBuilder.Entity<ChildInterest>()
                .HasOne(ci => ci.Interest)
                .WithMany(i => i.ChildInterests)
                .HasForeignKey(ci => ci.InterestId);
        }
    }
}