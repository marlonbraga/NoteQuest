﻿using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteQuest.Domain.Core.DTO;

namespace NoteQuest.Domain.Core.Classes
{
    public class Gladiador : IClasse
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public IArma ArmaInicial { get; set; }
        public int QtdMagias { get; set; }
        public string EventTrigger { get; set; }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent) => gameEvent;

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 6;
            Nome = "Gladiador";
            Vantagem = "Nenhuma.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Espada curta", -0);
            QtdMagias = 0;
        }

        public IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null)
        {
            throw new NotImplementedException();
        }
    }
}
