using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class PortaComum : IPortaComum
    {
        public int IdPorta { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Posicao Posicao { get; set; }
        public BaseSegmento SegmentoAlvo { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
        public List<IEscolha> Escolhas { get; set; }
        public int Andar { get; set; }
        public IMasmorra Masmorra { get; set; }

        public PortaComum(IMasmorraRepository masmorraRepository)
        {
            MasmorraRepository = masmorraRepository;
        }

        public PortaComum(BaseSegmento segmentoAtual, Posicao posicao)
        {
            SegmentoAtual = segmentoAtual;
            Posicao = posicao;
            Masmorra = segmentoAtual.Masmorra;
            IAcao acao = new VerificarPorta(this, 1);
            Escolha escolha = new(acao);
            Escolhas = new List<IEscolha>() { escolha };
            Andar = segmentoAtual.Andar;
        }

        public PortaComum(BaseSegmento segmentoAtual, Posicao posicao, int? indice = null)
        {
            SegmentoAtual = segmentoAtual;
            Posicao = posicao;
            Masmorra = segmentoAtual.Masmorra;
            IAcao acao = new EntrarPelaPorta(this, indice);
            Escolha escolha = new(acao);
            Escolhas = new List<IEscolha>() { escolha };
            Andar = segmentoAtual.Andar;
        }

        public EstadoDePorta VerificarFechadura(int? indice = null)
        {
            switch (indice)
            {
                case 1:
                    Escolhas = AbrirPorta();
                    EstadoDePorta = EstadoDePorta.aberta;
                    //TODO: Gera evento de cair em armadilha
                    break;
                case 2:
                case 3:
                    EstadoDePorta = EstadoDePorta.fechada;
                    IAcao acaoQuebrarPorta = new QuebrarPorta(this, 1);
                    Escolha escolhaQuebrarPorta = new(acaoQuebrarPorta);
                    IAcao acaoAbrirFechadura = new AbrirFechadura(this,1);
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
            //TODO: Fazer a passagem de tempo (perder 1 tocha)
            EstadoDePorta = EstadoDePorta.aberta;
            Escolhas = AbrirPorta();
        }

        public void QuebrarPorta()
        {
            //TODO: Criar evento de Passar o 1º turno SE houver batalha  
            EstadoDePorta = EstadoDePorta.quebrada;
            Escolhas = AbrirPorta();
        }

        public IPortaComum InvertePorta()
        {
            Posicao novaPosicao = Posicao switch
            {
                Posicao.direita => Posicao.esquerda,
                Posicao.esquerda => Posicao.direita,
                Posicao.frente => Posicao.tras,
                Posicao.tras => Posicao.frente,
                _ => throw new System.NotImplementedException()
            };
            IPortaComum porta = new PortaComum(MasmorraRepository)
            {
                Posicao = novaPosicao,
                EstadoDePorta = this.EstadoDePorta,
                SegmentoAlvo = this.SegmentoAtual,
                SegmentoAtual = this.SegmentoAlvo,
                Masmorra = this.SegmentoAtual.Masmorra,
                Andar = this.Andar,
            };
            IAcao acao = new EntrarPelaPorta(porta, null);
            acao.Titulo = $"◄┘ Voltar ({porta.Posicao})";
            IEscolha escolha = new Escolha(acao);
            List<IEscolha> escolhas = new() { escolha };
            porta.Escolhas = escolhas;

            return porta;
        }

        public List<IEscolha> AbrirPorta()
        {
            IAcao acao = new EntrarPelaPorta(this, null);
            Escolha escolha = new(acao);
            return new List<IEscolha>() { escolha };
        }
    }
}
