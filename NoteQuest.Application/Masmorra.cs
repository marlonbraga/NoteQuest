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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Application
{
    public class Masmorra
    {
        public IAcao EntrarEmMasmorra(int indice)
        {
            IKernel kernel = Bootstrap.GetKernel();
            IPortaEntrada portaEntrada = kernel.Get<PortaEntrada>();
            IMasmorraRepository masmorraRepository = kernel.Get<MasmorraRepository>();
            ISegmentoFactory segmentoFactory = kernel.Get<SegmentoFactory>();
            IMasmorraData masmorraData = kernel.Get<MasmorraData>();
            IAcao acao = new EntrarEmMasmorra(indice, masmorraRepository, segmentoFactory, portaEntrada, masmorraData);
            
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
