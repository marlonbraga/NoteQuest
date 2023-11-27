using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirFechadura : IAcaoPorta
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Efeito { get; set; }
        //public ISegmentoBuilder SegmentoFactory { get; set; }

        public AbrirFechadura(IPortaComum porta, int? indice = null)
        {
            GatilhoDeAcao = GatilhoDeAcao.AbrirFechadura;
            Porta = porta;
            Titulo = $"Abrir fechadura {porta.Posicao}";
            Descricao = "Abre acesso a sala trancada sem alertar monstros. Ação demorada. Gasta 1 tocha";
            Efeito = delegate
                {
                    //TODO: Evento de escuridão (remover 1 tocha ou encerrar o eeita da magia LUZ)
                    Porta.AbrirFechadura();
                    Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, indice ?? D6.Rolagem(deslocamento: true));
                    BaseSegmento novoSegmento = Porta.SegmentoAlvo;
                    string texto = string.Empty;
                    texto += $"\n  Você gasta algum tempo tentando arrombar o cadeado. A porta é destravada revelando um segmento da masmorra.";
                    texto += $"\n  Porém o processo foi demorado. A iluminação cessou te colocando outra vez na escuridão.";
                    texto += $"\n  #{novoSegmento.IdSegmento}";
                    texto += $"\n  {novoSegmento.Descricao}";
                    texto += novoSegmento.DetalhesDescricao;
                    ConsequenciaDTO consequencia = new()
                    {
                        Descricao = texto,
                        Segmento = novoSegmento,
                        Escolhas = novoSegmento.RecuperaTodasAsEscolhas()
                        //TODO: Verifica se é uma sala recem criada e passa a Escolha de gerar Conteudo e Monstros
                    };

                    return consequencia;
                };
        }

        //public ConsequenciaDTO Executar(int? indice = null)
        //{
        //    Porta.AbrirFechadura();
        //    Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, indice ?? D6.Rolagem(deslocamento: true));
        //    BaseSegmento novoSegmento = Porta.SegmentoAlvo;
        //    string texto = string.Empty;
        //    texto += $"\n  Você gasta algum tempo tentando arrombar o cadeado. Sua tocha acaba. Mas a porta é destravada revelando um segmento da masmorra.";
        //    texto += $"\n  #{novoSegmento.IdSegmento}";
        //    texto += $"\n  {novoSegmento.Descricao}";
        //    texto += novoSegmento.DetalhesDescricao;
        //    ConsequenciaDTO consequencia = new()
        //    {
        //        Descricao = texto,
        //        Segmento = novoSegmento,
        //        Escolhas = novoSegmento.RecuperaTodasAsEscolhas()
        //    };

        //    return consequencia;
        //}
    }
}
