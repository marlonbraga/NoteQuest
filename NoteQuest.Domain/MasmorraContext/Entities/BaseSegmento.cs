using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public enum SegmentoTipo
    {
        sala, corredor, escadaria
    }

    public abstract class BaseSegmento : ISegmento
    {
        public int Nivel { get; }
        public int IdSegmento { get; set; }
        public static int ContagemDeSalas { get; set; }
        public string Descricao { get; set; }
        public string DetalhesDescricao { get; set; }
        public List<IPorta> Portas { get; set; }
        public List<IEscolha> Escolhas { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }

        public BaseSegmento(ISegmentoBuilder segmentoFactory)
        {
            SegmentoFactory = segmentoFactory;
        }

        public void Build(IPorta portaDeEntrada, string descricao, int qtdPortas)
        {
            IdSegmento = ContagemDeSalas++;
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

        public void Build(string descricao, int qtdPortas)
        {
            ContagemDeSalas = 0;
            IdSegmento = ContagemDeSalas++;
            Descricao = descricao;
            Escolhas = GerarEscolhasBasicas();
            GerarPortas(qtdPortas);
        }

        public void DesarmarArmadilhas(int valorD6)
        {

        }

        private List<IEscolha> GerarEscolhasBasicas()
        {
            IAcao acaoDesarmarArmadilhas = new DesarmarArmadilhasService();
            Escolha desarmarArmadilhas = new(acaoDesarmarArmadilhas, null);
            IAcao acaoAcharPassagemSecreta = new AcharPassagemSecretaService();
            Escolha acharPassagemSecreta = new(acaoAcharPassagemSecreta, null);
            List<IEscolha> escolhas = new() { desarmarArmadilhas, acharPassagemSecreta };
            return escolhas;
        }

        private void GerarPortas(int qtdPortas)
        {
            //TODO: elaborar regra de posição
            for (int i = 1; i <= qtdPortas; i++)
            {
                IPortaComum porta = SegmentoFactory.CriarPortaComum(this, RecuperaPosicaoPorIndice(i));
                porta.Build(this, RecuperaPosicaoPorIndice(i));
                Portas.Add(porta);
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
            //TODO: Adicionar demais escolhas em segmento
            //List<IEscolha> escolhas = GerarEscolhasBasicas();
            List<IEscolha> escolhas = new();
            escolhas.AddRange(RecuperaEscolhasDePortas());
            return escolhas;
        }
    }
}
