namespace NoteQuest.Domain.CombateContext.Entities
{
    public class Monstro
    {
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int PvMaximo { get; set; }
        public int PV { get; set; }
        public string[] Caracteristicas { get; set; }

        public Monstro(string nome, int dano, int pv)
        {
            Nome = nome;
            Dano = dano;
            PV = pv;
            PvMaximo = pv;
        }

        public int Atacar()
        {
            return Dano;
        }

        public void LevarDano(int dano)
        {
            PV -= dano;
        }

        public void Morre()
        {
            PV = 0;
        }
    }
}
