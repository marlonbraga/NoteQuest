using Ninject;
using Ninject.Modules;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Infrastructure.Data.Masmorra;

namespace NoteQuest.Application.IoC
{
    public static class Bootstrap
    {
        public static IKernel GetKernel(Bindings bindings = null)
        {
            IKernel kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IMasmorraRepository>().To<MasmorraRepository>();
            //kernel.Bind<ISegmentoFactory>().To<SegmentoFactory>();
            kernel.Bind<IPortaEntrada>().To<PortaEntrada>();
            kernel.Bind<IPortaComum>().To<Porta>();

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
            Bind<IMasmorraRepository>().To<MasmorraRepository>();
            //Bind<ISegmentoFactory>().To<SegmentoFactory>();
            Bind<IPortaEntrada>().To<PortaEntrada>();
            Bind<IPortaComum>().To<Porta>();
        }
    }
}
