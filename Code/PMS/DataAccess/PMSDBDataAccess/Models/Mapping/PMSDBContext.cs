using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PMS.PMSDBDataAccess.Models.Mapping;
using PMS.Model;

namespace PMSDBDataAccess.Models
{
    public partial class PMSDBContext : DbContext
    {
        static PMSDBContext()
        {
            Database.SetInitializer<PMSDBContext>(null);
        }

        public PMSDBContext()
            : base("Name=PMSDBContext")
        {
        }

        public DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectParticipator> ProjectParticipators { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectVersion> ProjectVersions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new C__RefactorLogMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ProjectParticipatorMap());
            modelBuilder.Configurations.Add(new RequirementMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ProjectVersionMap());
        }
    }
}
