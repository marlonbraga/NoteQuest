using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IPortaComum : IPorta
    {
        BaseSegmento SegmentoAlvo { get; set; }
        EstadoDePorta EstadoDePorta { get; set; }

        IPortaComum InvertePorta();
        EstadoDePorta VerificarFechadura(int? indice = null);
        void AbrirFechadura();
        void QuebrarPorta();
    }
}
