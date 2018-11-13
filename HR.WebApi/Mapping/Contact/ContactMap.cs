using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{

    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Title).HasMaxLength(2000);
            this.Property(t => t.Bio).HasMaxLength(2000);
            this.Property(t => t.Name).HasMaxLength(200);
            this.Property(t => t.PhoneNumber);
            this.Property(t => t.MobileNumber);
            this.Property(t => t.EmailAddress);
            this.Property(t => t.QuickFacts).HasMaxLength(200);
            this.Property(t => t.Website).HasMaxLength(200);
            
            this.Property(t => t.DepartmentId);

            // Table & Column Mappings
            this.ToTable("Contacts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Bio).HasColumnName("Bio");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.QuickFacts).HasColumnName("QuickFacts");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
        }

    }
}