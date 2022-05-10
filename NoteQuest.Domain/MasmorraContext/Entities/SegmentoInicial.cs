using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class SegmentoInicial : BaseSegmento, ISegmentoInicial
    {
        public SegmentoTipo Segmento { get; set; }
        int ISegmentoInicial.Portas { get; set; }

        public SegmentoInicial(IPorta portaDeEntrada, int qtdPortas, string descricao, IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory) : base(null, descricao)
        {
            IPortaComum porta;
            for (int i = 0; i < qtdPortas; i++)
            {
                porta = new Porta(masmorraRepository, segmentoFactory)
                {
                    Posicao = Posicao.atras,
                    SegmentoAlvo = null
                };
                Portas = new() { porta };
            }
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
