﻿using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Interfaces;

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
        public ISegmentoFactory SegmentoFactory { get; set; }
        private IEnumerable<ActionResult> Consequencia { get; set; }
        public BaseSegmento SalaFinal { get; set; }
        public int QtdPortasInexploradas { get; set; }
        public bool FoiExplorada { get; set; }
        public bool FoiConquistada { get; set; }

        public Masmorra(IPortaEntrada portaEntrada, IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory, IArmadilhaFactory armadilhaFactory)
        {
            portaEntrada.Masmorra = this;
            PortaEntrada = portaEntrada;
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
            ArmadilhaFactory = armadilhaFactory;

            (string descricao, BaseSegmento segmentoInicial) entradaEmMasmorra;
            entradaEmMasmorra = GeraSegmentoInicial();

            BaseSegmento segmentoInicial = entradaEmMasmorra.segmentoInicial;
            segmentoInicial.Masmorra = this;
            foreach (var porta in segmentoInicial.Portas)
            {
                porta.Masmorra = this;
            }

            DungeonConsequence dungeonConsequence = new($"  {entradaEmMasmorra.descricao}", segmentoInicial);
            Consequencia = new List<DungeonConsequence>( 1) { dungeonConsequence };
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

        public (string descricao, BaseSegmento segmentoInicial) GeraSegmentoInicial()
        {
            return SegmentoFactory.GeraSegmentoInicial(this);
        }
    }
}
