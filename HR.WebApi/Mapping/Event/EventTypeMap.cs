﻿using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HR.WebApi.Mapping
{
    public class EventTypeMap : EntityTypeConfiguration<EventType>
    {
        public EventTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Name).HasMaxLength(200);
         
            // Table & Column Mappings
            this.ToTable("EventTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");

        }

    }
}