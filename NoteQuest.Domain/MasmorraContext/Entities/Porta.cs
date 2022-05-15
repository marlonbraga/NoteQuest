using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Porta : IPortaComum
    {
        public int IdPorta { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Posicao Posicao { get; set; }
        public BaseSegmento SegmentoAlvo { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
        public ISegmentoFactory SegmentoFactory { get; }
        public List<IEscolha> Escolhas { get; set; }

        public Porta(IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory)
        {
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
        }

        public Porta(BaseSegmento segmentoAtual, Posicao posicao)
        {
            SegmentoAtual = segmentoAtual;
            Posicao = posicao;
            IAcao acao = new VerificarPorta(this);
            Escolha escolha = new(acao);
            Escolhas = new List<IEscolha>() { escolha };
        }

        public BaseSegmento Entrar()
        {
            return SegmentoAlvo.Entrar(this);
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
                    IAcao acaoQuebrarPorta = new QuebrarPorta();
                    Escolha escolhaQuebrarPorta = new(acaoQuebrarPorta);
                    IAcao acaoAbrirFechadura = new AbrirFechadura();
                    Escolha escolhaAbrirFechadura = new(acaoAbrirFechadura);
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
            IAcao acao = new EntrarPelaPorta(this);
            Escolha escolha = new(acao);
            Escolhas = new List<IEscolha>() { escolha };
        }

        public void QuebrarPorta()
        {
            EstadoDePorta = EstadoDePorta.quebrada;
            Escolhas = AbrirPorta();
        }

        public BaseSegmento ExpiarSala(IPortaComum portaDeEntrada)
        {
            SegmentoAlvo = SegmentoFactory.GeraSegmento(portaDeEntrada, D6.Rolagem(1));
            return SegmentoAlvo;
        }

        public IPortaComum InvertePorta()
        {
            IPortaComum porta = new Porta(MasmorraRepository, SegmentoFactory)
            {
                Posicao = this.Posicao,//TODO: Inverter posição
                EstadoDePorta = this.EstadoDePorta,
                SegmentoAlvo = this.SegmentoAtual,
                SegmentoAtual = this.SegmentoAlvo
            };
            return porta;
        }

        private List<IEscolha> AbrirPorta()
        {
            IAcao acao = new EntrarPelaPorta(this);
            Escolha escolha = new(acao);
            return new List<IEscolha>() { escolha };
        }
    }
}
