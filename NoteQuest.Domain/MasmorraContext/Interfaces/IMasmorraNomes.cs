using NoteQuest.Domain.MasmorraContext.DTO;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraNomes
    {
        public Tipodemasmorra[] TipoDeMasmorra { get; set; }
        public Segundaparte[] SegundaParte { get; set; }
        public Terceiraparte[] TerceiraParte { get; set; }
    }
}
