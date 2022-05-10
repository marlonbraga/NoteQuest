using NoteQuest.Domain.MasmorraContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface ISegmentoInicial
    {
        public SegmentoTipo Segmento { get; set; }
        public string Descricao { get; set; }
        public int Portas { get; set; }
    }
}
