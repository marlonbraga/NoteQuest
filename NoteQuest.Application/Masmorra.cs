using Ninject;
using NoteQuest.Application.IoC;
using NoteQuest.Domain.Core.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Infrastructure.Data.Masmorra;
using System;

namespace NoteQuest.Application
{
    public class Masmorra
    {
        public IKernel Kernel { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public ISegmentoFactory SegmentoFactory { get; set; }

        public Masmorra()
        {
            Kernel = Bootstrap.GetKernel();
            PortaEntrada = Kernel.Get<PortaEntrada>();
            MasmorraRepository = Kernel.Get<MasmorraRepository>();
            SegmentoFactory = Kernel.Get<SegmentoFactory>();
        }

        public IAcao EntrarEmMasmorra(int indice)
        {
            IAcao acao = new EntrarEmMasmorra(indice, MasmorraRepository, SegmentoFactory, PortaEntrada);
            return acao;
        }

        public void SairDeMasmorra()
        {
            throw new NotImplementedException();
        }

        public IPortaComum DestrancarPorta()
        {
            throw new NotImplementedException();
        }

        public void VerificarPorta()
        {

        }

        public BaseSegmento VerificarSala()
        {
            throw new NotImplementedException();
        }

        public BaseSegmento QuebrarPorta()
        {
            throw new NotImplementedException();
        }

        public BaseSegmento PassarPelaPorta(IPortaComum porta)
        {
            throw new NotImplementedException();
        }
    }
}
