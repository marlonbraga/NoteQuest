using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class DungeonConsequence: ActionResult
    {
        public DungeonConsequence(string descricao, BaseSegmento segment) : base(descricao)
        {
            Segment = segment;
        }

        public BaseSegmento Segment { get; set; }
    }
}