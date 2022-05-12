using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public enum SegmentoTipo
    {
        sala, corredor, escadaria
    }

    public abstract class BaseSegmento
    {
        public int Nivel { get; }
        public int IdSegmento { get; }
        public string Descricao { get; set; }
        public List<IPorta> Portas { get; set; }

        public BaseSegmento(IPorta portaDeEntrada, string descricao)
        {
            IPorta porta = portaDeEntrada;
            if (portaDeEntrada is IPortaComum)
            {
                porta = ((IPortaComum)portaDeEntrada).InvertePorta();
            }
            Portas = new() { porta };
            Descricao = descricao;
        }

        public BaseSegmento Entrar(IPortaComum portaDeEntrada)
        {
            return this;
        }

        public void DesarmarArmadilhas(int valorD6)
        {

        }

        private void GerarPortas()
        {

        }
    }
}
