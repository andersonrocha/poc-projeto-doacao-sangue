using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoacaoSangue.Api.Models;

namespace DoacaoSangue.Api.Repositorio
{
    public static class SolicitacaoBolsaRespositorio
    {
        private static ConcurrentBag<SolicitacaoBolsa> _solicitacoesBolsasList = new ConcurrentBag<SolicitacaoBolsa>();

        static SolicitacaoBolsaRespositorio()
        {
            _solicitacoesBolsasList.Add(new SolicitacaoBolsa
            {
                Id = 1,
                UnidadeHospitalar = UnidadeHospitalar.Hospital01,
                DataSolicitacao = DateTime.Now.AddDays(-3),
                NomePaciente = "João da Silva",
                TipoSanguineo = TipoSanguineo.ABNegativo,
                Motivo = "Cirurgia",
                Quantidade = 2,
                LaboratorioAtendeu = Laboratorio.Laboratorio01,
                DataAtendimento = DateTime.Now.AddHours(-45),
                IsAtendida = true
            });
            _solicitacoesBolsasList.Add(new SolicitacaoBolsa
            {
                Id = 2,
                UnidadeHospitalar = UnidadeHospitalar.Hospital02,
                DataSolicitacao = DateTime.Now.AddDays(-2),
                NomePaciente = "Benedita da Silva",
                TipoSanguineo = TipoSanguineo.ANegativo,
                Motivo = "Cirurgia coração",
                Quantidade = 4,
                LaboratorioAtendeu = Laboratorio.Laboratorio02,
                DataAtendimento = DateTime.Now.AddHours(-22),
                IsAtendida = true
            });
            _solicitacoesBolsasList.Add(new SolicitacaoBolsa
            {
                Id = 3,
                UnidadeHospitalar = UnidadeHospitalar.Hospital01,
                DataSolicitacao = DateTime.Now.AddDays(-1),
                NomePaciente = "José Antônio",
                TipoSanguineo = TipoSanguineo.BPositivo,
                Motivo = "Acidente",
                Quantidade = 2.5M
            });
            _solicitacoesBolsasList.Add(new SolicitacaoBolsa
            {
                Id = 4,
                UnidadeHospitalar = UnidadeHospitalar.Hospital02,
                DataSolicitacao = DateTime.Now.AddHours(-4),
                NomePaciente = "Marieta Joaquina",
                TipoSanguineo = TipoSanguineo.OPositivo,
                Motivo = "Doação de rim",
                Quantidade = 3.5M
            });
        }

        public static IEnumerable<SolicitacaoBolsa> ObterSolicitacoes(UnidadeHospitalar? unidadeHospitalar = null)
        {
            if (unidadeHospitalar.HasValue)
                return _solicitacoesBolsasList
                            .Where(sb => sb.UnidadeHospitalar == unidadeHospitalar.Value)
                            .OrderBy(x => x.DataSolicitacao);

            return _solicitacoesBolsasList.OrderBy(x => x.DataSolicitacao);
        }

        public static SolicitacaoBolsa ObterSolicitacao(int id, UnidadeHospitalar? unidadeHospitalar = null)
        {
            if (unidadeHospitalar.HasValue)
                return _solicitacoesBolsasList.FirstOrDefault(x => x.Id == id && x.UnidadeHospitalar == unidadeHospitalar.Value);

            return _solicitacoesBolsasList.FirstOrDefault(x => x.Id == id);
        }

        public static void AdicionarSolicitacao(SolicitacaoBolsa solicitacaoBolsa)
        {
            solicitacaoBolsa.Id = _solicitacoesBolsasList.Count + 1;
            solicitacaoBolsa.DataSolicitacao = DateTime.Now;

            _solicitacoesBolsasList.Add(solicitacaoBolsa);
        }

        public static bool AtenderSolicitacao(int idSolicitacao, Laboratorio laboratorio)
        {
            var solicitacao = _solicitacoesBolsasList.First(x => x.Id == idSolicitacao && !x.IsAtendida);

            if (solicitacao == null)
                return false;

            solicitacao.IsAtendida = true;
            solicitacao.DataAtendimento = DateTime.Now;
            solicitacao.LaboratorioAtendeu = laboratorio;

            return true;
        }
    }
}