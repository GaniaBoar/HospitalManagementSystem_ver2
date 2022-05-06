using HospitalManagementSystem.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.BAL.Configurations
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //this is for making column unique

            //builder.Entity<Animals>()
            //.HasIndex(u => u.Name)
            //.IsUnique();

           

            base.OnModelCreating(builder);
        }

        public DbSet<Medicines> Medicines { get; set; }
        public DbSet<Bed> Bed { get; set; }
        
        public DbSet<Stock> Stock { get; set; }
       
        public DbSet<test> test { get; set; }
        public DbSet<PatientRegistration> PatientRegistration { get; set; }
    }
}
