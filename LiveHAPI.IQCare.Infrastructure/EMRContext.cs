using LiveHAPI.IQCare.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.IQCare.Infrastructure
{
    public class EMRContext : DbContext
    {
        public EMRContext(DbContextOptions<EMRContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientFamily> Families { get; set; }

    }
}