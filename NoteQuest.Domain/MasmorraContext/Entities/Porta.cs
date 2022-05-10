using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
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

        public Porta(IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory)
        {
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
        }

        public BaseSegmento Entrar()
        {
            return SegmentoAlvo.Entrar(this);
        }

        public void VerificarFechadura(int valorD6)
        {
            switch (valorD6)
            {
                case 1:
                    EstadoDePorta = EstadoDePorta.aberta;
                    break;
                case 2:
                case 3:
                    EstadoDePorta = EstadoDePorta.fechada;
                    break;
                case 4:
                case 5:
                case 6:
                    EstadoDePorta = EstadoDePorta.aberta;
                    break;
            }
        }

        public void AbrirFechadura()
        {
            EstadoDePorta = EstadoDePorta.aberta;
        }

        public void QuebrarPorta()
        {
            EstadoDePorta = EstadoDePorta.aberta;
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
    }
}
