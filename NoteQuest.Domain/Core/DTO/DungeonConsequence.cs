using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.Core.DTO
{
    public class DungeonConsequence: ActionResult, ISalaSegmentConsequence
    {
        public DungeonConsequence(string descricao, BaseSegmento segment) : base(descricao)
        {
            Segment = segment;
        }

        public BaseSegmento Segment { get; set; }
    }
}