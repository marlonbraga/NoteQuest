﻿using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class QuebrarPorta : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }

        public QuebrarPorta(int indice, IPortaComum porta)
        {
            Porta = porta;
            Titulo = $"Quebrar porta {porta.Posicao}";
            Descricao = "Abre acesso a sala trancada sem gastar tochas. Se houver monstros, sofrerá ataque primeiro.";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            Porta.QuebrarPorta();
            Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, indice ?? D6.Rolagem(deslocamento: true));
            BaseSegmento novoSegmento = Porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Você aplica diversos golpes a porta. O barulho ecoa pelo ambinete. A porta logo é quebrada revelando um segmento da masmorra.";
            texto += $"\n  #{novoSegmento.IdSegmento}";
            texto += $"\n  {novoSegmento.Descricao}";
            texto += novoSegmento.DetalhesDescricao;
            ConsequenciaDTO consequencia = new()
            {
                Descricao = texto,
                Segmento = novoSegmento,
                Escolhas = novoSegmento.RecuperaTodasAsEscolhas()
            };

            return consequencia;
        }
    }
}
