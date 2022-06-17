using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public interface IPortaComum : IPorta
    {
        public BaseSegmento SegmentoAlvo { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public IPortaComum InvertePorta();
        public EstadoDePorta VerificarFechadura(int valorD6);
        public void Build(BaseSegmento segmentoAtual, Posicao posicao);
    }
}
