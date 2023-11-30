using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Conteudo : IConteudo
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public bool? PassagemSecreta { get; set; }
        public IPortaComum Porta { get; set; }
        public string Moedas { get; set; }
        public string Pergaminho { get; set; }
        public long Bau { get; set; }
        public string ItensMagico { get; set; }

        public Conteudo(TabelaConteudo tabelaConteudo)
        {
            Indice = tabelaConteudo.Indice;
            Descricao = tabelaConteudo.Descricao;
            PassagemSecreta = tabelaConteudo.PassagemSecreta;
            Porta = null;
            Moedas = tabelaConteudo.Moedas;
            Pergaminho = tabelaConteudo.Pergaminho;
            Bau = tabelaConteudo.Baú;
            ItensMagico = tabelaConteudo.ItensMagico;
        }
    }
}
