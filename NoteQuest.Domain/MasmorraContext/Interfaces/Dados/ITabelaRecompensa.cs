using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ITabelaRecompensa
    {
        public ITabelaItemTesouro[] TabelaTesouro { get; set; }
        public ITabelaItemMaravilha[] TabelaMaravilha { get; set; }
        public ITabelaItemMagico[] TabelaItemMágico { get; set; }
    }
}
