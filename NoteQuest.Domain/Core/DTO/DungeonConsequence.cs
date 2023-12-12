using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class DungeonConsequence: ActionResult
    {
        public BaseSegmento Segment { get; set; }
    }
}