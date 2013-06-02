using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class RequirementMap : EntityTypeConfiguration<Requirement>
    {
        public RequirementMap()
        {
            // Primary Key
            this.HasKey(t => t.RequirementId);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Requirement");
            this.Property(t => t.RequirementId).HasColumnName("RequirementId");
            this.Property(t => t.VersionId).HasColumnName("VersionId");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.IsValid).HasColumnName("IsValid");

            // Relationships
            this.HasRequired(t => t.ProjectVersion)
                .WithMany(t => t.Requirements)
                .HasForeignKey(d => d.VersionId);
            this.HasOptional(t => t.Requirement2)
                .WithMany(t => t.Requirement1)
                .HasForeignKey(d => d.ParentId);

        }
    }
}
