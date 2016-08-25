using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DoacaoSangue.Api.Models;
using DoacaoSangue.Api.Repositorio;

namespace DoacaoSangue.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/solicitacaobolsa")]
    public class SolicitacaoBolsaController : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "Hospital")]
        [Route("")]
        public IHttpActionResult Post(SolicitacaoBolsa solicitacaoBolsa)
        {
            if (ModelState.IsValid)
            {
                SolicitacaoBolsaRespositorio.AdicionarSolicitacao(solicitacaoBolsa);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Authorize(Roles = "Laboratorio, Hospital")]
        [Route("")]
        public IEnumerable<SolicitacaoBolsa> Get()
        {
            if (User.IsInRole("Laboratorio"))
                return SolicitacaoBolsaRespositorio.ObterSolicitacoes();
            
            return SolicitacaoBolsaRespositorio.ObterSolicitacoes((UnidadeHospitalar)Enum.Parse(typeof(UnidadeHospitalar), IdUsuario));
        }

        [HttpGet]
        [Authorize(Roles = "Laboratorio, Hospital")]
        [Route("{id:int}")]
        public SolicitacaoBolsa Get(int id)
        {
            return SolicitacaoBolsaRespositorio.ObterSolicitacao(id);
        }

        [HttpPut]
        [Route("atender")]
        [Authorize(Roles = "Laboratorio")]
        public IHttpActionResult PutAtenderSolicitacao(AtenderSolicitacaoBolsaModel model)
        {
            if (ModelState.IsValid)
            {
                var result = SolicitacaoBolsaRespositorio.AtenderSolicitacao(model.Id, model.Laboratorio);

                if (result)
                    return Ok();

                return BadRequest("Solicitação não encontrada.");
            }

            return BadRequest(ModelState);
        }
    }
}