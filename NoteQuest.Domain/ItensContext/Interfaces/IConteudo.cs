using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.ItensContext.Interfaces
{
    public interface IConteudo
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }
        public IList<IRepositorio> Repositorio { get; set; }
        public bool? PassagemSecreta { get; set; }
        public bool Armadilhas { get; set; }
        public int? Bau { get; set; }
        public int? QtdItens { get; set; }
        public IMasmorra Masmorra { get; set; }
    }
}
