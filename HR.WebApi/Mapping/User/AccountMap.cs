using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountNumber);

            this.Property(t => t.AccountNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(t => t.AccName).HasMaxLength(200);
          

            // Table & Column Mappings
            this.ToTable("Accounts");
            this.Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            this.Property(t => t.AccName).HasColumnName("AccName");

        }

    }
}