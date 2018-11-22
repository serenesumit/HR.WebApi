using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{
    public class UserSettingMap : EntityTypeConfiguration<UserSetting>
    {
        public UserSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(t => t.Settings);

            // Table & Column Mappings
            this.ToTable("UserSettings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Settings).HasColumnName("Settings");

        }
    }
}