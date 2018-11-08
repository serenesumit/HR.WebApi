using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories.Common
{
    using HR.WebApi.Models;


    #region using

    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    #endregion

    public interface IDbContextRepository : IDisposable
    {

        IDbSet<Employee> Employees { get; set; }

        IDbSet<EmployeeDoc> EmployeeDocs { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
