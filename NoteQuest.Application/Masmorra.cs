using Ninject;
using NoteQuest.Application.IoC;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Infrastructure.Data.Masmorra;
using System;
using NoteQuest.Domain.Core.DTO;

namespace NoteQuest.Application
{
    public class Masmorra
    {
        public IKernel Kernel { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        //public ISegmentoFactory SegmentoFactory { get; set; }
        public int Indice { get; set; }
        public Masmorra(int indice)
        {
            Kernel = Bootstrap.GetKernel();
            PortaEntrada = Kernel.Get<PortaEntrada>();
            MasmorraRepository = Kernel.Get<MasmorraRepository>();
            //SegmentoFactory = Kernel.Get<SegmentoFactory>();
            Indice = indice;
            SegmentoFactory.Instancia(MasmorraRepository, indice);
        }

        public ConsequenciaDTO EntrarEmMasmorra()
        {
            IAcao acao = new EntrarEmMasmorra(Indice, MasmorraRepository, /*SegmentoFactory,*/ PortaEntrada);
            ConsequenciaDTO consequencia = acao.Executar(); 
            return consequencia;
        }

        public ConsequenciaDTO VerificarPorta(int indice, IPortaComum porta)
        {
            IAcao acao = new VerificarPorta(indice, porta);
            ConsequenciaDTO consequencia = acao.Executar();
            return consequencia;
        }

        public ConsequenciaDTO EntrarPelaPorta(IPortaComum porta, ISegmentoFactory segmentoFactory)
        {
            IAcao acao = new EntrarPelaPorta(porta/*, segmentoFactory*/);
            ConsequenciaDTO consequencia = acao.Executar();
            return consequencia;
        }

        public ConsequenciaDTO DestrancarPorta(int indice, IPortaComum porta)
        {
            IAcao acao = new AbrirFechadura(indice, porta);
            ConsequenciaDTO consequencia = acao.Executar();
            return consequencia;
            throw new NotImplementedException();
        }

        public ConsequenciaDTO QuebrarPorta(int indice, IPortaComum porta)
        {
            IAcao acao = new QuebrarPorta(indice, porta);
            ConsequenciaDTO consequencia = acao.Executar();
            return consequencia;
            throw new NotImplementedException();
        }

        public ConsequenciaDTO SairDeMasmorra()
        {
            throw new NotImplementedException();
        }
    }
}
