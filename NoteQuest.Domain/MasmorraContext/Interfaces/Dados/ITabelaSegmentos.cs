using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ITabelaSegmentos
    {
        public ITabelaAPartirDe[] TabelaAPartirDeEscadaria { get; set; }
        public ITabelaAPartirDe[] TabelaAPartirDeCorredor { get; set; }
        public ITabelaAPartirDe[] TabelaAPartirDeSala { get; set; }
    }
}
