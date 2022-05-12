using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Escadaria : BaseSegmento
    {
        public Escadaria(IPortaComum portaDeEntrada, string descricao) : base(portaDeEntrada, descricao)
        {
            Descricao = descricao;
            //TODO: 1 única porta com um nível abaixo
        }
    }
}