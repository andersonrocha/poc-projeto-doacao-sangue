using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoacaoSangue.Api.Models
{
    public class AtenderSolicitacaoBolsaModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Laboratorio Laboratorio { get; set; }
    }
}