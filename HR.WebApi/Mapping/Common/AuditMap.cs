using HR.WebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping.Common
{
    public class AuditMap : EntityTypeConfiguration<Audit>
    {
        public AuditMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // Table & Column Mappings
            this.ToTable("Audits");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.UtcDatetime).HasColumnName("UtcDatetime");
            this.Property(t => t.KeyValues).HasColumnName("KeyValues");
            this.Property(t => t.OldValues).HasColumnName("OldValues");
            this.Property(t => t.NewValues).HasColumnName("NewValues");
        }
    }
}