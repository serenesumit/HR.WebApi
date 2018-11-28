using HR.WebApi.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HR.WebApi.Mapping.Common
{
    public class ErrorLogMap : EntityTypeConfiguration<ErrorLog>
    {
        public ErrorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // Table & Column Mappings
            this.ToTable("ErrorLogs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ControllerName).HasColumnName("ControllerName");
            this.Property(t => t.MethodName).HasColumnName("MethodName");
            this.Property(t => t.ErrorMessage).HasColumnName("ErrorMessage");
            this.Property(t => t.ErrorType).HasColumnName("ErrorType");
            this.Property(t => t.VerbAttribute).HasColumnName("VerbAttribute");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}