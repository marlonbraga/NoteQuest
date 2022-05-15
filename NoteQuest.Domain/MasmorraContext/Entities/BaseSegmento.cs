using NoteQuest.Domain.Core.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public enum SegmentoTipo
    {
        sala, corredor, escadaria
    }

    public abstract class BaseSegmento
    {
        public int Nivel { get; }
        public int IdSegmento { get; }
        public string Descricao { get; set; }
        public List<IPorta> Portas { get; set; }
        public List<IEscolha> Escolhas { get; set; }

        public BaseSegmento(IPorta portaDeEntrada, string descricao, int qtdPortas)
        {
            IPorta porta = portaDeEntrada;
            if (portaDeEntrada is IPortaComum)
            {
                porta = ((IPortaComum)portaDeEntrada).InvertePorta();
            }
            Portas = new() { porta };
            Descricao = descricao;
            Escolhas = GerarEscolhasBasicas();
            GerarPortas(qtdPortas);
        }

        public BaseSegmento(string descricao, int qtdPortas)
        {
            Portas = new();
            Descricao = descricao;
            Escolhas = GerarEscolhasBasicas();
            GerarPortas(qtdPortas);
        }

        public BaseSegmento Entrar(IPortaComum portaDeEntrada)
        {
            return this;
        }

        public void DesarmarArmadilhas(int valorD6)
        {

        }

        private List<IEscolha> GerarEscolhasBasicas()
        {
            IAcao acaoDesarmarArmadilhas = new DesarmarArmadilhas();
            Escolha desarmarArmadilhas = new(acaoDesarmarArmadilhas);
            IAcao acaoAcharPassagemSecreta = new AcharPassagemSecreta();
            Escolha acharPassagemSecreta = new(acaoAcharPassagemSecreta);
            List<IEscolha> escolhas = new() { desarmarArmadilhas, acharPassagemSecreta };
            return escolhas;
        }

        private void GerarPortas(int qtdPortas)
        {
            //IPortaComum porta = ;
            //TODO: elaborar regra de posição

            for (int i = 1; i <= qtdPortas; i++)
            {
                Portas.Add(new Porta(this, RecuperaPosicaoPorIndice(i)));
            }
        }

        private Posicao RecuperaPosicaoPorIndice(int indice)
        {
            switch (indice)
            {
                case 1: return Posicao.frente;
                case 2: return Posicao.direita;
                case 3: return Posicao.tras;
                case 4: return Posicao.esquerda;
                default: return Posicao.frente;
            }
        }

        public List<IEscolha> RecuperaEscolhasDePortas()
        {
            List<IEscolha> escolhas = new();
            foreach (var porta in Portas)
            {
                escolhas.AddRange(porta.Escolhas);
            }
            return escolhas;
        }

        public List<IEscolha> RecuperaTodasAsEscolhas()
        {
            List<IEscolha> escolhas = GerarEscolhasBasicas();
            escolhas.AddRange(RecuperaEscolhasDePortas());
            return escolhas;
        }
    }
}
