using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.Core.Entities.Classes.Basica
{
    public class Lenhador : IClasse
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
            Pv = 4;
            Nome = "Lenhador";
            Vantagem = "Quando destruir portas, role o dado. Se cair 6, ganha 1 Tocha.";
            ArmaInicial = "Machado (Dano 1D6)";
            QtdMagias = 0;
        }
    }
}