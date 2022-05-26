using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NoteQuest.Application.Interface;
using NoteQuest.CLI.Interfaces;
using NoteQuest.CLI.IoC;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using NoteQuest.Infrastructure.Data.Masmorra;

namespace NoteQuest.UnitTest.Base
{
    public class Container : IContainer
    {
        public IKernel Kernel { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IMasmorra Masmorra { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
        public IEscolhaFacade EscolhaFacade { get; set; }
        public IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        public IVerificarPortaService VerificarPortaService { get; set; }
        public IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        public IAbrirFechaduraService AbrirFechaduraService { get; set; }
        public IQuebrarPortaService QuebrarPortaService { get; set; }
        public ISairDeMasmorraService SairDeMasmorraService { get; set; }

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
