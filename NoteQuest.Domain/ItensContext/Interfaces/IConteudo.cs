using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.ItensContext.Interfaces
{
    public interface IConteudo
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public bool? PassagemSecreta { get; set; }
        public IPortaComum Porta { get; set; }
        public string Moedas { get; set; }
        public string Pergaminho { get; set; }
        public long Bau { get; set; }
        public string ItensMagico { get; set; }
    }
}
