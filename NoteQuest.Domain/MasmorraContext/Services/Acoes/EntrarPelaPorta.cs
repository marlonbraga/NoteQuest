using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class EntrarPelaPorta : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }
        public int? IndicePreDefinido { get; set; }

        public EntrarPelaPorta(IPortaComum porta, int? indicePreDefinido)
        {
            Porta = porta;
            Titulo = $"Entrar pela porta de {porta.Posicao}";
            Descricao = "Acessa nova sala. Se houver monstros, você ataca primeiro.";
            IndicePreDefinido = indicePreDefinido;
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
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
    }
}
