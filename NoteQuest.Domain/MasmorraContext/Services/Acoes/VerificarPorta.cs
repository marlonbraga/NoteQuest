﻿using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class VerificarPorta : IAcao
    {
        public IPortaComum Porta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Efeito { get; set; }

        public VerificarPorta(object indice, IPortaComum porta)
        {
            GatilhoDeAcao = GatilhoDeAcao.VerificarPorta;
            Efeito = delegate { return Executar(); };
            Porta = porta;
            Titulo = "Verificar porta";
            Descricao = "Pode acionar armadilhas";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            EstadoDePorta estado = Porta.VerificarFechadura(indice ?? D6.Rolagem());
            BaseSegmento segmentoAtual = Porta.SegmentoAtual;
            List<IEscolha> escolhas = segmentoAtual.RecuperaTodasAsEscolhas();
            ConsequenciaDTO consequencia = new()
            {
                Descricao = $"\n  A porta está {estado}",
                Segmento = segmentoAtual,
                Escolhas = escolhas
            };

            return consequencia;
        }
    }
}
