using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using DoacaoSangue.Web.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace DoacaoSangue.Web.Infra.DoacaoSangueApi
{
    public class SolicitacaoBolsaApi
    {
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["ApiResourceServer"];
        private readonly string _token;

        public SolicitacaoBolsaApi(string token)
        {
            _token = token;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            client.Authenticator = new JwtAuthenticator(_token);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer.ContentType = "application/json; charset=utf-8";

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Erro ao obter a resposta. Confira detalhes para mais informações.";
                var apiException = new ApplicationException(message, response.ErrorException);
                throw apiException;
            }

            return response.Data;
        }

        public IEnumerable<SolicitacaoBolsaModel> GetSolicitacoes()
        {
            var request = new RestRequest();
            request.Resource = "solicitacaobolsa";
            request.RootElement = "SolicitacaoBolsaModel";

            return Execute<List<SolicitacaoBolsaModel>>(request);
        }

        public void AddSolicitacao(SolicitacaoBolsaModel solicitacao)
        {
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.Resource = "solicitacaobolsa";
            request.RootElement = "SolicitacaoBolsaModel";

            request.AddJsonBody(solicitacao);

            Execute<SolicitacaoBolsaModel>(request);
        }

        public void AtenderSolicitacao(int idSolicitacao, Laboratorio laboratorio)
        {
            var request = new RestRequest(Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.Resource = "solicitacaobolsa/{id}/atender";
            request.RootElement = "SolicitacaoBolsaModel";

            request.AddParameter("id", idSolicitacao, ParameterType.UrlSegment);
            request.AddJsonBody(new { laboratorio });

            Execute<SolicitacaoBolsaModel>(request);
        }   
    }
}