using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Arma : IArma
    {
        public IPontosDeVida Pv { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool EmpunhaduraDupla { get; set; }
        public short Dano { get; set; }

        public void Build(string nome, short dano = 0, bool empunhaduraDupla = false)
        {
            Nome = nome;
            Descricao = $"({FormataDano(dano)})";
            Dano = dano;
            EmpunhaduraDupla = empunhaduraDupla;
        }
        private string FormataDano(short dano)
        {
            if (dano < 0) return $"1d6{dano}";
            if (dano > 0) return $"1d6+{dano}";
            return $"1d6";
        }
    }
}
