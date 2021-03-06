using HR.WebApi.Repositories;
using HR.WebApi.Repositories.Common;
using HR.WebApi.Services;
using System;

using Unity;

namespace HR.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            container.RegisterType<IDbContextRepository, DbContextRepository>();
            container.RegisterType<IFileRepository, AzureBlobStorageFileRepository>();

            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<IEventDocRepository, EventDocRepository>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IUserSettingRepository, UserSettingRepository>();

            container.RegisterType<IEventDocService, EventDocService>();
            container.RegisterType<IEventService, EventService>();

            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IEmployeeDocRepository, EmployeeDocRepository>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IEmployeeDocService, EmployeeDocService>();

            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IContactRepository, ContactRepository>();
            container.RegisterType<IContactDocRepository, ContactDocRepository>();
            container.RegisterType<INoteRepository, NoteRepository>();

            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IContactDocService, ContactDocService>();
            container.RegisterType<IContactService, ContactService>();
            container.RegisterType<INoteService, NoteService>();


            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IUserSettingService, UserSettingService>();

        }
    }
}