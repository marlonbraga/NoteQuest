using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface ISegmentoFactory
    {
        public BaseSegmento GeraSegmentoInicial();
        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice);
    }
}
