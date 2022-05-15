using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IEscolha
    {
        public IAcao Acao { get; set; }
    }
}
