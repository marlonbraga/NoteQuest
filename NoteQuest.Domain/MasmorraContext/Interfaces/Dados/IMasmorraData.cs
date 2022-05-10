using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public enum SegmentoTipo { Corredor, Escadaria, Sala };

    public interface IMasmorraData
    {
        public string Descricao { get; set; }
        public SegmentoInicial SegmentoInicial { get; set; }
        public ITabelaSegmentos TabelaSegmentos { get; set; }
        public ITabelaArmadilhaElement[] TabelaPassagemSecreta { get; set; }
        public ITabelaArmadilhaElement[] TabelaArmadilha { get; set; }
        public ITabelaConteudo[] TabelaConteudo { get; set; }
        public ITabelaMonstro[] TabelaMonstro { get; set; }
        public ITabelaRecompensa TabelaRecompensa { get; set; }
        public ITabelaChefeDaMasmorra[] TabelaChefeDaMasmorra { get; set; }
        public ITabelaArmadura[] TabelaArmadura { get; set; }
        public ITabelaArma[] TabelaArma { get; set; }
    }
}
