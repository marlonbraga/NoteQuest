using NoteQuest.Domain.MasmorraContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.DTO
{
    public class ConsequenciaDTO
    {
        public string Descricao { get; set; }
        public BaseSegmento Segmento { get; set; }
        public List<IEscolha> Escolhas { get; set; }
    }
}
