namespace HR.WebApi.Repositories.Common
{
    using HR.WebApi.Models;
    using HR.WebApi.Models.Common;


    #region using

    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    #endregion

    public interface IDbContextRepository : IDisposable
    {

        IDbSet<Employee> Employees { get; set; }

        IDbSet<EmployeeDoc> EmployeeDocs { get; set; }

        IDbSet<EmployeeType> EmployeeTypes { get; set; }

        IDbSet<Contact> Contacts { get; set; }

        IDbSet<ContactDoc> ContactDocs { get; set; }

        IDbSet<Department> Departments { get; set; }

        IDbSet<Note> Notes { get; set; }

        IDbSet<Event> Events { get; set; }

        IDbSet<EventDoc> EventDocs { get; set; }

        IDbSet<EventType> EventTypes { get; set; }

        IDbSet<UserSetting> UserSettings { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<Account> Accounts { get; set; }

        // IDbSet<UserAccount> UserAccounts { get; set; }

        IDbSet<Audit> Audits { get; set; }

        IDbSet<ErrorLog> ErrorLogs { get; set; }

        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
