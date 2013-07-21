using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PMS.PMSDBDataAccess.Models.Mapping;
using PMS.Model;

namespace PMS.PMSDBDataAccess.Models
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
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectParticipator> ProjectParticipators { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectVersion> ProjectVersions { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RequirementHistory> RequirementHistories { get; set; }
        public DbSet<TaskParticipator> TaskParticipators { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new C__RefactorLogMap());
            modelBuilder.Configurations.Add(new PlanMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ProjectParticipatorMap());
            modelBuilder.Configurations.Add(new ProjectTaskMap());
            modelBuilder.Configurations.Add(new ProjectVersionMap());
            modelBuilder.Configurations.Add(new RequirementMap());
            modelBuilder.Configurations.Add(new RequirementHistoryMap());
            modelBuilder.Configurations.Add(new TaskParticipatorMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
