using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.ItensContext.Factories;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Masmorra : IMasmorra
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public TipoMasmorra TipoMasmorra { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IArmadilhaFactory ArmadilhaFactory { get; set; }
        public IItemFactory ItemFactory { get; set; }
        public ISegmentoFactory SegmentoFactory { get; set; }
        private IEnumerable<ActionResult> Consequencia { get; set; }
        public BaseSegmento SalaFinal { get; set; }
        public int QtdPortasInexploradas { get; set; }
        public bool FoiExplorada { get; set; }
        public bool FoiConquistada { get; set; }

        public Masmorra(IPortaEntrada portaEntrada, IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory, IArmadilhaFactory armadilhaFactory, IItemFactory itemFactory)
        {
            portaEntrada.Masmorra = this;
            PortaEntrada = portaEntrada;
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
            ArmadilhaFactory = armadilhaFactory;
            ItemFactory = itemFactory;
        }

        public void Build()
        {
            (string descricao, BaseSegmento segmentoInicial) entradaEmMasmorra;
            entradaEmMasmorra = GeraSegmentoInicial();

            BaseSegmento segmentoInicial = entradaEmMasmorra.segmentoInicial;
            segmentoInicial.Masmorra = this;
            foreach (var porta in segmentoInicial.Portas)
            {
                porta.Masmorra = this;
            }

            DungeonConsequence dungeonConsequence = new($"  {entradaEmMasmorra.descricao}", segmentoInicial);
            Consequencia = new List<DungeonConsequence>(1) { dungeonConsequence };
        }

        public IEnumerable<ActionResult> EntrarEmMasmorra()
        {
            return Consequencia;
        }

        public string BuscarTipo(int indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.TipoDeMasmorra[indice];
        }

        public IEvent GeraArmadilha(int? indice = null)
        {
            return ArmadilhaFactory.GeraArmadilha(this, indice);
        }

        public IItem GeraItem(int? indice = null)
        {
            indice ??= D6.Rolagem(1, true);
            IItem item =  ItemFactory.GeraTesouro(this, indice);
            return item;
        }

        public (string descricao, BaseSegmento segmentoInicial) GeraSegmentoInicial()
        {
            return SegmentoFactory.GeraSegmentoInicial(this);
        }
    }
}
