using System.Reflection;
using Ninject;
using Ninject.Modules;
using NoteQuest.Application;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces.Personagem.Data;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Infrastructure.Data.Masmorra;

namespace NoteQuest.CLI.IoC
{
    public static class Bootstrap
    {
        public static IKernel GetKernel(Bindings bindings = null)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            //kernel.Bind<IMasmorraRepository>().To<MasmorraRepository>();
            //kernel.Bind<ISegmentoFactory>().To<SegmentoFactory>();
            //kernel.Bind<IPortaEntrada>().To<PortaEntrada>();
            //kernel.Bind<IPortaComum>().To<PortaComum>();

            //Bindings
            if (bindings is null)
            {
                bindings = new();
            }
            //bindings.Load();

            return kernel;

            //IMasmorraRepository masmorraRepository = kernel.Get<IMasmorraRepository>();
        }
    }

    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IRacaRepository>().To<RacaRepository>();
            Bind<IClasseRepository>().To<ClasseRepository>();

            Bind<IPersonagemBuilder>().To<PersonagemBuilder>();
            Bind<IPersonagemService>().To<PersonagemService>();

            //Bind<IClasseBasicaRepository>().To<MasmorraRepository>();

            Bind<IMasmorraRepository>().To<MasmorraRepository>();
            Bind<IPortaEntrada>().To<PortaEntrada>();
            Bind<IPortaComum>().To<PortaComum>();
            //Bind<ISegmentoFactory>().To<SegmentoFactory>();

            //Bind<IEscolhaFacade>().To<EscolhaFacade>();
            //Bind<IEntrarEmMasmorraService>().To<EntrarEmMasmorraService>();
            //Bind<IVerificarPortaService>().To<VerificarPortaService>();
            //Bind<IEntrarPelaPortaService>().To<EntrarPelaPortaService>();
            //Bind<IAbrirFechaduraService>().To<AbrirFechaduraService>();
            //Bind<IQuebrarPortaService>().To<QuebrarPortaService>();
            //Bind<ISairDeMasmorraService>().To<SairDeMasmorraService>();

            Bind<IMasmorra>().To<Masmorra>();
        }
    }
}
