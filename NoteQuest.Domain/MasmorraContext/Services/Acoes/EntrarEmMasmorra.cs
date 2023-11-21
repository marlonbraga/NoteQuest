using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class EntrarEmMasmorra : IAcao
    {
        public int Indice { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public EntrarEmMasmorra(int indice, IMasmorraRepository masmorraRepository, IMasmorra masmorra, IPortaEntrada portaEntrada)
        {
            Indice = indice;
            MasmorraRepository = masmorraRepository;
            Masmorra = masmorra;
            PortaEntrada = portaEntrada;
            Titulo = "Entrar em masmorra";
            Descricao = "Ambiente escuro e perigoso. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            (string, BaseSegmento) entradaEmMasmorra;
            entradaEmMasmorra = SegmentoFactory.Instancia(MasmorraRepository).GeraSegmentoInicial(Masmorra);
            BaseSegmento segmentoInicial = entradaEmMasmorra.Item2;
            ConsequenciaDTO consequencia = new()
            {
                Descricao = $"  {entradaEmMasmorra.Item1}\n  {segmentoInicial.Descricao}",
                Segmento = segmentoInicial,
                Escolhas = segmentoInicial.RecuperaTodasAsEscolhas()
            };

            return consequencia;
        }
    }
}
