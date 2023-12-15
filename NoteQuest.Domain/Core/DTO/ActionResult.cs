using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class ActionResult
    {
        public string Descricao { get; set; }
        public List<ConsequenciaDTO> Consequences { get; set; }
        
        public ActionResult(string descricao)
        {
            Descricao = descricao;
        }
    }
}