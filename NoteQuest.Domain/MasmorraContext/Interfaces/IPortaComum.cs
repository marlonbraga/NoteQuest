using NoteQuest.Domain.MasmorraContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IPortaComum : IPorta
    {
        public IPortaComum InvertePorta();

        public BaseSegmento SegmentoAlvo { get; set; }
    }
}
