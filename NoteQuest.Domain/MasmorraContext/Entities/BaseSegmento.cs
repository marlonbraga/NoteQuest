using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;
using System;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public enum SegmentoTipo
    {
        sala, corredor, escadaria
    }

    public abstract class BaseSegmento
    {
        public int IdSegmento { get; }
        public static int ContagemDeSalas { get; set; }
        public string Descricao { get; set; }
        public string DetalhesDescricao { get; set; }
        public List<IPorta> Portas { get; set; }
        public List<IEscolha> Escolhas { get; set; }
        public IMasmorra Masmorra { get; set; }
        public int Andar { get; set; }

        public BaseSegmento(IPorta portaDeEntrada, string descricao, int qtdPortas)
        {
            IdSegmento = ContagemDeSalas++;
            Masmorra = portaDeEntrada.Masmorra;
            Andar = portaDeEntrada.Andar;
            Masmorra.QtdPortasInexploradas += qtdPortas;
            IPorta porta = null;
            if (portaDeEntrada is IPortaComum)
            {
                porta = ((IPortaComum)portaDeEntrada).InvertePorta();
                porta.SegmentoAtual = this;
            }
            Portas = new() { porta };
            Descricao = descricao;
            DetalhesDescricao = string.Empty;
            Escolhas = GerarEscolhasBasicas();
            GerarPortas(qtdPortas);
        }

        public BaseSegmento(string descricao, int qtdPortas)
        {
            ContagemDeSalas = 0;
            IdSegmento = ContagemDeSalas++;
            Portas = new();
            Descricao = descricao;
            Escolhas = GerarEscolhasBasicas();
            GerarPortas(qtdPortas);
        }

        public void DesarmarArmadilhas(int valorD6)
        {

        }

        private List<IEscolha> GerarEscolhasBasicas()
        {
            IEvent acaoDesarmarArmadilhas = new DesarmarArmadilhas();
            Escolha desarmarArmadilhas = new (acaoDesarmarArmadilhas);
            IEvent acaoAcharPassagemSecreta = new AcharPassagemSecreta();
            Escolha acharPassagemSecreta = new (acaoAcharPassagemSecreta);
            List<IEscolha> escolhas = new() { desarmarArmadilhas, acharPassagemSecreta };
            return escolhas;
        }

        private void GerarPortas(int qtdPortas)
        {
            //IPortaComum porta = ;
            Random random = new ();
            int i = 1;
            while(i <= qtdPortas)
            {
                int indice = random.Next(1, 5);
                Posicao posicaoDePorta = RecuperaPosicaoPorIndice(indice);
                IPorta portaExistente = Portas.Find(x => x.Posicao == posicaoDePorta);
                if (portaExistente is not null)
                    continue;
                Portas.Add(new PortaComum(this, posicaoDePorta));
                i++;
            }

            Portas = ReordenarPortas(Portas);
        }

        private Posicao RecuperaPosicaoPorIndice(int indice)
        {
            switch (indice)
            {
                case 1: return Posicao.frente;
                case 2: return Posicao.esquerda; 
                case 3: return Posicao.direita;
                case 4: return Posicao.tras;
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
            //TODO: Adicionar demais escolhas em segmento
            //List<IEscolha> escolhas = GerarEscolhasBasicas();
            List<IEscolha> escolhas = new();
            escolhas.AddRange(RecuperaEscolhasDePortas());
            return escolhas;
        }
        protected List<IPorta> SubstituirPorta(List<IPorta> portas, IPorta novaPorta)
        {
            int index = portas.FindIndex(s => s.Posicao == novaPorta.Posicao);

            if (index != -1)
                portas[index] = novaPorta;

            return portas;
        }
        protected List<IPorta> ReordenarPortas(List<IPorta> portas)
        {
            List<IPorta> novaLista = new(portas.Count);

            for (int i = 0; i < 4; i++)
            {
                int index = portas.FindIndex(s => s.Posicao == (Posicao)i);
                if (index >= 0)
                    novaLista.Add(portas[index]);
            }

            portas = novaLista;
            return portas;
        }
    }
}
