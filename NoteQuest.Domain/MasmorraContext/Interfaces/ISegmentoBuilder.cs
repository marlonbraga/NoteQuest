using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface ISegmentoBuilder
    {
        public Tuple<string, BaseSegmento> GeraSegmentoInicial();
        //public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice);
        public void Build(int D6 = 1);

        //public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Direcao direcao, IEscolha escolha, TabelaAPartirDe segmentoAlvo);
        //public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Direcao direcao, IEscolha escolha = null, BaseSegmento segmentoAlvo = null);
        //public IPortaEntrada CriarPortaDeEntrada(IList<IEscolha> escolhas);
        
        //public IEntrarPelaPortaService CriarEntrarPelaPortaService(IPortaComum portaComum);
        //public IQuebrarPortaService CriarQuebrarPortaService(IPortaComum portaComum);
        //public IAbrirFechaduraService CriarAbrirFechaduraService(IPortaComum portaComum);
        //public IVerificarPortaService CriarVerificarPortaService(IPortaComum portaComum);
    }
}
