using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using DoacaoSangue.AuthServerApi.Entidade;

namespace DoacaoSangue.AuthServerApi.Repositorio
{
    public static class UsuarioRepositorio
    {
        public static ConcurrentDictionary<string, Usuario> UsuariosList = new ConcurrentDictionary<string, Usuario>();

        static UsuarioRepositorio()
        {
            UsuariosList.TryAdd("laboratorio01@doacaosangue.com",
                                new Usuario()
                                {
                                    ClientId = ConfigurationManager.AppSettings["AudienceTest:ClientId"],
                                    Id = "Laboratorio01",
                                    Email = "laboratorio01@doacaosangue.com",
                                    Senha = "laboratorio01",
                                    Roles = "Laboratorio"
                                });
            UsuariosList.TryAdd("laboratorio02@doacaosangue.com",
                                new Usuario()
                                {
                                    ClientId = ConfigurationManager.AppSettings["AudienceTest:ClientId"],
                                    Id = "Laboratorio02",
                                    Email = "laboratorio01@doacaosangue.com",
                                    Senha = "laboratorio01",
                                    Roles = "Laboratorio"
                                });
            UsuariosList.TryAdd("hospital01@doacaosangue.com",
                                new Usuario()
                                {
                                    ClientId = ConfigurationManager.AppSettings["AudienceTest:ClientId"],
                                    Id = "Hospital01",
                                    Email = "hospital01@doacaosangue.com",
                                    Senha = "hospital01",
                                    Roles = "Hospital"
                                });
            UsuariosList.TryAdd("hospital02@doacaosangue.com",
                                new Usuario()
                                {
                                    ClientId = ConfigurationManager.AppSettings["AudienceTest:ClientId"],
                                    Id = "Hospital02",
                                    Email = "hospital02@doacaosangue.com",
                                    Senha = "hospital02",
                                    Roles = "Hospital"
                                });
        }

        public static Usuario GetUsuario(string email, string clientId)
        {
            Usuario usuario = null;

            if (UsuariosList.TryGetValue(email, out usuario))
            {
                return usuario;
            }

            return null;
        }
    }
}