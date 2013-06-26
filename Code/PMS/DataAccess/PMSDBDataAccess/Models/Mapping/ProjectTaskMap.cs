using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class ProjectTaskMap : EntityTypeConfiguration<ProjectTask>
    {
        public ProjectTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskId);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.History)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectTask");
            this.Property(t => t.TaskId).HasColumnName("TaskId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.RequirementId).HasColumnName("RequirementId");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.History).HasColumnName("History");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");

            // Relationships
            this.HasRequired(t => t.Project)
                .WithMany(t => t.ProjectTasks)
                .HasForeignKey(d => d.ProjectId);
            this.HasOptional(t => t.Requirement)
                .WithMany(t => t.ProjectTasks)
                .HasForeignKey(d => d.RequirementId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ProjectTasks)
                .HasForeignKey(d => d.Creator);

        }
    }
}
