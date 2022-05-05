using NoteQuest.Domain.CombateContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class MasmorraBuilder
    {
        public string Descrição { get; set; }
        public TabelaSegmentos TabelaSegmentos { get; set; }
        public TabelaPassagemSecreta[] TabelaPassagemSecreta { get; set; }
        public TabelaArmadilha[] TabelaArmadilha { get; set; }
        public TabelaConteúdo[] TabelaConteúdo { get; set; }
        public TabelaMonstro[] TabelaMonstro { get; set; }
        public TabelaRecompensa TabelaRecompensa { get; set; }
        public TabelaChefeDaMasmorra[] TabelaChefeDaMasmorra { get; set; }
        public TabelaArmadura[] TabelaArmadura { get; set; }
        public TabelaArma[] TabelaArma { get; set; }

        public MasmorraBuilder(int indice = 1)
        {

        }
    }

    public class TabelaSegmentos
    {
        public TabelaAPartirDeEscadaria[] TabelaApartirdeEscadaria { get; set; }
        public TabelaAPartirDeCorredor[] TabelaApartirdeCorredor { get; set; }
        public TabelaAPartirDeSala[] TabelaApartirdeSala { get; set; }
    }
    public class TabelaAPartirDeEscadaria
    {
        public int indice { get; set; }
        public string segmento { get; set; }
        public string descricao { get; set; }
        public string portas { get; set; }
    }
    public class TabelaAPartirDeCorredor
    {
        public int indice { get; set; }
        public string segmento { get; set; }
        public string descricao { get; set; }
        public string portas { get; set; }
    }
    public class TabelaAPartirDeSala
    {
        public int indice { get; set; }
        public string segmento { get; set; }
        public string descricao { get; set; }
        public string portas { get; set; }
    }
    public class TabelaRecompensa
    {
        public TabelaTesouro[] TabelaTesouro { get; set; }
        public TabelaMaravilha[] TabelaMaravilha { get; set; }
        public TabelaItemMágico[] TabelaItemMágico { get; set; }
    }
    public class TabelaTesouro
    {
        public int indice { get; set; }
        public string descricao { get; set; }
        public string efeito { get; set; }
    }
    public class TabelaMaravilha
    {
        public int indice { get; set; }
        public string descricao { get; set; }
        public string efeito { get; set; }
    }
    public class TabelaItemMágico
    {
        public int indice { get; set; }
        public string descricao { get; set; }
        public string efeito { get; set; }
    }
    public class TabelaPassagemSecreta
    {
        public int indice { get; set; }
        public string descricao { get; set; }
    }
    public class TabelaArmadilha
    {
        public int indice { get; set; }
        public string descricao { get; set; }
    }
    public class TabelaConteúdo
    {
        public int indice { get; set; }
        public string descricao { get; set; }
        public bool PassagemSecreta { get; set; }
        public string moedas { get; set; }
        public string pergaminho { get; set; }
        public string baú { get; set; }
        public string ItensMagico { get; set; }
    }
    public class TabelaMonstro
    {
        public int indice { get; set; }
        public string qtd { get; set; }
        public string nome { get; set; }
        public int dano { get; set; }
        public int pvs { get; set; }
        public string caracteristicas { get; set; }
    }

    public class TabelaChefeDaMasmorra
    {
        public int indice { get; set; }
        public string descricao { get; set; }
        public int qtd { get; set; }
        public string nome { get; set; }
        public int dano { get; set; }
        public int pvs { get; set; }
    }
    public class TabelaArmadura
    {
        public int indice { get; set; }
        public string nome { get; set; }
        public string pvs { get; set; }
    }
    public class TabelaArma
    {
        public int indice { get; set; }
        public string nome { get; set; }
        public string dano { get; set; }
        public string caracteristicas { get; set; }
    }
}
