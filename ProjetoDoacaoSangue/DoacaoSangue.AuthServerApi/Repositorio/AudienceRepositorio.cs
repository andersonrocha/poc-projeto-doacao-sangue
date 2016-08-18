using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using DoacaoSangue.AuthServerApi.Entidade;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace DoacaoSangue.AuthServerApi.Repositorio
{
    /// <summary>
    /// Classe responsável por adicionar e buscar espectadores que podem requisitar tokens de autenticação.
    /// Por se tratar de uma implementação de protótipo é utilizado um espectador fake em memória.
    /// No caso de uma implementação em produção, deve ser implementado a persistência dos cadastros de espectadores.
    /// </summary>
    public static class AudienceRepositorio
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudienceRepositorio()
        {
            AudiencesList.TryAdd(ConfigurationManager.AppSettings["AudienceTest:ClientId"],
                                new Audience
                                {
                                    ClientId = ConfigurationManager.AppSettings["AudienceTest:ClientId"],
                                    Base64Secret = ConfigurationManager.AppSettings["AudienceTest:Base64Secret"],
                                    Name = "Doação de Sangue API"
                                });
        }

        /// <summary>
        /// Adiciona um novo espectador para requisitar token, gerando o clientId e a key em Base 64.
        /// </summary>
        /// <param name="name">O nome do espectador.</param>
        /// <returns>Retorna dados do espectador cadastrado.</returns>
        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        /// <summary>
        /// Obtem dados de um espectador.
        /// </summary>
        /// <param name="clientId">ClientId do espectador</param>
        /// <returns>Dados do espectador</returns>
        public static Audience GetAudience(string clientId)
        {
            Audience audience = null;

            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }

            return null;
        }
    }
}