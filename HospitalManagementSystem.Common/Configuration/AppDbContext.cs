using HospitalManagementSystem.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.BAL.Configurations
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
            base.OnModelCreating(builder);
        }

        public DbSet<Medicines> Medicines { get; set; }
        
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BedType> BedType { get; set; }
        public DbSet<BedNo> BedNo { get; set; }
        public DbSet<BedConfig> BedConfig { get; set; }



        public DbSet<PatientRegistration> PatientRegistration { get; set; }
    }
}
