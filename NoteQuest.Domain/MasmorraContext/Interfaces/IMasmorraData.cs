using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IMasmorraData
    {
        public string Descricao { get; set; }
        public SegmentoInicial SegmentoInicial { get; set; }
        public TabelaSegmentos TabelaSegmentos { get; set; }
        public TabelaArmadilhaElement[] TabelaPassagemSecreta { get; set; }
        public TabelaArmadilhaElement[] TabelaArmadilha { get; set; }
        public TabelaConteudo[] TabelaConteudo { get; set; }
        public TabelaMonstro[] TabelaMonstro { get; set; }
        public TabelaRecompensa TabelaRecompensa { get; set; }
        public TabelaChefeDaMasmorra[] TabelaChefeDaMasmorra { get; set; }
        public TabelaArmadura[] TabelaArmadura { get; set; }
        public TabelaArma[] TabelaArma { get; set; }
    }
}
