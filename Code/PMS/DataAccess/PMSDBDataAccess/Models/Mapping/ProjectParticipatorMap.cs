using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PMS.Model;
namespace PMS.PMSDBDataAccess.Models.Mapping
{
    public class ProjectParticipatorMap : EntityTypeConfiguration<ProjectParticipator>
    {
        public ProjectParticipatorMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectId);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProjectParticipator");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Roles).HasColumnName("Roles");
            this.Property(t => t.JoinTime).HasColumnName("JoinTime");
        }
    }
}
