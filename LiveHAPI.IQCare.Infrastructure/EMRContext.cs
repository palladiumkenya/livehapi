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
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupFeature> GroupFeatures { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientFamily> Families { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(c => new { c.GroupID, c.UserID});

            modelBuilder.Entity<GroupFeature>()
                .HasKey(c => new { c.GroupID, c.FeatureID,c.FunctionID });
        }
    }
}