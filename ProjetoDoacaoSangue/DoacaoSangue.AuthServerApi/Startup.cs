using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using DoacaoSangue.AuthServerApi.Formats;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using DoacaoSangue.AuthServerApi.Providers;

namespace DoacaoSangue.AuthServerApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = (ConfigurationManager.AppSettings["Token:AllowInsecureHttp"] ?? "true") == "true",
                TokenEndpointPath = new PathString(ConfigurationManager.AppSettings["Token:EndPointPath"]),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["Token:Expiration"] ?? "30")),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["Token:AccessTokenFormat"])
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}