using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
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
        public IClasseBasicaRepository MasmorraRepository { get; set; }

        public Masmorra(IClasseBasicaRepository masmorraRepository, ISegmentoBuilder segmentoBuilder, IPortaEntrada portaEntrada)
        {
            MasmorraRepository = masmorraRepository;
            SegmentoBuilder = segmentoBuilder;
            PortaEntrada = portaEntrada;
        }

        public void Build(ushort indice1, ushort indice2, ushort indice3)
        {
            Tuple<string, BaseSegmento> entradaDaMasmorra;
            SegmentoBuilder.Build(indice1);
            entradaDaMasmorra = SegmentoBuilder.GeraSegmentoInicial();
            Descricao = entradaDaMasmorra.Item1;
            SegmentoInicial = (ISegmentoInicial)entradaDaMasmorra.Item2;
            Nome = GerarNome(indice1, indice2, indice3);
        }

        public string GerarNome(ushort indice1, ushort indice2, ushort indice3)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();

            string nomeParte1 = masmorraNomes.TipoDeMasmorra[indice1].tipo;
            string nomeParte2 = masmorraNomes.SegundaParte[indice2].nome;
            string nomeParte3 = masmorraNomes.TerceiraParte[indice3].nome;

            nomeParte3 = TratarGenero(nomeParte2, nomeParte3);

            Nome = $"{nomeParte1} {nomeParte2} {nomeParte3}";

            return Nome;
        }

        private string TratarGenero(string nomeParte2, string nomeParte3)
        {
            if (nomeParte2[1] == 'o' || nomeParte2[1] == 'a')
            {
                nomeParte3 = nomeParte3.Replace("o(a)", nomeParte2[1].ToString());
            }
            return nomeParte3;
        }
        public string BuscarTipo(ushort indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.TipoDeMasmorra[indice].tipo;
        }
        public string BuscarSegundaParteDoNome(ushort indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.SegundaParte[indice].nome;
        }
        public string BuscarTerceiraParteDoNome(ushort indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.TerceiraParte[indice].nome;
        }
    }
}
