using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.Core.Entities.Raca.Basica
{
    public class Gnomo : IRaca
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public int QtdMagias { get; set; }
        public string Vantagem { get; set; }
        public Type TipoAcao { get; set; }
        public IAcao Acao { get; set; }

        public IAcao AtualizarAcao(IAcao acao)
        {
            throw new NotImplementedException();
        }

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 14;
            QtdMagias = 0;
            Nome = "Gnomo";
            Vantagem = "Começa o jogo com 3 usos de Magias Básicas aleatórias.";
        }

        public ConsequenciaDTO Efeito()
        {
            throw new NotImplementedException();
        }
    }
}
