using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IArmadilhaFactory
    {
        public IArmadilha GeraArmadilha(IMasmorra masmorra, int? indice = null);
    }
}
