using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Title).HasMaxLength(2000);
            this.Property(t => t.Location).HasMaxLength(2000);
            this.Property(t => t.City).HasMaxLength(200);
            this.Property(t => t.State).HasMaxLength(200);
            this.Property(t => t.Zip).HasMaxLength(200);
            this.Property(t => t.StartDate);
            this.Property(t => t.EndDate);
            this.Property(t => t.Website).HasMaxLength(200);
            this.Property(t => t.Schedule).HasMaxLength(200);
            this.Property(t => t.Agenda).HasMaxLength(200);
            this.Property(t => t.EventTypeId);

            // Table & Column Mappings
            this.ToTable("Events");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Zip).HasColumnName("Zip");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Schedule).HasColumnName("Schedule");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Agenda).HasColumnName("Agenda");
            this.Property(t => t.EventTypeId).HasColumnName("EventTypeId");

        }

    }
}