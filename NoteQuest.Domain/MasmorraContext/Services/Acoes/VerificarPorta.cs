using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class VerificarPorta : IAcao
    {
        public IPortaComum Porta { get; set; }
        public IMasmorra Masmorra { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int? IndicePreDefinido { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IPersonagem Personagem {get; set; }

        public VerificarPorta(IPortaComum porta, int? indicePreDefinido)
        {
            GatilhoDeAcao = GatilhoDeAcao.VerificarPorta;
            Efeito = delegate { return Executar(); };
            Porta = porta;
            Masmorra = porta.Masmorra;
            Titulo = "Verificar porta";
            Descricao = "Pode acionar armadilhas";
            IndicePreDefinido = indicePreDefinido;
        }

        public IEnumerable<ActionResult> Executar(int? indicePorta = null, int? indiceArmadilha = null)
        {
            List<ActionResult> result = new(2);

            EstadoDePorta estado = Porta.VerificarFechadura(indicePorta ?? D6.Rolagem());
            IndicePreDefinido = EhEscadariaObrigatoria(Porta);
            if (estado == EstadoDePorta.aberta)
                Porta.SegmentoAlvo = Porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(Porta, indicePorta ?? IndicePreDefinido ?? D6.Rolagem(deslocamento: true));

            indiceArmadilha ??= D6.Rolagem();
            if (indiceArmadilha == 1)
            {
                ActionResult armadilha = new ActionResult() {Descricao = "ARMADILHA!"};
                result.Add(armadilha);

                //TODO: Remover singleton!
                //IArmadilha armadilha = ArmadilhaFactory.GeraArmadilha(Porta.Masmorra, 1);
                //descricao = $"\n  {armadilha.Efeito(Personagem)}";
            }

            BaseSegmento segmentoAtual = Porta.SegmentoAtual;
            List<IEscolha> escolhas = segmentoAtual.RecuperaTodasAsEscolhas();
            ActionResult dungeonConsequence = new DungeonConsequence()
            {
                Descricao = $"\n  A porta está {estado}",
                Segment = segmentoAtual,
                //Escolhas = escolhas
            };
            result.Add(dungeonConsequence);

            return result;
        }

        public int? EhEscadariaObrigatoria(IPortaComum porta)
        {
            int floor = porta.Andar;
            var segmentoAlvo = porta.SegmentoAlvo;
            var salaFinalEncontrada = porta.Masmorra.SalaFinal is not null;
            var veioDeEscada = porta.SegmentoAtual.GetType() == typeof(Escadaria);
            if (floor > -2 && Masmorra.QtdPortasInexploradas == 1 && segmentoAlvo is null && !salaFinalEncontrada && !veioDeEscada)
                return 5;
            return IndicePreDefinido;
        }
    }
}
