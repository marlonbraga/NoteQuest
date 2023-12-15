using System;

namespace NoteQuest.Domain.Core.Interfaces.Personagem
{
    public interface IRaca : IEfeito
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public int QtdMagias { get; set; }
        public string Vantagem { get; set; }
        public Type TipoAcao { get; set; }
        public IEvent Acao { get; set; }

        public void Build();
    }
}
