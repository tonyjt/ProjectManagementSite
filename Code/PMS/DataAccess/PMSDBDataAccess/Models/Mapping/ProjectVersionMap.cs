using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class ProjectVersionMap : EntityTypeConfiguration<ProjectVersion>
    {
        public ProjectVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.VersionId);

            // Properties
            this.Property(t => t.VersionName)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("ProjectVersion");
            this.Property(t => t.VersionId).HasColumnName("VersionId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.VersionName).HasColumnName("VersionName");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Project)
                .WithMany(t => t.ProjectVersions)
                .HasForeignKey(d => d.ProjectId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ProjectVersions)
                .HasForeignKey(d => d.Creator);

        }
    }
}
