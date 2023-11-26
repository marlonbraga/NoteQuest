using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;

namespace NoteQuest.Domain.Core.Interfaces.Personagem
{
    public interface IClasse : IModificador
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public IArma ArmaInicial { get; set; }
        public int QtdMagias { get; set; }

        public void Build(/*IAcao acao*/);
    }
}
