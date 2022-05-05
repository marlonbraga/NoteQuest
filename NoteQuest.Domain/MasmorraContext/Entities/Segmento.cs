using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public abstract class Segmento
    {
        public int Nivel { get; }
        public int IdSegmento { get; }
        public string Descricao { get; set; }
        public List<IPorta> Portas { get; set; }

        public Segmento(IPorta portaDeEntrada, string descricao)
        {
            IPorta porta = new Porta()
            {
                Posicao = Posicao.direita,//TODO: Inverter posição
                SegmentoAlvo = portaDeEntrada.SegmentoAtual,
                SegmentoAtual = portaDeEntrada.SegmentoAlvo
            };
            Portas = new() { porta };
            Descricao = descricao;
        }

        public Segmento Entrar(IPorta portaDeEntrada)
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
