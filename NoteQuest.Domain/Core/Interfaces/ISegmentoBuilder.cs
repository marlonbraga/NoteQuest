using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.DTO;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface ISegmentoBuilder
    {
        public Tuple<string, BaseSegmento> GeraSegmentoInicial();
        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, ushort indice);
        public void Build(ushort D6 = 1);

        public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Direcao direcao, IEscolha escolha, TabelaAPartirDe segmentoAlvo);
        public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Direcao direcao, IEscolha escolha = null, BaseSegmento segmentoAlvo = null);
        public IPortaEntrada CriarPortaDeEntrada(IList<IEscolha> escolhas);



        public IEntrarPelaPortaService CriarEntrarPelaPortaService(IPortaComum portaComum);
        public IQuebrarPortaService CriarQuebrarPortaService(IPortaComum portaComum);
        public IAbrirFechaduraService CriarAbrirFechaduraService(IPortaComum portaComum);
        public IVerificarPortaService CriarVerificarPortaService(IPortaComum portaComum);

    }
}
