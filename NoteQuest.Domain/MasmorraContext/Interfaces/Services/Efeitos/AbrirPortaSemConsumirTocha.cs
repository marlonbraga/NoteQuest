using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Services.Efeitos
{
    public class AbrirPortaSemConsumirTocha //: IEfeito, IEntrarPelaPortaService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }

        public AbrirPortaSemConsumirTocha(ISegmentoBuilder segmentoFactory)
        {
            Descricao = "Acessa nova sala. Se houver monstros, você ataca primeiro.";
            SegmentoFactory = segmentoFactory;
        }

        public void Build(IPortaComum porta)
        {
            Porta = porta;
            Titulo = $"Entrar pela porta de {porta.Posicao}";
            //Execucao = Executar();
        }

        public ConsequenciaDTO Executar()
        {
            Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, D6.Rolagem());
            BaseSegmento novoSegmento = Porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Você destranca a fechadura com successo e consegue espiar um novo segmento da masmorra.";
            texto += $"\n  Porém o processo foi demorado. A iluminação cessou te colocando outra vez na escuridão.";
            texto += $"\n  #{novoSegmento.IdSegmento}";
            texto += $"\n  {novoSegmento.Descricao}";
            //TODO: Mostras descrição de detalhes em uma nova ação
            texto += novoSegmento.DetalhesDescricao;
            ConsequenciaDTO consequencia = new()
            {
                Descricao = texto,
                Segmento = novoSegmento,
                //TODO: Verifica se é uma sala recem criada e passa a Escolha de gerar Conteudo e Monstros
                Escolhas = novoSegmento.RecuperaTodasAsEscolhas()
            };

            return consequencia;
        }
    }
}
