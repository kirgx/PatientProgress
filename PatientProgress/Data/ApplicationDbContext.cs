using Microsoft.EntityFrameworkCore;
using PatientProgress.Models;

namespace PatientProgress.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientImage> PatientImages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate(); 
        }
    }
}
