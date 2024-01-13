using NoteQuest.Domain.ItensContext.Interfaces;

namespace NoteQuest.Domain.Core.DTO
{
    public class PersonagemConsequence : ActionResult
    {
        public PersonagemConsequence(string descricao, IRepositorio repositorio) : base(descricao)
        {
            Repositorio = repositorio;
        }

        public IRepositorio Repositorio { get; set; }
    }
}