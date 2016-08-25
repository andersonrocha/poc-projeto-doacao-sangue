using System;
using System.Web;
using System.Web.Security;

namespace DoacaoSangue.Web.Infra
{
    public static class TicketHelper
    {
        public static HttpCookie CreateAuthCookie(string userName, string userData, bool persistent)
        {
            DateTime issued = DateTime.Now;
            int formsTimeout = FormsAuthentication.Timeout.Minutes;

            DateTime expiration = DateTime.Now.AddMinutes(formsTimeout);
            string cookiePath = FormsAuthentication.FormsCookiePath;

            var ticket = new FormsAuthenticationTicket(0, userName, issued, expiration, true, userData, cookiePath);
            return CreateAuthCookie(ticket, expiration, persistent);
        }

        public static HttpCookie CreateAuthCookie(FormsAuthenticationTicket ticket, DateTime expiration, bool persistent)
        {
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (persistent)
            {
                cookie.Expires = expiration;
            }

            return cookie;
        }
    }
}