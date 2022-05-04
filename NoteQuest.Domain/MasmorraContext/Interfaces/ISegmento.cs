using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface ISegmento
    {
        public int IdSegmento { get; }
        public string Descricao { get; set; }
        public List<IPorta> Portas { get; set; }

        public string Entrar();
        public void DesarmarArmadilhas(int valorD6);
    }
}
