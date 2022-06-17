namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public interface IAcaoPorta
    {
        public IPortaComum Porta { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
    }
}
