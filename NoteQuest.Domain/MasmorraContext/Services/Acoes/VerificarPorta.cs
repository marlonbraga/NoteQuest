using NoteQuest.Domain.Core;
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
        public IMasmorra Masmorra { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int? IndicePreDefinido { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Efeito { get; set; }

        public VerificarPorta(IPortaComum porta, int? indicePreDefinido)
        {
            GatilhoDeAcao = GatilhoDeAcao.VerificarPorta;
            Efeito = delegate { return Executar(); };
            Porta = porta;
            Masmorra = porta.Masmorra;
            Titulo = "Verificar porta";
            Descricao = "Pode acionar armadilhas";
            IndicePreDefinido = indicePreDefinido;
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            EstadoDePorta estado = Porta.VerificarFechadura(indice ?? D6.Rolagem());
            IndicePreDefinido = EhEscadariaObrigatoria(Porta);
            if (estado == EstadoDePorta.aberta)
                Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, indice ?? IndicePreDefinido ?? D6.Rolagem(deslocamento: true));
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

        public int? EhEscadariaObrigatoria(IPortaComum porta)
        {
            int floor = porta.Andar;
            var segmentoAlvo = porta.SegmentoAlvo;
            var salaFinalEncontrada = porta.Masmorra.SalaFinal is not null;
            var veioDeEscada = porta.SegmentoAtual.GetType() == typeof(Escadaria);
            if (floor > -2 && Masmorra.QtdPortasInexploradas == 1 && segmentoAlvo is null && !salaFinalEncontrada && !veioDeEscada)
                return 5;
            return IndicePreDefinido;
        }
    }
}
