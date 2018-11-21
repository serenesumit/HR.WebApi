

namespace HR.WebApi.Repositories.Common
{
    using global::Repositories.Mapping;
    using HR.WebApi.Mapping;
    using HR.WebApi.Models;

    #region using

    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Threading.Tasks;


    #endregion

    public class DbContextRepository : DbContext, IDbContextRepository
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

        public IDbSet<UserAccount> UserAccounts { get; set; }

        



        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            try
            {
                // Save changes with the default options
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw;
            }
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
    }
}
