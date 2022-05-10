using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core.DTO
{
    public class ResultadoAcao : IResultadoAcao
    {
        public string Descrição { get; set; }
        public List<IAcao> AcoesPossiveis { get; set; }
    }
}
