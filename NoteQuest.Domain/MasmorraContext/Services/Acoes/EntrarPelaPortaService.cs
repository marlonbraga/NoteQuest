using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class EntrarPelaPortaService : IEntrarPelaPortaService, IAcaoPorta
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public EntrarPelaPortaService(IPortaComum porta, ISegmentoBuilder segmentoFactory)
        {
            Porta = porta;
            Titulo = $"Entrar pela porta de {porta.Posicao}";
            Descricao = "Acessa nova sala. Se houver monstros, você ataca primeiro.";
            SegmentoFactory = segmentoFactory;
            Execucao = Executar;
        }

        public ConsequenciaDTO Executar()
        {
            Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, D6.Rolagem());
            BaseSegmento novoSegmento = Porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Você abre a porta revelando um segmento da masmorra.";
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
