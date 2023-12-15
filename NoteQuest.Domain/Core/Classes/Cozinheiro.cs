using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;

namespace NoteQuest.Domain.Core.Classes
{
    public class Cozinheiro : IClasse
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public IArma ArmaInicial { get; set; }
        public int QtdMagias { get; set; }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent) => gameEvent;

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 2;
            Nome = "Cozinheiro";
            Vantagem = "Ganha 1 Provisão por monstro (que não seja morto-vivo).";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Cutelo");
            QtdMagias = 0;
        }
    }
}
