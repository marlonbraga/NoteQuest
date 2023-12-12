using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class ActionResult
    {
        public string Descricao { get; set; }
        public List<ConsequenciaDTO> Consequences { get; set; }
    }
}