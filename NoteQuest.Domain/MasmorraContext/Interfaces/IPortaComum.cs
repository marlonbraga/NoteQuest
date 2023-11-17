using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IPortaComum : IPorta
    {
        BaseSegmento SegmentoAlvo { get; set; }
        EstadoDePorta EstadoDePorta { get; set; }

        IPortaComum InvertePorta();
        EstadoDePorta VerificarFechadura(int valorD6);
        void AbrirFechadura();
        void QuebrarPorta();
    }
}
