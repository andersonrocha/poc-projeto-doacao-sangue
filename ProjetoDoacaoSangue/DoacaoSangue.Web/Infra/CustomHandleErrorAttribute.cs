using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DoacaoSangue.Web.Infra
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Exception exp = context.Exception;

            if (exp is AuthenticationException)
            {
                context.ExceptionHandled = true;
                context.Result = new RedirectResult(FormsAuthentication.LoginUrl);
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}
