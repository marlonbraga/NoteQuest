using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Arma : IArma
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool EmpunhaduraDupla { get; set; }
        public short Dano { get; set; }

        public void Build(string nome, short dano = 0, bool empunhaduraDupla = false)
        {
            Nome = nome;
            Descricao = FormataDano(dano);
            Dano = dano;
            EmpunhaduraDupla = empunhaduraDupla;
        }
        private string FormataDano(short dano)
        {
            if (dano < 0) return $"Dano: [1-6] {dano}";
            if (dano > 0) return $"Dano: [1-6] +{dano}";
            return $"Dano: [1-6]";
        }
    }
}
