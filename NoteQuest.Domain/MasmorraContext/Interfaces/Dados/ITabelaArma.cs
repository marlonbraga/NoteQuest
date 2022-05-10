using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ITabelaArma
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Dano { get; set; }
        public string Caracteristicas { get; set; }
    }
}
