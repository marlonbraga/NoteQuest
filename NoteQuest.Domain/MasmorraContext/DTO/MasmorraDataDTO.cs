using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.DTO
{

    public partial class MasmorraDataDTO : IMasmorraData
    {
        public string Descricao { get; set; }
        public SegmentoInicial SegmentoInicial { get; set; }
        public TabelaSegmentos TabelaSegmentos { get; set; }
        public TabelaArmadilhaElement[] TabelaPassagemSecreta { get; set; }
        public TabelaArmadilhaElement[] TabelaArmadilha { get; set; }
        public TabelaConteudo[] TabelaConteudo { get; set; }
        public TabelaMonstro[] TabelaMonstro { get; set; }
        public TabelaRecompensa TabelaRecompensa { get; set; }
        public TabelaChefeDaMasmorra[] TabelaChefeDaMasmorra { get; set; }
        public TabelaArmadura[] TabelaArmadura { get; set; }
        public TabelaArma[] TabelaArma { get; set; }
    }

    public partial class TabelaArma
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Dano { get; set; }
        public string Caracteristicas { get; set; }
    }

    public partial class TabelaArmadilhaElement
    {
        public int Indice { get; set; }
        public string Efeito { get; set; }
        public string Descricao { get; set; }
    }

    public partial class TabelaArmadura
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public long Pvs { get; set; }
    }

    public partial class TabelaChefeDaMasmorra
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public int Qtd { get; set; }
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int Pvs { get; set; }
    }

    public partial class TabelaConteudo
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public bool? PassagemSecreta { get; set; }
        public string Moedas { get; set; }
        public string Pergaminho { get; set; }
        public long Baú { get; set; }
        public string ItensMagico { get; set; }
    }

    public partial class TabelaMonstro
    {
        public int Indice { get; set; }
        public string Qtd { get; set; }
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int Pvs { get; set; }
        public string Caracteristicas { get; set; }
    }

    public partial class TabelaRecompensa
    {
        public TabelaItemTesouro[] TabelaTesouro { get; set; }
        public TabelaItemMaravilha[] TabelaMaravilha { get; set; }
        public TabelaItemMagico[] TabelaItemMágico { get; set; }
    }

    public partial class TabelaItemTesouro
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }
    public partial class TabelaItemMaravilha
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }
    public partial class TabelaItemMagico
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }

    public partial class TabelaSegmentos
    {
        public TabelaAPartirDe[] TabelaAPartirDeEscadaria { get; set; }
        public TabelaAPartirDe[] TabelaAPartirDeCorredor { get; set; }
        public TabelaAPartirDe[] TabelaAPartirDeSala { get; set; }
    }

    public partial class TabelaAPartirDe
    {
        public int Indice { get; set; }
        public SegmentoTipo Segmento { get; set; }
        public string Descricao { get; set; }
        public int QtdPortas { get; set; }
    }
}
