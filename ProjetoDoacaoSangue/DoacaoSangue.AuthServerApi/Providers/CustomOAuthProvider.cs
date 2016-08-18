using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DoacaoSangue.AuthServerApi.Repositorio;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace DoacaoSangue.AuthServerApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_id não informado");
                return Task.FromResult<object>(null);
            }

            var audience = AudienceRepositorio.GetAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", $"client_id '{context.ClientId}' inválido.");
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Por ser um protótipo, faz uma checagem simples em lista de usuário em memória.
            //Em uma implementação real deve ser feita implementação mais complexa.
            var usuario = UsuarioRepositorio.GetUsuario(context.UserName, context.ClientId);

            if (usuario == null || usuario.Senha != context.Password)
            {
                context.SetError("invalid_grant", "Usuário ou senha incorreto.");

                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Email));
            identity.AddClaim(new Claim("sub", usuario.Email));
            identity.AddClaim(new Claim("id", usuario.Id));
            identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Roles));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", context.ClientId ?? string.Empty
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }
    }
}