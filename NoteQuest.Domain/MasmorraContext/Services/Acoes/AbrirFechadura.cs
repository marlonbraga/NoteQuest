using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirFechadura : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }

        public AbrirFechadura(int indice, IPortaComum porta)
        {
            Porta = porta;
            Titulo = $"Abrir fechadura {porta.Posicao}";
            Descricao = "Abre acesso a sala trancada sem alertar monstros. Ação demorada. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            Porta.AbrirFechadura();
            Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, indice ?? D6.Rolagem(deslocamento: true));
            BaseSegmento novoSegmento = Porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Você gasta algum tempo tentando arrombar o cadeado. Sua tocha acaba. Mas a porta é destravada revelando um segmento da masmorra.";
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
