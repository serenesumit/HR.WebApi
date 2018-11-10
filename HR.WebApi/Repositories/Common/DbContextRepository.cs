

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
        }

        public IDbSet<Employee> Employees { get; set; }

        public IDbSet<EmployeeDoc> EmployeeDocs { get; set; }
       
        public IDbSet<Contact> Contacts { get; set; }

        public IDbSet<ContactDoc> ContactDocs { get; set; }

        public IDbSet<Department> Departments { get; set; }

        public IDbSet<Note> Notes { get; set; }

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

            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new ContactDocMap());

        }
    }
}
