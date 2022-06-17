using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.Core.Entities.Classes.Basica
{
    public class Coveiro : IClasse
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public string ArmaInicial { get; set; }
        public int Dano { get; set; }
        public int QtdMagias { get; set; }
        public Type TipoAcao { get; set; }
        public IAcao Acao { get; set; }

        public IAcao AtualizarAcao(IAcao acao) => acao;

        public void Build(IAcao acao)
        {
            Acao = null;
            Pv = 4;
            Nome = "Coveiro";
            Vantagem = "Causa +2 de dano em Mortos-Vivos.";
            ArmaInicial = "Pá (Dano 1D6-1)";
            QtdMagias = 0;
        }

        public ConsequenciaDTO Efeito()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}