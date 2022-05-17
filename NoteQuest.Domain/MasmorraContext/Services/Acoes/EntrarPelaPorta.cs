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
        //public ISegmentoFactory SegmentoFactory { get; set; }

        public EntrarPelaPorta(IPortaComum porta/*, ISegmentoFactory SegmentoFactory*/)
        {
            Porta = porta;
            Titulo = $"Entrar pela porta de {porta.Posicao}";
            Descricao = "Acessa nova sala. Se houver monstros, você ataca primeiro.";
            //this.SegmentoFactory = SegmentoFactory;
        }

        public ConsequenciaDTO Executar()
        {
            BaseSegmento novoSegmento = SegmentoFactory.GeraSegmento(Porta, D6.Rolagem());

            ConsequenciaDTO consequencia = new()
            {
                Descricao = $"\n  {novoSegmento.Descricao}",
                Segmento = novoSegmento,
                Escolhas = novoSegmento.RecuperaTodasAsEscolhas()
            };

            return consequencia;
        }
    }
}
