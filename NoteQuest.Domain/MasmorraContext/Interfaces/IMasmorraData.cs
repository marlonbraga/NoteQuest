using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IMasmorraData
    {
        public string Descricao { get; set; }
        public SegmentoInicial SegmentoInicial { get; set; }
        public TabelaSegmentos TabelaSegmentos { get; set; }
        public IDictionary<ushort, TabelaArmadilhaElement> TabelaPassagemSecreta { get; set; }
        public IDictionary<ushort, TabelaArmadilhaElement> TabelaArmadilha { get; set; }
        public IDictionary<ushort, TabelaConteudo> TabelaConteudo { get; set; }
        public IDictionary<ushort, TabelaMonstro> TabelaMonstro { get; set; }
        public TabelaRecompensa TabelaRecompensa { get; set; }
        public IDictionary<ushort, TabelaChefeDaMasmorra> TabelaChefeDaMasmorra { get; set; }
        public IDictionary<ushort, TabelaArmadura> TabelaArmadura { get; set; }
        public IDictionary<ushort, TabelaArma> TabelaArma { get; set; }
    }
}
