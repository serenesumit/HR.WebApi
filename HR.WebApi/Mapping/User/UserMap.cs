using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UID);

            this.Property(t => t.UID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(t => t.FirstName).HasMaxLength(200);
            this.Property(t => t.Email).HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UID).HasColumnName("UID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.Email).HasColumnName("Email");

        }

    }
}