using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace DoacaoSangue.Api.Controllers
{
    public class BaseController : ApiController
    {
        private ClaimsIdentity _claimsIdentity;

        public string IdUsuario 
        {
            get
            {
                if (_claimsIdentity == null)
                    LoadClaimsIdentity();

                return _claimsIdentity.FindFirst("id").Value;
            }
        }

        protected void LoadClaimsIdentity()
        {
            if (User?.Identity == null)
                return;

            _claimsIdentity = (ClaimsIdentity)User.Identity;
        }
    }
}