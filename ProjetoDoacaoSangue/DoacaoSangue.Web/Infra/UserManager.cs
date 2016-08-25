using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using DoacaoSangue.Web.Models;
using Newtonsoft.Json;

namespace DoacaoSangue.Web.Infra
{
    public static class UserManager
    {
        public static bool ValidateUser(LoginModel logon, HttpResponseBase response)
        {
            bool result = false;
            User user = UserManager.AuthenticateUser(logon.EmailUsuario, logon.Senha);

            if (user != null)
            {
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(user);

                response.Cookies.Add(TicketHelper.CreateAuthCookie(logon.EmailUsuario, userData, true));
                result = true;
            }

            return result;
        }

        public static User AuthenticateUser(string email, string senha)
        {
            User user = null;

            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "username", email },
                    { "password", senha },
                    { "grant_type", "password" },
                    { "client_id", ConfigurationManager.AppSettings["ClientIdResource"] }
                });

            HttpClient httpClient = new HttpClient();
            var result = httpClient.PostAsync(ConfigurationManager.AppSettings["AuthTokenServer"], formContent).Result;
            var contentResult = result.Content.ReadAsStringAsync().Result;
            dynamic jsonResult = JsonConvert.DeserializeObject(contentResult);

            if (jsonResult.access_token != null)
            {
                var jwtToken = (string)jsonResult.access_token;
                user = new User {Id = (string)jsonResult.id, Email = email, JwtToken = jwtToken};
            }

            return user;
        }

        public static void Logoff(HttpSessionStateBase session, HttpResponseBase response)
        {
            session.Abandon();

            FormsAuthentication.SignOut();

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(cookie);
        }

        public static User User
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return ((MyPrincipal)(HttpContext.Current.User)).User;
                }

                if (HttpContext.Current.Items.Contains("User"))
                {
                    return (User)HttpContext.Current.Items["User"];
                }

                return null;
            }
        }
    }
}