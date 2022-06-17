using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public interface ISegmento
    {
        public int Nivel { get; }
        public int IdSegmento { get; set; }
        public static int ContagemDeSalas { get; set; }
        public string Descricao { get; set; }
        public string DetalhesDescricao { get; set; }
        public List<IPorta> Portas { get; set; }
        public List<IEscolha> Escolhas { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }

        public void Build(IPorta portaDeEntrada, string descricao, int qtdPortas);

        public void Build(string descricao, int qtdPortas);

        public List<IEscolha> RecuperaEscolhasDePortas();

        public List<IEscolha> RecuperaTodasAsEscolhas();
    }
}
