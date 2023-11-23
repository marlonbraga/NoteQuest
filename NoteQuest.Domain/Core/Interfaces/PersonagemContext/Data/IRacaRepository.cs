﻿using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.PersonagemContext.Data
{
    public interface IRacaRepository
    {
        public Dictionary<int, IRaca> RacasBasicas { get; set; }

        public IRaca PegarRacaBasica(int indice);
    }
}
