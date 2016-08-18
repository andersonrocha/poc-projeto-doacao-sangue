using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DoacaoSangue.Web.Infra;
using DoacaoSangue.Web.Models;
using Newtonsoft.Json;

namespace DoacaoSangue.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [Route("login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (UserManager.ValidateUser(model, Response))
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Usuário ou senha incorreto.");
            }

            return View(model);
        }

        [Route("sair")]
        public ActionResult Logoff()
        {
            UserManager.Logoff(Session, Response);

            return RedirectToAction("Login");
        }
    }
}