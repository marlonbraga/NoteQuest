﻿using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AcharPassagemSecreta : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public AcharPassagemSecreta()
        {
            Titulo = "Procurar passagem secreta";
            Descricao = "Ação demorada. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            return null;
        }
    }
}
