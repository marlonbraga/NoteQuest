using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
