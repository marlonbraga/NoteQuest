using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.Core.Entities.Classes.Basica
{
    public class Nobre : IClasse
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

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 0;
            QtdMagias = 1;
            Nome = "Nobre";
            Vantagem = "Começa o jogo com +1 Magia Básica aleatória. Pode construir castelos.";
            ArmaInicial = "Rapieira (Dano 1D6+1)";
            QtdMagias = 1;
        }
    }
}