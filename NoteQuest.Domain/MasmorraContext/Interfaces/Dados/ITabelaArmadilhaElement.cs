using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ITabelaArmadilhaElement
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
    }
}
