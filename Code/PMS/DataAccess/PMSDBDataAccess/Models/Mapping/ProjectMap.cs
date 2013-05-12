using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Project");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.StartDate).HasColumnName("StartDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Projects)
                .HasForeignKey(d => d.Creator);

        }
    }
}
