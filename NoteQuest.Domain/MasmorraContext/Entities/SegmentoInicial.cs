using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class SegmentoInicial : BaseSegmento, ISegmentoInicial
    {
        public new string Descricao { get; set; }
        public int QtdPortas { get; set; }
        public TabelaAPartirDe SegmentoSeguinte { get; set; }

        public SegmentoInicial(string descricao, int qtdPortas, TabelaAPartirDe segmentoSeguinte, ISegmentoBuilder segmentoBuilder) : base(segmentoBuilder)
        {
            this.Descricao = descricao;
            this.QtdPortas = qtdPortas;
            this.SegmentoSeguinte = segmentoSeguinte;
        }

        public void Build(ISegmentoBuilder segmentoBuilder)
        {
            IAcao acaoSairDeMasmorra = new SairDeMasmorraService();
            IEscolha sairDeMasmorra = new Escolha(acaoSairDeMasmorra, null);
            List<IEscolha> escolhasPortaDeEntrada = new() { sairDeMasmorra };
            IPorta portaDeEntrada = segmentoBuilder.CriarPortaDeEntrada(escolhasPortaDeEntrada);
            base.Passagens = new Dictionary<Direcao, Tuple<int, IPorta>>();
            base.Passagens.Add(Direcao.tras, new(5, portaDeEntrada));

            BuildSegmentoSeguinte(segmentoBuilder);

            base.SegmentoFactory = segmentoBuilder;
            base.Build(Descricao, QtdPortas);
        }

        private void BuildSegmentoSeguinte(ISegmentoBuilder segmentoBuilder)
        {
            IPortaComum porta;
            IAcao AcaoEntrarPelaPorta;
            IEscolha entrarPelaPorta;

            //TODO: Remover repetição de código
            porta = segmentoBuilder.CriarPortaComum(this, Direcao.frente);

            AcaoEntrarPelaPorta = new EntrarPelaPortaService(porta, segmentoBuilder);
            entrarPelaPorta = new Escolha(AcaoEntrarPelaPorta, null);
            porta = segmentoBuilder.CriarPortaComum(this, Direcao.frente, entrarPelaPorta, SegmentoSeguinte);

            AcaoEntrarPelaPorta = new EntrarPelaPortaService(porta, segmentoBuilder);
            entrarPelaPorta = new Escolha(AcaoEntrarPelaPorta, null);
            porta = segmentoBuilder.CriarPortaComum(this, Direcao.frente, entrarPelaPorta, SegmentoSeguinte);

            base.Passagens.Add(Direcao.frente, new(5, porta));
        }
    }
}
