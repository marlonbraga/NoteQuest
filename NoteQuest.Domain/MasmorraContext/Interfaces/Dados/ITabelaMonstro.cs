using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ITabelaMonstro
    {
        public int Indice { get; set; }
        public string Qtd { get; set; }
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int Pvs { get; set; }
        public string Caracteristicas { get; set; }
    }
}
