using Ninject;
using NoteQuest.Application;
using NoteQuest.Application.Interface;
using NoteQuest.CLI.Interfaces;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Factories;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Infrastructure.Data.Masmorra;

namespace NoteQuest.CLI.IoC
{
    public class Container : IContainer
    {
        public IKernel Kernel { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IPortaComum Porta { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
        public ISegmentoInicial SegmentoInicial { get; set; }
        public IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        public IVerificarPortaService VerificarPortaService { get; set; }
        public IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        public IAbrirFechaduraService AbrirFechaduraService { get; set; }
        public IQuebrarPortaService QuebrarPortaService { get; set; }
        public ISairDeMasmorraService SairDeMasmorraService { get; set; }
        public IEscolhaFacade EscolhaFacade { get; set; }

        public Container()
        {
            Kernel = Bootstrap.GetKernel();
            PortaEntrada = Kernel.Get<PortaEntrada>();

            //SegmentoInicial = Kernel.Get<SegmentoInicial>();
            SegmentoFactory = Kernel.Get<SegmentoBuilder>();
            MasmorraRepository = Kernel.Get<MasmorraRepository>();

            Porta = Kernel.Get<PortaComum>();
            EntrarEmMasmorraService = Kernel.Get<EntrarEmMasmorraService>();
            VerificarPortaService = Kernel.Get<VerificarPortaService>();
            EntrarPelaPortaService = Kernel.Get<EntrarPelaPortaService>();
            AbrirFechaduraService = Kernel.Get<AbrirFechaduraService>();
            QuebrarPortaService = Kernel.Get<QuebrarPortaService>();
            SairDeMasmorraService = Kernel.Get<SairDeMasmorraService>();
            EscolhaFacade = Kernel.Get<EscolhaFacade>();

            Masmorra = Kernel.Get<Masmorra>();
        }
    }
}
