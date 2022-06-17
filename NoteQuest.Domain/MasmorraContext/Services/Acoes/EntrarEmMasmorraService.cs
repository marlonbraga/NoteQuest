using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class EntrarEmMasmorraService : IEntrarEmMasmorraService
    {
        public int Indice { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoBuilder SegmentoBuilder { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        //TODO: depender de IMasmorra. Executar apenas Gera escolhas e gasta tocha
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public EntrarEmMasmorraService(IClasseBasicaRepository masmorraRepository, IPortaEntrada portaEntrada, ISegmentoBuilder segmentoBuilder)
        {
            MasmorraRepository = masmorraRepository;
            PortaEntrada = portaEntrada;
            SegmentoBuilder = segmentoBuilder;
            Titulo = "Entrar em masmorra";
            Descricao = "Ambiente escuro e perigoso. Gasta 1 tocha";
        }

        public void Build(IMasmorra masmorra)
        {
            Masmorra = masmorra;
            Execucao = Executar;
        }

        public ConsequenciaDTO Executar()
        {
            //TODO: Entrar em masmorra não gera segmento
            //Entrar em masmorra Entra em Segmento

            IPorta porta = Masmorra.PortaEntrada;
            BaseSegmento segmento = (BaseSegmento)Masmorra.SegmentoInicial;
            ConsequenciaDTO consequencia = new()
            {
                Descricao = $"  {Masmorra.Descricao}\n\t{segmento.Descricao}",
                Segmento = segmento,
                Escolhas = segmento.RecuperaTodasAsEscolhas()
            };

            return consequencia;
        }
    }
}
