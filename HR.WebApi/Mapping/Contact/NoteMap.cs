using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{
    public class NoteMap : EntityTypeConfiguration<Note>
    {
        public NoteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Title).HasMaxLength(200);
            this.Property(t => t.Desc).HasMaxLength(200);
            this.Property(t => t.ContactId);

            // Table & Column Mappings
            this.ToTable("Notes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Desc).HasColumnName("Desc");
            this.Property(t => t.ContactId).HasColumnName("ContactId");
        }
    }
}