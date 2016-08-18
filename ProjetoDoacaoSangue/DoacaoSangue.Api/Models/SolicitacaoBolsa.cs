using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoacaoSangue.Api.Models
{
    public class SolicitacaoBolsa
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        [Required]
        public UnidadeHospitalar UnidadeHospitalar { get; set; }
        [Required]
        public string NomePaciente { get; set; }
        [Required]
        public TipoSanguineo TipoSanguineo { get; set; }
        [Required]
        public decimal Quantidade { get; set; }
        [Required]
        public string Motivo { get; set; }
        public bool IsAtendida { get; set; }
        public DateTime DataAtendimento { get; set; }
        public Laboratorio LaboratorioAtendeu { get; set; }
    }

    public enum TipoSanguineo
    {
        APositivo,
        ANegativo,
        BPositivo,
        BNegativo,
        ABPositivo,
        ABNegativo,
        OPositivo,
        ONegativo,
    }

    public enum UnidadeHospitalar
    {
        Hospital01,
        Hospital02
    }

    public enum Laboratorio
    {
        Laboratorio01,
        Laboratorio02
    }
}