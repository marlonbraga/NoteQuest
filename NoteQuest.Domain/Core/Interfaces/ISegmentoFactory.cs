using System.Dynamic;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface ISegmentoFactory
    {
        IMasmorra Masmorra {get; set;}

        public (string descricao, BaseSegmento segmentoInicial) GeraSegmentoInicial(IMasmorra masmorra, int indice = 1);
        
        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice);

        public bool EhSalaFinal(IPortaComum portaDeEntrada);

        public BaseSegmento GeraSalaFinal(IPortaComum portaDeEntrada, int? indice = null);

    }
}
