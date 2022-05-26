﻿using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class PortaComum : IPortaComum
    {
        public int IdPorta { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Posicao Posicao { get; set; }
        public BaseSegmento SegmentoAlvo { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
        public List<IEscolha> Escolhas { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }

        public PortaComum(IMasmorraRepository masmorraRepository, ISegmentoBuilder segmentoFactory)
        {
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
            Escolhas = new List<IEscolha>();
        }

        public void Build(BaseSegmento segmentoAtual, Posicao posicao)
        {
            SegmentoAtual = segmentoAtual;
            Posicao = posicao;
            IAcao acao = SegmentoFactory.CriarVerificarPortaService(this);
            Escolha escolha = new(acao);
            Escolhas = new List<IEscolha>() { escolha };
        }

        public EstadoDePorta VerificarFechadura(int valorD6)
        {
            switch (valorD6)
            {
                case 1:
                    Escolhas = AbrirPorta();
                    EstadoDePorta = EstadoDePorta.aberta;
                    //TODO: Gera evento de cair em armadilha
                    break;
                case 2:
                case 3:
                    EstadoDePorta = EstadoDePorta.fechada;
                    IAcao acaoQuebrarPorta = SegmentoFactory.CriarQuebrarPortaService(this);
                    Escolha escolhaQuebrarPorta = new(acaoQuebrarPorta);
                    IAcao acaoAbrirFechadura = SegmentoFactory.CriarAbrirFechaduraService(this);
                    Escolha escolhaAbrirFechadura = new(acaoAbrirFechadura);
                    Escolhas = new List<IEscolha>() { escolhaQuebrarPorta, escolhaAbrirFechadura };
                    break;
                case 4:
                case 5:
                case 6:
                    Escolhas = AbrirPorta();
                    EstadoDePorta = EstadoDePorta.aberta;
                    break;
            }
            return EstadoDePorta;
        }

        public void AbrirFechadura()
        {
            EstadoDePorta = EstadoDePorta.aberta;
            IAcao acao = SegmentoFactory.CriarEntrarPelaPortaService(this);
            Escolha escolha = new(acao);
            Escolhas = new List<IEscolha>() { escolha };
        }

        public void QuebrarPorta()
        {
            EstadoDePorta = EstadoDePorta.quebrada;
            Escolhas = AbrirPorta();
        }

        public IPortaComum InvertePorta()
        {
            IPortaComum porta = new PortaComum(MasmorraRepository, SegmentoFactory)
            {
                Posicao = this.Posicao,//TODO: Inverter posição ←┘ ←Ꝋ
                EstadoDePorta = this.EstadoDePorta,
                SegmentoAlvo = this.SegmentoAtual,
                SegmentoAtual = this.SegmentoAlvo
            };
            IAcao acao = SegmentoFactory.CriarEntrarPelaPortaService(porta);
            acao.Titulo = "▲ " + acao.Titulo;
            IEscolha escolha = new Escolha(acao);
            List<IEscolha> escolhas = new() { escolha };
            porta.Escolhas = escolhas;

            return porta;
        }

        private List<IEscolha> AbrirPorta()
        {
            IAcao acao = SegmentoFactory.CriarEntrarPelaPortaService(this);
            Escolha escolha = new(acao);
            return new List<IEscolha>() { escolha };
        }
    }
}