using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoacaoSangue.Web.Infra.DoacaoSangueApi;
using DoacaoSangue.Web.Models;

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

            ViewBag.NomeHospital = CurrentUser.User.Id.ToLower().Contains("hospital") ? CurrentUser.User.Id : null;

            return View(solicitacoes);
        }

        [Route("nova")]
        public ActionResult NovaSolicitacao()
        {
            return View(new SolicitacaoBolsaModel());
        }

        [Route("nova")]
        [HttpPost]
        public ActionResult NovaSolicitacao(SolicitacaoBolsaModel model)
        {
            if (ModelState.IsValid)
            {
                var solicitacaoApi = new SolicitacaoBolsaApi(AuthToken);
                solicitacaoApi.AddSolicitacao(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Route("{id}")]
        public ActionResult Visualizar(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            var solicitacaoApi = new SolicitacaoBolsaApi(AuthToken);
            var solicitacao = solicitacaoApi.GetSolicitacao(id);

            if (solicitacao == null)
                return HttpNotFound();

            ViewBag.NomeHospital = CurrentUser.User.Id.ToLower().Contains("hospital") ? CurrentUser.User.Id : null;

            return View(solicitacao);
        }


        [Route("atender")]
        [HttpPost]
        public ActionResult Atender(int id)
        {
            var solicitacaoApi = new SolicitacaoBolsaApi(AuthToken);
            solicitacaoApi.AtenderSolicitacao(id, (Laboratorio)Enum.Parse(typeof(Laboratorio), CurrentUser.User.Id));

            TempData["MsgOk"] = "Solicitação atendida com sucesso!";

            return Json("ok");
        }
    }
}