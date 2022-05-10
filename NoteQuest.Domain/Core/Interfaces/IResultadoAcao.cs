using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IResultadoAcao
    {
        public string Descrição { get; set; }
        public List<IAcao> AcoesPossiveis { get; set; }
    }
}
