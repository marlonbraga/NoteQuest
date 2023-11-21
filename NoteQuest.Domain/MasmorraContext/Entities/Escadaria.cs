using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Escadaria : BaseSegmento, ISegmento
    {
        public Escadaria(IPortaComum portaDeEntrada, string descricao, int qtdPortas) : base(portaDeEntrada, descricao, qtdPortas)
        {
            Descricao = descricao;
            Andar = portaDeEntrada.Andar - 1;
            foreach (IPorta porta in Portas)
            {
                porta.Andar = this.Andar;
            }
        }
    }
}