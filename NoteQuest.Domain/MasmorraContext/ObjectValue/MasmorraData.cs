using NoteQuest.Domain.CombateContext.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.ObjectValue
{
    public partial class MasmorraData : IMasmorraData
    {
        public string Descricao { get; set; }
        public SegmentoInicial SegmentoInicial { get; set; }
        public ITabelaSegmentos TabelaSegmentos { get; set; }
        public ITabelaArmadilhaElement[] TabelaPassagemSecreta { get; set; }
        public ITabelaArmadilhaElement[] TabelaArmadilha { get; set; }
        public ITabelaConteudo[] TabelaConteudo { get; set; }
        public ITabelaMonstro[] TabelaMonstro { get; set; }
        public ITabelaRecompensa TabelaRecompensa { get; set; }
        public ITabelaChefeDaMasmorra[] TabelaChefeDaMasmorra { get; set; }
        public ITabelaArmadura[] TabelaArmadura { get; set; }
        public ITabelaArma[] TabelaArma { get; set; }
    }

    public partial class TabelaArma : ITabelaArma
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Dano { get; set; }
        public string Caracteristicas { get; set; }
    }

    public partial class TabelaArmadilhaElement : ITabelaArmadilhaElement
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
    }

    public partial class TabelaArmadura : ITabelaArmadura
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public long Pvs { get; set; }
    }

    public partial class TabelaChefeDaMasmorra : ITabelaChefeDaMasmorra
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public int Qtd { get; set; }
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int Pvs { get; set; }
    }

    public partial class TabelaConteúdo : ITabelaConteudo
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public bool? PassagemSecreta { get; set; }
        public string Moedas { get; set; }
        public string Pergaminho { get; set; }
        public long Baú { get; set; }
        public string ItensMagico { get; set; }
    }

    public partial class TabelaMonstro : ITabelaMonstro
    {
        public int Indice { get; set; }
        public string Qtd { get; set; }
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int Pvs { get; set; }
        public string Caracteristicas { get; set; }
    }

    public partial class TabelaRecompensa : ITabelaRecompensa
    {
        public ITabelaItemTesouro[] TabelaTesouro { get; set; }
        public ITabelaItemMaravilha[] TabelaMaravilha { get; set; }
        public ITabelaItemMagico[] TabelaItemMágico { get; set; }
    }

    public partial class TabelaItemTesouro : ITabelaItemTesouro
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }
    public partial class TabelaItemMaravilha : ITabelaItemMaravilha
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }
    public partial class TabelaItemMagico : ITabelaItemMagico
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }

    public partial class TabelaSegmentos : ITabelaSegmentos
    {
        public ITabelaAPartirDe[] TabelaAPartirDeEscadaria { get; set; }
        public ITabelaAPartirDe[] TabelaAPartirDeCorredor { get; set; }
        public ITabelaAPartirDe[] TabelaAPartirDeSala { get; set; }
    }

    public partial class TabelaAPartirDe : ITabelaAPartirDe
    {
        public int Indice { get; set; }
        public SegmentoTipo Segmento { get; set; }
        public string Descricao { get; set; }
        public int Portas { get; set; }
    }
}
