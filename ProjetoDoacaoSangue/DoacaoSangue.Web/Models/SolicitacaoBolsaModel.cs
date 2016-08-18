using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoacaoSangue.Web.Models
{
    public class SolicitacaoBolsaModel
    {
        public int Id { get; set; }
        [Display(Name = "Data Solicitação")]
        public DateTime DataSolicitacao { get; set; }
        [Display(Name = "Unidade Hospitalar")]
        [Required]
        public UnidadeHospitalar UnidadeHospitalar { get; set; }
        [Display(Name = "Nome do Paciente")]
        [Required]
        public string NomePaciente { get; set; }
        [Display(Name = "Tipo Sanguíneo")]
        [Required]
        public TipoSanguineo TipoSanguineo { get; set; }
        [Required]
        public decimal Quantidade { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Display(Name = "Atendido?")]
        public bool IsAtendida { get; set; }
        [Display(Name = "Data Atendimento")]
        public DateTime DataAtendimento { get; set; }
        [Display(Name = "Laboratório que atendeu")]
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