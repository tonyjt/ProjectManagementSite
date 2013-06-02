using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class RequirementHistoryMap : EntityTypeConfiguration<RequirementHistory>
    {
        public RequirementHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoryId);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("RequirementHistory");
            this.Property(t => t.HistoryId).HasColumnName("HistoryId");
            this.Property(t => t.RequirementId).HasColumnName("RequirementId");
            this.Property(t => t.VersionId).HasColumnName("VersionId");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.ProjectVersion)
                .WithMany(t => t.RequirementHistories)
                .HasForeignKey(d => d.VersionId);
            this.HasOptional(t => t.Requirement)
                .WithMany(t => t.RequirementHistories)
                .HasForeignKey(d => d.ParentId);

        }
    }
}
