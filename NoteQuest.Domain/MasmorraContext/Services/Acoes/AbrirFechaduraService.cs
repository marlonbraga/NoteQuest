using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirFechaduraService : IAbrirFechaduraService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int QtdTochasConsumidas { get; set; }
        public IPortaComum Porta { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public AbrirFechaduraService(IPortaComum porta)
        {
            Titulo = "Abrir fechadura";
            Descricao = "Abre acesso a sala trancada sem alertar monstros. Ação demorada. Gasta 1 tocha";
        }

        public void Build(int qtdTochasConsumidas)
        {
            QtdTochasConsumidas = qtdTochasConsumidas;
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
