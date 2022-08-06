using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class VerificarPortaService : IVerificarPortaService
    {
        public IPortaComum Porta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public VerificarPortaService(IPortaComum porta)
        {
            Porta = porta;
            Titulo = "Verificar porta";
            Descricao = "Checar se está aberta ou fechada. Pode acionar armadilhas.";
        }

        public ConsequenciaDTO Executar(int valorD6)
        {
            EstadoDePorta estado = Porta.VerificarFechadura(valorD6);
            BaseSegmento segmentoAtual = Porta.SegmentoAtual;
            IList<IEscolha> escolhas = segmentoAtual.RecuperaTodasAsEscolhas();
            ConsequenciaDTO consequencia = new()
            {
                Descricao = $"\n  A porta está {estado}",
                Segmento = segmentoAtual,
                Escolhas = escolhas
            };

            return consequencia;
        }

        public ConsequenciaDTO Executar()
        {
            //TODO: Deleta-me sem cagar a Interface
            return Executar(D6.Rolagem());
        }
    }
}
