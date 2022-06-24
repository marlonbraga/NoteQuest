﻿using Ninject;
using Ninject.Modules;
using NoteQuest.Application;
using NoteQuest.Application.Interface;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Factories;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Infrastructure.Data.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Dados;
using System.Reflection;
using NoteQuest.Application.Interfaces;
using NoteQuest.Infrastructure.Data.Core;
using NoteQuest.Domain.Core.Services;

namespace NoteQuest.CLI.IoC
{
    public static class Bootstrap
    {
        public static IKernel GetKernel(Bindings bindings = null)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            //Bindings
            if (bindings is null)
            {
                bindings = new();
            }
            //bindings.Load();

            return kernel;
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

            Bind<IClasseBasicaRepository>().To<MasmorraRepository>();
            Bind<IPortaEntrada>().To<PortaEntrada>();
            Bind<IPortaComum>().To<PortaComum>();

            Bind<ISegmentoBuilder>().To<SegmentoBuilder>();
            Bind<ISegmentoInicial>().To<SegmentoInicial>();
            //Bind<ISegmentoFactory>().To<SegmentoFactory>().InSingletonScope();
            //Bind<SegmentoFactory>().ToSelf().InSingletonScope();

            Bind<IEscolhaFacade>().To<EscolhaFacade>();
            Bind<IEntrarEmMasmorraService>().To<EntrarEmMasmorraService>();
            Bind<IVerificarPortaService>().To<VerificarPortaService>();
            Bind<IEntrarPelaPortaService>().To<EntrarPelaPortaService>();
            Bind<IAbrirFechaduraService>().To<AbrirFechaduraService>();
            Bind<IQuebrarPortaService>().To<QuebrarPortaService>();
            Bind<ISairDeMasmorraService>().To<SairDeMasmorraService>();

            Bind<IMasmorra>().To<Masmorra>();
        }
    }
}
