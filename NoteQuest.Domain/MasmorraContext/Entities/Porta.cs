using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.Services;

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
        //public ISegmentoFactory SegmentoFactory { get; }
        public List<IEscolha> Escolhas { get; set; }

        public Porta(IMasmorraRepository masmorraRepository/*, ISegmentoFactory segmentoFactory*/)
        {
            MasmorraRepository = masmorraRepository;
            //SegmentoFactory = segmentoFactory;
        }

        public Porta(BaseSegmento segmentoAtual, Posicao posicao)
        {
            SegmentoAtual = segmentoAtual;
            Posicao = posicao;
            IAcao acao = new VerificarPorta(1,this);
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
                    IAcao acaoQuebrarPorta = new QuebrarPorta(1, this);
                    Escolha escolhaQuebrarPorta = new(acaoQuebrarPorta);
                    IAcao acaoAbrirFechadura = new AbrirFechadura(1, this);
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
            IAcao acao = new EntrarPelaPorta(this/*, SegmentoFactory*/);
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
            IAcao acao = new VerificarPorta(1, this);
            IEscolha escolha = new Escolha(acao);
            List<IEscolha> escolhas = new() { escolha };
            IPortaComum porta = new Porta(MasmorraRepository)
            {
                Posicao = this.Posicao,//TODO: Inverter posição
                EstadoDePorta = this.EstadoDePorta,
                SegmentoAlvo = this.SegmentoAtual,
                SegmentoAtual = this.SegmentoAlvo,
                Escolhas = Escolhas = escolhas
            };
            return porta;
        }

        private List<IEscolha> AbrirPorta()
        {
            IAcao acao = new EntrarPelaPorta(this/*, SegmentoFactory*/);
            Escolha escolha = new(acao);
            return new List<IEscolha>() { escolha };
        }
    }
}
