using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class SegmentoInicial : BaseSegmento, ISegmentoInicial
    {
        public new string Descricao { get; set; }
        public int QtdPortas { get; set; }

        public SegmentoInicial(string descricao, int qtdPortas, ISegmentoBuilder segmentoFactory) : base(segmentoFactory)
        {
            this.Descricao = descricao;
            this.QtdPortas = qtdPortas;
        }

        public void Build(ISegmentoBuilder segmentoFactory)
        {
            IAcao acaoSairDeMasmorra = new SairDeMasmorraService();
            IEscolha sairDeMasmorra = new Escolha(acaoSairDeMasmorra, null);
            List<IEscolha> escolhas = new() { sairDeMasmorra };
            IPorta portaDeEntrada = segmentoFactory.CriarPortaDeEntrada(escolhas);
            base.Passagens = new Dictionary<Direcao, Tuple<int, IPorta>>();
            base.Passagens.Add(Direcao.tras, new(5, portaDeEntrada));
            base.SegmentoFactory = segmentoFactory;
            base.Build(Descricao, QtdPortas);
        }
    }
}
