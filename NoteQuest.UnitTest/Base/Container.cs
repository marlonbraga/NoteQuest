using Ninject;
using NoteQuest.Application.Interface;
using NoteQuest.Application.Interfaces;
using NoteQuest.CLI.Interfaces;
using NoteQuest.CLI.IoC;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Dados;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Infrastructure.Data.Masmorra;

namespace NoteQuest.UnitTest.Base
{
    public class Container : IContainer
    {
        public IKernel Kernel { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }
        public IMasmorra Masmorra { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
        public IEscolhaFacade EscolhaFacade { get; set; }
        public IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        public IVerificarPortaService VerificarPortaService { get; set; }
        public IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        public IAbrirFechaduraService AbrirFechaduraService { get; set; }
        public IQuebrarPortaService QuebrarPortaService { get; set; }
        public ISairDeMasmorraService SairDeMasmorraService { get; set; }
        public IRacaRepository RacaRepository { get; set; }
        public IClasseRepository ClasseRepository { get; set; }
        public IPersonagemBuilder PersonagemBuilder { get; set; }
        public IPersonagemService PersonagemService { get; set; }

        public Container()
        {
            //TODO: MOCKEAR TUDO
            Kernel = Bootstrap.GetKernel();
            PortaEntrada = Kernel.Get<PortaEntrada>();
            MasmorraRepository = Kernel.Get<MasmorraRepository>();
            //SegmentoFactory = Kernel.Get<SegmentoFactory>();
        }
    }
}
