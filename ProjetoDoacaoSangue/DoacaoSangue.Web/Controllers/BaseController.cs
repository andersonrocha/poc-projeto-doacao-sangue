using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoacaoSangue.Web.Infra;

namespace DoacaoSangue.Web.Controllers
{
    public class BaseController : Controller
    {
        public MyPrincipal CurrentUser => (MyPrincipal)User;
        public string AuthToken => CurrentUser.User.JwtToken;
    }
}