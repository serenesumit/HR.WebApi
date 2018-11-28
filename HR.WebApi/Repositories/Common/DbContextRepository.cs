

namespace HR.WebApi.Repositories.Common
{
    using global::Repositories.Mapping;
    using HR.WebApi.Mapping;
    using HR.WebApi.Mapping.Common;
    using HR.WebApi.Models;
    using HR.WebApi.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #region using

    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;


    #endregion

    public partial class DbContextRepository : DbContext, IDbContextRepository
    {
        static DbContextRepository()
        {
            Database.SetInitializer<DbContextRepository>(null);
        }

        public DbContextRepository()
            : base("Name=EmployeeContext")
        {
            //// (connectionString)
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<DbContextRepository>(null);
        }

        public IDbSet<Employee> Employees { get; set; }

        public IDbSet<EmployeeDoc> EmployeeDocs { get; set; }

        public IDbSet<EmployeeType> EmployeeTypes { get; set; }

        public IDbSet<Contact> Contacts { get; set; }

        public IDbSet<ContactDoc> ContactDocs { get; set; }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<EventDoc> EventDocs { get; set; }

        public IDbSet<EventType> EventTypes { get; set; }


        public IDbSet<Department> Departments { get; set; }

        public IDbSet<Note> Notes { get; set; }

        public IDbSet<UserSetting> UserSettings { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Account> Accounts { get; set; }

        // public IDbSet<UserAccount> UserAccounts { get; set; }


        public DbSet<Audit> Audits { get; set; }


        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public int SaveChanges()
        {
            // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
            foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                // For each changed record, get the audit record entries and add them
                foreach (Audit x in GetAuditRecordsForChange(ent, "1"))
                {
                    this.Audits.Add(x);
                }
            }

            // Call the original SaveChanges(), which will save both the changes made and the audit records
            return base.SaveChanges();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = false;
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new EmployeeDocMap());
            modelBuilder.Configurations.Add(new EmployeeTypeMap());

            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new ContactDocMap());


            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new EventTypeMap());
            modelBuilder.Configurations.Add(new EventDocMap());

            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserSettingMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AuditMap());

            modelBuilder.Entity<User>()
               .HasOptional(s => s.UserSetting)
               .WithRequired(u => u.User);

            // This will create UserAccounts Table in the db
            modelBuilder.Entity<User>()
               .HasMany<Account>(s => s.Accounts)
               .WithMany(c => c.Users)
               .Map(cs =>
               {
                   cs.MapLeftKey("UID");
                   cs.MapRightKey("AccountNumber");
                   cs.ToTable("UserAccounts");
               });

        }


        private string GetTableName(DbEntityEntry ent)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
            Type entityType = ent.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                entityType = entityType.BaseType;

            string entityTypeName = entityType.Name;

            EntityContainer container =
                objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = (from meta in container.BaseEntitySets
                                    where meta.ElementType.Name == entityTypeName
                                    select meta.Name).First();
            return entitySetName;
        }

        private string GetPrimaryKey(DbEntityEntry dbEntry)
        {
           string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0).Name;

           // string keyName = "Id";
            string primaryKeyValue = dbEntry.CurrentValues.GetValue<object>(keyName).ToString();
            return primaryKeyValue;
        }

        public string GetPropertieswithValues(DbEntityEntry dbEntry)
        {
            StringBuilder newValues = new StringBuilder();
            foreach (var propertyName in dbEntry.CurrentValues.PropertyNames)
            {
                newValues = newValues.Append(propertyName + "=" + dbEntry.CurrentValues.GetValue<object>(propertyName).ToString() + ",");
            }
            string result = "";
            if (newValues != null)
            {
                result = Convert.ToString(newValues);
            }

            return result;
        }


        private List<Audit> GetAuditRecordsForChange(DbEntityEntry dbEntry, string userId)
        {
            List<Audit> result = new List<Audit>();

            // Get the Table() attribute, if one exists
            var tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            var tableName = GetTableName(dbEntry);
            if (dbEntry.State == EntityState.Added)
            {
                // For Inserts, just add the whole record
                // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()
                result.Add(new Audit()
                {
                    Action = "Added", // Added
                    TableName = tableName,
                    UtcDatetime = DateTime.UtcNow,
                    RecordID = GetPrimaryKey(dbEntry),

                    NewValues = GetPropertieswithValues(dbEntry),
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow
                });
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                var props = dbEntry.Entity.GetType().GetProperties();
                var keyName = props.Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()).Name;

                result.Add(new Audit()
                {
                    Action = "Deleted", // Deleted
                    TableName = tableName,
                    UtcDatetime = DateTime.UtcNow,
                    RecordID = GetPrimaryKey(dbEntry),
                    NewValues = GetPropertieswithValues(dbEntry),
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow
                });
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                var originalEntry = dbEntry.GetDatabaseValues();
                StringBuilder newValues = new StringBuilder();
                StringBuilder oldValues = new StringBuilder();
                foreach (var propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    // For updates, we only want to capture the columns that actually changed
                    if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        var props = dbEntry.Entity.GetType().GetProperties();
                        var keyName = props.Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()).Name;
                        oldValues = oldValues.Append(propertyName + "=" + originalEntry.GetValue<object>(propertyName).ToString() + ",");
                        newValues = newValues.Append(propertyName + "=" + dbEntry.CurrentValues.GetValue<object>(propertyName).ToString() + ",");
                    }
                }

                result.Add(new Audit()
                {
                    Action = "Modified",
                    TableName = tableName,
                    UtcDatetime = DateTime.UtcNow,
                    // RecordID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                    RecordID = GetPrimaryKey(dbEntry),
                    OldValues = oldValues.ToString(),
                    NewValues = newValues.ToString(),
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow
                });


            }

            return result;
        }

    }
}
