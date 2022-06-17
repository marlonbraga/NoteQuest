using NoteQuest.Application.Interface;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;

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

        public ConsequenciaDTO SelecionaEscolha(IEscolha escolha)
        {
            return escolha.Acao.Executar();
        }

        public ConsequenciaDTO SelecionaEscolha(IEscolha escolha, int indice)
        {
            return escolha.Acao.Executar();
        }

        public ConsequenciaDTO EntrarEmMasmorra(IMasmorra masmorra)
        {
            EntrarEmMasmorraService.Build(masmorra);
            ConsequenciaDTO consequencia = EntrarEmMasmorraService.Executar();
            return consequencia;
        }

        //public ConsequenciaDTO VerificarPorta(int indice, IPortaComum porta)
        //{
        //    IAcao acao = new VerificarPorta(indice, porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //}

        //public ConsequenciaDTO EntrarPelaPorta(IPortaComum porta, ISegmentoFactory segmentoFactory)
        //{
        //    IAcao acao = new EntrarPelaPorta(porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //}

        //public ConsequenciaDTO DestrancarPorta(int indice, IPortaComum porta)
        //{
        //    IAcao acao = new AbrirFechadura(indice, porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //    throw new NotImplementedException();
        //}

        //public ConsequenciaDTO QuebrarPorta(int indice, IPortaComum porta)
        //{
        //    IAcao acao = new QuebrarPorta(indice, porta);
        //    ConsequenciaDTO consequencia = acao.Executar();
        //    return consequencia;
        //    throw new NotImplementedException();
        //}

        //public ConsequenciaDTO SairDeMasmorra()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
