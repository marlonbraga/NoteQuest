using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public enum TipoMasmorra{
        none = 0,
        Palácio = 1,
        Cripta = 2,
        Tumba = 3,
        Santuário = 4,
        Templo = 5,
        Calabouço = 6,
        Esgotos = 7,
        CidadelaDosAnões = 8,
        Pirâmide = 9,
        Zigurate = 10,
        Laboratório = 11,
        Necrópole = 12,
        Entranhas = 13,
        MegaMasmorra = 14,
        Caverna = 15,
        CavernaAquática = 16,
        CavernaVulcânica = 17,
        Mina = 18,

    }

    public interface IMasmorra
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public TipoMasmorra TipoMasmorra { get; set; }
        public BaseSegmento SalaFinal { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public int QtdPortasInexploradas {get; set; }
        public bool FoiConquistada { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IEnumerable<ActionResult> EntrarEmMasmorra();
        string GerarNome(int parte2, int parte3);
    }
}
