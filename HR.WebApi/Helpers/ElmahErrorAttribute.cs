using HR.WebApi.Models.Common;
using HR.WebApi.Repositories.Common;
using System;
using System.Data.Entity;

namespace HR.WebApi.Helpers
{
    public class ElmahErrorAttribute :
     System.Web.Http.Filters.ExceptionFilterAttribute
    {

        public override void OnException(
            System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {

            if (actionExecutedContext.Exception != null)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);
                try
                {
                    using (var dbcontext = new DbContextRepository())
                    {
                        ErrorLog errorLog = new ErrorLog();

                        errorLog.MethodName = actionExecutedContext.Exception.TargetSite.Name;
                        errorLog.ControllerName = actionExecutedContext.Exception.TargetSite.DeclaringType.Name;
                        var customAtttribute = actionExecutedContext.Exception.TargetSite.CustomAttributes;
                        foreach (var attr in customAtttribute)
                        {
                            errorLog.VerbAttribute = attr.AttributeType.Name;
                            break;
                        }

                        errorLog.ErrorMessage = actionExecutedContext.Exception.Message;

                        errorLog.UserId = 1;
                        errorLog.CreatedDate = DateTime.UtcNow;
                        dbcontext.Entry(errorLog).State = EntityState.Added;
                        dbcontext.ErrorLogs.Add(errorLog);
                        dbcontext.SaveChangesAsync();
                    }

                }
                catch (Exception ex)
                {

                }

            }


            base.OnException(actionExecutedContext);
        }
    }
}