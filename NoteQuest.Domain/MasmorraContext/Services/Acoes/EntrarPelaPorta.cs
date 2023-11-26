using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class EntrarPelaPorta : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }
        public IMasmorra Masmorra { get; set; }
        public int? IndicePreDefinido { get; set; }

        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public EntrarPelaPorta(IPortaComum porta, int? indicePreDefinido)
        {
            Porta = porta;
            Masmorra = porta.Masmorra;
            Titulo = $"Entrar pela porta de {porta.Posicao}";
            Descricao = "Acessa nova sala. Se houver monstros, você ataca primeiro.";
            IndicePreDefinido = indicePreDefinido;
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            IndicePreDefinido = EhEscadariaObrigatoria(Porta);
            Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, indice ?? IndicePreDefinido ?? D6.Rolagem(deslocamento: true));
            BaseSegmento novoSegmento = Porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Você abre a porta revelando um segmento da masmorra.";
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

        public int? EhEscadariaObrigatoria(IPortaComum porta)
        {
            int floor = porta.Andar;
            var segmentoAlvo = porta.SegmentoAlvo;
            var salaFinalEncontrada = porta.Masmorra.SalaFinal is not null;
            if (floor > -2 && Masmorra.QtdPortasInexploradas == 1 && segmentoAlvo is null && !salaFinalEncontrada)
                return 5;
            return IndicePreDefinido;
        }
    }
}
