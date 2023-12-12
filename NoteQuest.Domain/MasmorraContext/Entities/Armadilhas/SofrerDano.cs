using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Entities.Armadilhas
{
    public class SofrerDano : IArmadilha
    {
        public string Descricao { get; set; }
        public int Dano { get; set; }

        public SofrerDano(string descricao, int dano)
        {
            Descricao = descricao;
            Dano = dano;
        }

        public string Efeito(IPersonagem personagem)
        {
            string texto = $"\n  ARMADILHA!";
            texto += $"\n  {Descricao}";
            texto += $"\n  {personagem.Nome} sofreu {Dano} de dano!";
            personagem.Pv.ReceberDano(Dano, out bool morreu);
            if (morreu)
            {
                texto += $"\n  {personagem.Nome} morreu!";
            }

            return texto;
        }
    }
}
