using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface ISegmentoBuilder
    {
        public Tuple<string, BaseSegmento> GeraSegmentoInicial();
        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice);
        public void Build(int D6 = 1);


        public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Posicao posicao);
        public IPortaEntrada CriarPortaDeEntrada(List<IEscolha> escolhas);



        public IEntrarPelaPortaService CriarEntrarPelaPortaService(IPortaComum portaComum);
        public IQuebrarPortaService CriarQuebrarPortaService(IPortaComum portaComum);
        public IAbrirFechaduraService CriarAbrirFechaduraService(IPortaComum portaComum);
        public IVerificarPortaService CriarVerificarPortaService(IPortaComum portaComum);

    }
}
