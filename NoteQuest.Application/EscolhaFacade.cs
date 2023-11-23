using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Application
{
    public class EscolhaFacade
    {
        public IContainer Container { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }

        public EscolhaFacade(IContainer container)
        {
            Container = container;
            //PortaEntrada = Container.PortaEntrada;
            //MasmorraRepository = Container.MasmorraRepository;
        }

        public ConsequenciaDTO SelecionaEscolha(IEscolha escolha)
        {
            return escolha.Acao.Executar();
        }

        public ConsequenciaDTO SelecionaEscolha(IEscolha escolha, int indice)
        {
            return escolha.Acao.Executar();
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
