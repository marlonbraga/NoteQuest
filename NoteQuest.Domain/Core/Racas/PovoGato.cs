﻿using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Racas
{
    public class PovoGato : IRaca
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public int QtdMagias { get; set; }
        public string Vantagem { get; set; }
        public Type TipoAcao { get; set; }
        public IEvent Acao { get; set; }
        public string EventTrigger { get; set; }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent) => gameEvent;

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 19;
            QtdMagias = 0;
            Nome = "PovoGato";
            Vantagem = "Vende equipamentos pelo dobro do valor";
        }

        public IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null)
        {
            throw new NotImplementedException();
        }
    }
}
