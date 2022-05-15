using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Corredor : BaseSegmento
    {
        public Corredor(IPortaComum portaDeEntrada, string descricao, int qtdPortas) : base(portaDeEntrada, descricao, qtdPortas)
        {
            Descricao = descricao;
        }
    }
}