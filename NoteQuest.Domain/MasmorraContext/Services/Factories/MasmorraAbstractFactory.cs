using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Services.Factories
{
    public class MasmorraAbstractFactory
    {
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IArmadilhaFactory ArmadilhaFactory { get; set; }
        public ISegmentoFactory SegmentoFactory { get; set; }

        public MasmorraAbstractFactory(IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory, IArmadilhaFactory armadilhaFactory)
        {
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
            ArmadilhaFactory = armadilhaFactory;
        }

        public Masmorra GerarMasmorra(int? indice1 = null, int? indice2 = null, int? indice3 = null, int? indiceChefe = null)
        {
            IPortaEntrada portaEntrada = new PortaEntrada();
            indice1 ??= D6.Rolagem();
            indice2 ??= D6.Rolagem(deslocamento: true);
            indice3 ??= D6.Rolagem(deslocamento: true);
            indiceChefe ??= D6.Rolagem(deslocamento: true);
            Masmorra masmorra = new (portaEntrada, MasmorraRepository, SegmentoFactory, ArmadilhaFactory)
            {
                Nome = GerarNome((TipoMasmorra)indice1, (int)indice2, (int)indice3),
                QtdPortasInexploradas = 1,
                FoiExplorada = false,
                FoiConquistada = false,
                TipoMasmorra = (TipoMasmorra)indice1
            };

            var entradaEmMasmorra = SegmentoFactory.GeraSegmentoInicial(masmorra, (int)indice1);
            portaEntrada.SegmentoAtual = entradaEmMasmorra.segmentoInicial;
            masmorra.Descrição = entradaEmMasmorra.descricao;
            portaEntrada.Masmorra = masmorra;
            return masmorra;
        }
        
        public string GerarNome(TipoMasmorra tipoMasmorra, int indice2, int indice3)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            string nomeParte1 = masmorraNomes.TipoDeMasmorra[((int)tipoMasmorra)-1];
            string nomeParte2 = masmorraNomes.SegundaParte[indice2];
            string nomeParte3 = masmorraNomes.TerceiraParte[indice3];

            nomeParte3 = TratarGenero(nomeParte2, nomeParte3);

            string nome = $"{nomeParte1} {nomeParte2} {nomeParte3}";

            return nome;
        }

        private string TratarGenero(string nomeParte2, string nomeParte3)
        {
            if (nomeParte2[1] == 'o' || nomeParte2[1] == 'a')
            {
                nomeParte3 = nomeParte3.Replace("o(a)", nomeParte2[1].ToString());
            }
            return nomeParte3;
        }
    }
}
