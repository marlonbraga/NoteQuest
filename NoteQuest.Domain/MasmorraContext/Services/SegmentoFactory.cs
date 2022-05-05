using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services
{
    public class SegmentoFactory
    {
        readonly MasmorraBuilder masmorraBuilder;

        public SegmentoFactory(int D6 = 1)
        {
            masmorraBuilder = new(D6);
        }

        public Segmento GeraSegmento(IPorta portaDeEntrada, int indice)
        {
            string tipoSegmento = TipoSegmento(portaDeEntrada.SegmentoAtual, indice);
            Segmento segmento = GerarSegmentoPorSegmento(portaDeEntrada, tipoSegmento);

            return segmento;
        }
        private string TipoSegmento(Segmento segmentoAtual, int indice)
        {
            switch (segmentoAtual.GetType().Name)
            {
                case "Sala":
                    return masmorraBuilder.TabelaSegmentos.TabelaApartirdeSala[indice].segmento;
                case "Corredor":
                    return masmorraBuilder.TabelaSegmentos.TabelaApartirdeSala[indice].segmento;
                case "Escadaria":
                    return masmorraBuilder.TabelaSegmentos.TabelaApartirdeSala[indice].segmento;
                default: return null;
            }
        }
        private Segmento GerarSegmentoPorSegmento(IPorta portaDeEntrada, string tipoSegmento)
        {
            Segmento segmento = null;
            string descricao = null;
            switch (tipoSegmento)
            {
                case "sala":
                    descricao = masmorraBuilder.TabelaSegmentos.TabelaApartirdeSala[6].descricao;
                    segmento = new Sala(portaDeEntrada, descricao);
                    segmento = ((Sala)segmento).AdicionaMonstros(GeraMonstros(masmorraBuilder.TabelaMonstro[12]));
                    //segmento = ((Sala)segmento).AdicionaConteudo(GeraConteudo(masmorraBuilder.TabelaConteúdo[12]));
                    break;
                case "corredor":
                    descricao = masmorraBuilder.TabelaSegmentos.TabelaApartirdeCorredor[1].descricao;
                    segmento = new Corredor(portaDeEntrada, descricao);
                    break;
                case "escadaria":
                    descricao = masmorraBuilder.TabelaSegmentos.TabelaApartirdeEscadaria[1].descricao;
                    segmento = new Escadaria(portaDeEntrada, descricao);
                    break;
            }
            return segmento;
        }
        private List<Monstro> GeraMonstros(TabelaMonstro tabelaMonstro)
        {
            Monstro monstro = new(tabelaMonstro.nome, tabelaMonstro.dano, tabelaMonstro.pvs)
            {
                Caracteristicas = new string[] { tabelaMonstro.caracteristicas }
            };
            List<Monstro> monstros = new();
            for (int i = 0; i < ConverteQtdMonstros(tabelaMonstro.qtd); i++)
            {
                monstros.Add(monstro);
            }

            return monstros;
        }
        //private List<IConteudo> GeraConteudo(TabelaConteudo tabelaConteudo)
        //{
        //    IConteudo conteudo = new(tabelaMonstro.nome, tabelaMonstro.dano, tabelaMonstro.pvs)
        //    {
        //        Caracteristicas = { tabelaMonstro.caracteristicas }
        //    };
        //    List<Monstro> monstros = new();
        //    for (int i = 0; i < tabelaMonstro.qtd; i++)
        //    {
        //        conteudo.Add(monstro);
        //    }

        //    return conteudo;
        //}
        private int ConverteQtdMonstros(string qtd)
        {
            switch (qtd)
            {
                case "1d6":
                case "1D6":
                    return D6.Rolagem(1);
                case "2d6":
                case "2D6":
                    return D6.Rolagem(2);
                case "3d6":
                case "3D6":
                    return D6.Rolagem(3);
                default:
                    return Int32.Parse(qtd);
            }
        }
    }
}
