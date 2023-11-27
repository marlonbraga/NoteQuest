﻿using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class SairDeMasmorra : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Efeito { get; set; }

        public SairDeMasmorra()
        {
            GatilhoDeAcao = GatilhoDeAcao.SairDeMasmorra;
            Efeito = delegate { return Executar(); };
            Titulo = "Sair de Masmorra";
            Descricao = "Pode voltar a cidade para se recuperar. Mas os montros da masmorra também restaurarão as energias.";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            return null;
        }
    }
}
