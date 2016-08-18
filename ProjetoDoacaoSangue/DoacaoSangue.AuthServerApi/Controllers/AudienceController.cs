using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DoacaoSangue.AuthServerApi.Entidade;
using DoacaoSangue.AuthServerApi.Models;
using DoacaoSangue.AuthServerApi.Repositorio;

namespace DoacaoSangue.AuthServerApi.Controllers
{
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Audience newAudience = AudienceRepositorio.AddAudience(audienceModel.Name);

            return Ok<Audience>(newAudience);
        }
    }
}