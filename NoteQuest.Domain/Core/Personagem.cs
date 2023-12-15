using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.ItensContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core
{
    public class Personagem : IPersonagem
    {
        public IPontosDeVida Pv { get; set; }
        public string Nome { get; set; }
        public IRaca Raca { get; set; }
        public List<IClasse> Classes { get; set; }
        public IInventario Inventario { get; set; }

        public IEvent ChainOfResponsabilityEfeito(IEvent acao)
        {
            acao.Personagem = this;
            IEvent acaoModificada;
            acaoModificada = Raca.EffectSubstitutionComposite(acao);
            foreach (IClasse classe in Classes)
            {
                acaoModificada = classe.EffectSubstitutionComposite(acao);
            }

            return acaoModificada;
        }

        public Personagem()
        {
            Pv = new PontosDeVida();
            Inventario = new Inventario();
        }

        public void Build(string nome, IRaca indiceRaca, IClasse indiceClasse)
        {
            Nome = nome;
            Raca = indiceRaca;
            Classes = new List<IClasse>() { indiceClasse };
        }
    }
}
