using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class TaskParticipatorMap : EntityTypeConfiguration<TaskParticipator>
    {
        public TaskParticipatorMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskParticipatorId);

            // Properties
            this.Property(t => t.Role)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("TaskParticipator");
            this.Property(t => t.TaskParticipatorId).HasColumnName("TaskParticipatorId");
            this.Property(t => t.TaskId).HasColumnName("TaskId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Role).HasColumnName("Role");

            // Relationships
            this.HasRequired(t => t.ProjectTask)
                .WithMany(t => t.TaskParticipators)
                .HasForeignKey(d => d.TaskId);
            this.HasOptional(t => t.User)
                .WithMany(t => t.TaskParticipators)
                .HasForeignKey(d => d.UserId);

        }
    }
}
