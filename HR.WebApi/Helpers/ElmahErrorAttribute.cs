using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

                var test = actionExecutedContext.Exception.InnerException;
                var test1 = actionExecutedContext.Exception.Data;
                var test2 = actionExecutedContext.Exception.Message;
                var test3 = actionExecutedContext.Exception.Source;
            }
                

            base.OnException(actionExecutedContext);
        }
    }
}