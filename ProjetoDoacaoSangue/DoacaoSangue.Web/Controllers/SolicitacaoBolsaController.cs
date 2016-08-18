using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoacaoSangue.Web.Infra.DoacaoSangueApi;

namespace DoacaoSangue.Web.Controllers
{
    [Authorize]
    [RoutePrefix("solicitacaobolsa")]
    public class SolicitacaoBolsaController : BaseController
    {
        [Route("lista")]
        public ActionResult Index()
        {
            var solicitacaoApi = new SolicitacaoBolsaApi(AuthToken);
            var solicitacoes = solicitacaoApi.GetSolicitacoes();

            return View(solicitacoes);
        }
    }
}