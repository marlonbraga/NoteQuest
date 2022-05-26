using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Masmorra : IMasmorra
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoInicial SegmentoInicial { get; set; }
        public ISegmentoBuilder SegmentoBuilder { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }

        public Masmorra(IMasmorraRepository masmorraRepository, ISegmentoBuilder segmentoBuilder, IPortaEntrada portaEntrada)
        {
            MasmorraRepository = masmorraRepository;
            SegmentoBuilder = segmentoBuilder;
        }

        public void Build(int indice1, int indice2, int indice3)
        {
            Tuple<string, BaseSegmento> entradaEmMasmorra;
            SegmentoBuilder.Build(indice1);
            entradaEmMasmorra = SegmentoBuilder.GeraSegmentoInicial();
            Descricao = entradaEmMasmorra.Item1;
            SegmentoInicial = (ISegmentoInicial)entradaEmMasmorra.Item2;
            Nome = GerarNome(indice1, indice2, indice3);
        }

        public string GerarNome(int indice1, int indice2, int indice3)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra(); 

            string nomeParte1 = masmorraNomes.TipoDeMasmorra[indice1 - 1].tipo;
            string nomeParte2 = masmorraNomes.SegundaParte[indice2 - 1].nome;
            string nomeParte3 = masmorraNomes.TerceiraParte[indice3 - 1].nome;
            Nome = $"{nomeParte1} {nomeParte2} {nomeParte3}";

            return Nome;
        }
        public string BuscarTipo(int indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.TipoDeMasmorra[indice - 1].tipo;
        }
        public string BuscarSegundaParteDoNome(int indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.SegundaParte[indice - 1].nome;
        }
        public string BuscarTerceiraParteDoNome(int indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.TerceiraParte[indice - 1].nome;
        }
    }
}
