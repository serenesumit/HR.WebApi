
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mapping
{
    public class EmployeeDocMap : EntityTypeConfiguration<EmployeeDoc>
    {
        public EmployeeDocMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

             this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Link).HasMaxLength(2000);
            this.Property(t => t.Name).HasMaxLength(200);
            this.Property(t => t.EmployeeId);

            // Table & Column Mappings
            this.ToTable("EmployeeDocs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");

        }

    }
}
