﻿using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class MoverEmSilencioService : IMoverEmSilencioService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public MoverEmSilencioService()
        {
            Titulo = "Mover-se em silêncio";
            Descricao = "Tenta entrar em sala sem que os monstros te percebam. Se falhar, sofrerá ataque primeiro. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
