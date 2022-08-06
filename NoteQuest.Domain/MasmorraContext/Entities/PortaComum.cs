using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class PortaComum : IPortaComum
    {
        public int IdPorta { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Direcao Direcao { get; set; }
        public BaseSegmento SegmentoAlvo { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
        public IList<IEscolha> Escolhas { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }

        public PortaComum(IClasseBasicaRepository masmorraRepository, ISegmentoBuilder segmentoFactory)
        {
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
            Escolhas = new List<IEscolha>();
        }

        public void Build(BaseSegmento segmentoAtual, Direcao direcao)
        {
            SegmentoAtual = segmentoAtual;
            Direcao = direcao;
            IAcao acao = SegmentoFactory.CriarVerificarPortaService(this);
            Escolha escolha = new(acao, null);
            Escolhas = new List<IEscolha>() { escolha };
        }

        public EstadoDePorta VerificarFechadura(int valorD6)
        {
            switch (valorD6)
            {
                case 1:
                    Escolhas = AbrirPorta();
                    EstadoDePorta = EstadoDePorta.aberta;
                    //TODO: Gera evento de cair em armadilha
                    break;
                case 2:
                case 3:
                    EstadoDePorta = EstadoDePorta.fechada;
                    IAcao acaoQuebrarPorta = SegmentoFactory.CriarQuebrarPortaService(this);
                    Escolha escolhaQuebrarPorta = new(acaoQuebrarPorta, null);
                    IAcao acaoAbrirFechadura = SegmentoFactory.CriarAbrirFechaduraService(this);
                    Escolha escolhaAbrirFechadura = new(acaoAbrirFechadura, null);
                    Escolhas = new List<IEscolha>() { escolhaQuebrarPorta, escolhaAbrirFechadura };
                    break;
                case 4:
                case 5:
                case 6:
                    Escolhas = AbrirPorta();
                    EstadoDePorta = EstadoDePorta.aberta;
                    break;
            }
            return EstadoDePorta;
        }

        public void AbrirFechadura()
        {
            EstadoDePorta = EstadoDePorta.aberta;
            IAcao acao = SegmentoFactory.CriarEntrarPelaPortaService(this);
            Escolha escolha = new(acao, null);
            Escolhas = new List<IEscolha>() { escolha };
        }

        public void QuebrarPorta()
        {
            EstadoDePorta = EstadoDePorta.quebrada;
            Escolhas = AbrirPorta();
        }

        public IPortaComum InvertePorta()
        {
            IPortaComum porta = new PortaComum(MasmorraRepository, SegmentoFactory)
            {
                Direcao = (int)Direcao >= 2 ? Direcao - 2 : Direcao + 2,
                EstadoDePorta = this.EstadoDePorta,
                SegmentoAlvo = this.SegmentoAtual,
                SegmentoAtual = this.SegmentoAlvo
            };
            IAcao acao = SegmentoFactory.CriarEntrarPelaPortaService(porta);
            acao.Titulo = "▲ " + acao.Titulo;
            IEscolha escolha = new Escolha(acao, null);
            List<IEscolha> escolhas = new() { escolha };
            porta.Escolhas = escolhas;

            return porta;
        }

        private IList<IEscolha> AbrirPorta()
        {
            IAcao acao = SegmentoFactory.CriarEntrarPelaPortaService(this);
            Escolha escolha = new(acao, null);
            return new List<IEscolha>() { escolha };
        }
    }
}
