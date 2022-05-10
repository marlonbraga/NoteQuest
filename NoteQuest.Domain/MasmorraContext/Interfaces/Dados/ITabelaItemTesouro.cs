using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ITabelaItemTesouro
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }
}
