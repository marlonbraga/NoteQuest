using System.Collections;
using System.Collections.Generic;
using NoteQuest.Application.Interface;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Application
{
    public class EscolhaFacade : IEscolhaFacade
    {
        public IPortaEntrada PortaEntrada { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }
        public IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        public IVerificarPortaService VerificarPortaService { get; set; }
        public IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        public IAbrirFechaduraService AbrirFechaduraService { get; set; }
        public IQuebrarPortaService QuebrarPortaService { get; set; }
        public ISairDeMasmorraService SairDeMasmorraService { get; set; }
        private IDictionary<int, IEscolha> EscolhasDaRodada { get; set; }

        public EscolhaFacade(IPortaEntrada portaEntrada, IClasseBasicaRepository masmorraRepository, IEntrarEmMasmorraService entrarEmMasmorraService, IVerificarPortaService verificarPortaService, IEntrarPelaPortaService entrarPelaPortaService, IAbrirFechaduraService abrirFechaduraService, IQuebrarPortaService quebrarPortaService, ISairDeMasmorraService sairDeMasmorraService)
        {
            PortaEntrada = portaEntrada;
            MasmorraRepository = masmorraRepository;
            EntrarEmMasmorraService = entrarEmMasmorraService;
            VerificarPortaService = verificarPortaService;
            EntrarPelaPortaService = entrarPelaPortaService;
            AbrirFechaduraService = abrirFechaduraService;
            QuebrarPortaService = quebrarPortaService;
            SairDeMasmorraService = sairDeMasmorraService;
        }

        public ConsequenciaView SelecionaEscolha(int escolha, int? indice)
        {
            ConsequenciaDTO consequencia = EscolhasDaRodada[escolha].Acao.Executar();
            ConsequenciaView resultado = ConverteConsequencia(consequencia);
            EscolhasDaRodada = consequencia.EscolhasNumeradas;

            return resultado;
        }

        public ConsequenciaView SelecionaEscolha(IEscolha escolha)
        {
            ConsequenciaDTO consequencia = escolha.Acao.Executar();
            EscolhasDaRodada = consequencia.EscolhasNumeradas;
            ConsequenciaView resultado = ConverteConsequencia(consequencia);

            return resultado;
        }

        private ConsequenciaView ConverteConsequencia(ConsequenciaDTO consequencia)
        {
            ConsequenciaView resultado = new()
            {
                Descricao = consequencia.Descricao,
                Segmento = consequencia.Segmento,
                Escolhas = new Dictionary<int, EscolhaView>()
            };

            var key = 1;
            foreach (var escolha in consequencia.Escolhas)
            {
                EscolhaView value = new()
                {
                    Titulo = escolha.Acao.Titulo,
                    Descricao = escolha.Acao.Descricao,
                    AcaoTipo = escolha.Acao.AcaoTipo
                };
                resultado.Escolhas.Add(key, value);
                key++;
            }

            return resultado;
        }

        public ConsequenciaView EntrarEmMasmorra(IMasmorra masmorra)
        {
            EntrarEmMasmorraService.Build(masmorra);
            ConsequenciaDTO consequencia = EntrarEmMasmorraService.Executar();
            ConsequenciaView resultado = ConverteConsequencia(consequencia);
            EscolhasDaRodada = consequencia.EscolhasNumeradas;

            return resultado;
        }

        //public ConsequenciaView VerificarPorta(int indice, IPortaComum porta)
        //{
        //    IAcao acao = new VerificarPorta(indice, porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //}

        //public ConsequenciaView EntrarPelaPorta(IPortaComum porta, ISegmentoFactory segmentoFactory)
        //{
        //    IAcao acao = new EntrarPelaPorta(porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //}

        //public ConsequenciaView DestrancarPorta(int indice, IPortaComum porta)
        //{
        //    IAcao acao = new AbrirFechadura(indice, porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //    throw new NotImplementedException();
        //}

        //public ConsequenciaView QuebrarPorta(int indice, IPortaComum porta)
        //{
        //    IAcao acao = new QuebrarPorta(indice, porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //    throw new NotImplementedException();
        //}

        //public ConsequenciaView SairDeMasmorra()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
