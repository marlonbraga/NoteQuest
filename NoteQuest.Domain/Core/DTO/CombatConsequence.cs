using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.Core.DTO
{
    public class CombatConsequence : ActionResult
    {
        public CombatConsequence(string descricao, BaseSegmento segment) : base(descricao)
        {
            Segment = segment;
        }

        public BaseSegmento Segment { get; set; }
    }
}