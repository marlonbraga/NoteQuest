using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public enum Direcao
    {
        frente, direita, tras, esquerda
    }

    public interface ISegmento
    {
        public int Nivel { get; }
        public int IdSegmento { get; set; }
        public static int ContagemDeSalas { get; set; }
        public string Descricao { get; set; }
        public string DetalhesDescricao { get; set; }
        public Tuple<int, int> Dimensoes { get; set; }
        public IDictionary<Direcao,Tuple<int, IPorta>> Passagens { get; set; }
        public IList<IEscolha> Escolhas { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }

        public void Build(IPorta portaDeEntrada, string descricao, int qtdPortas);

        public void Build(string descricao, int qtdPortas);

        public IList<IEscolha> RecuperaEscolhasDePortas();

        public IList<IEscolha> RecuperaTodasAsEscolhas();
    }
}
