using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ISegmentoInicial
    {
        public SegmentoTipo Segmento { get; set; }
        public string Descricao { get; set; }
        public int QtdPortas { get; set; }
    }
}
