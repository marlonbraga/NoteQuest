using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.DTO
{

    public partial class MasmorraDataDTO : IMasmorraData
    {
        public string Descricao { get; set; }
        public SegmentoInicial SegmentoInicial { get; set; }
        public TabelaSegmentos TabelaSegmentos { get; set; }
        public IDictionary<ushort, TabelaArmadilhaElement> TabelaPassagemSecreta { get; set; }
        public IDictionary<ushort, TabelaArmadilhaElement> TabelaArmadilha { get; set; }
        public IDictionary<ushort, TabelaConteudo> TabelaConteudo { get; set; }
        public IDictionary<ushort, TabelaMonstro> TabelaMonstro { get; set; }
        public TabelaRecompensa TabelaRecompensa { get; set; }
        public IDictionary<ushort, TabelaChefeDaMasmorra> TabelaChefeDaMasmorra { get; set; }
        public IDictionary<ushort, TabelaArmadura> TabelaArmadura { get; set; }
        public IDictionary<ushort, TabelaArma> TabelaArma { get; set; }
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
        public IDictionary<ushort, TabelaItemTesouro> TabelaTesouro { get; set; }
        public IDictionary<ushort, TabelaItemMaravilha> TabelaMaravilha { get; set; }
        public IDictionary<ushort, TabelaItemMagico> TabelaItemMágico { get; set; }
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
        public IDictionary<ushort, TabelaAPartirDe> TabelaAPartirDeEscadaria { get; set; }
        public IDictionary<ushort, TabelaAPartirDe> TabelaAPartirDeCorredor { get; set; }
        public IDictionary<ushort, TabelaAPartirDe> TabelaAPartirDeSala { get; set; }
    }

    public partial class TabelaAPartirDe
    {
        public SegmentoTipo Segmento { get; set; }
        public string Descricao { get; set; }
        public int QtdPortas { get; set; }
    }
}
