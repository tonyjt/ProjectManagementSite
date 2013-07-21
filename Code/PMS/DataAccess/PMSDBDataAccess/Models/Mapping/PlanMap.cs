using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class PlanMap : EntityTypeConfiguration<Plan>
    {
        public PlanMap()
        {
            // Primary Key
            this.HasKey(t => t.PlanId);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Plan");
            this.Property(t => t.PlanId).HasColumnName("PlanId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TaskParticipatorId).HasColumnName("TaskParticipatorId");
            this.Property(t => t.AllDay).HasColumnName("AllDay");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.TaskParticipator)
                .WithMany(t => t.Plans)
                .HasForeignKey(d => d.TaskParticipatorId);

        }
    }
}
