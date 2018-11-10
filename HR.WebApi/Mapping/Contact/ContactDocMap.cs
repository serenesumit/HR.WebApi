using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{

    public class ContactDocMap : EntityTypeConfiguration<ContactDoc>
    {
        public ContactDocMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Link).HasMaxLength(2000);
            this.Property(t => t.Name).HasMaxLength(200);
            this.Property(t => t.ContactId);

            // Table & Column Mappings
            this.ToTable("ContactDocs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.ContactId).HasColumnName("ContactId");

        }

    }
}