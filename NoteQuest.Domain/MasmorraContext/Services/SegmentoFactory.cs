﻿using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;
using BaseSegmento = NoteQuest.Domain.MasmorraContext.Entities.BaseSegmento;

namespace NoteQuest.Domain.MasmorraContext.Services
{
    public class SegmentoFactory
    {
        private static MasmorraDataDTO masmorraData;
        private static SegmentoFactory Singleton;

        public static SegmentoFactory Instancia(IMasmorraRepository masmorraRepository, int D6 = 1)
        {
            if (Singleton is null)
            {
                Singleton = new SegmentoFactory(masmorraRepository, D6);
            }
            return Singleton;
        }

        private SegmentoFactory(IMasmorraRepository masmorraRepository, int D6 = 1)
        {
            //masmorraData = new(D6);
            masmorraData = masmorraRepository.PegarDadosMasmorra("Palacio");
        }

        public static Tuple<string, BaseSegmento> GeraSegmentoInicial()
        {
            BaseSegmento segmento = (BaseSegmento)masmorraData.SegmentoInicial;
            var entradaEmMasmorra = Tuple.Create(masmorraData.Descricao, segmento);
            return entradaEmMasmorra;
        }

        public static BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice)
        {
            BaseSegmento segmentoAtual = portaDeEntrada.SegmentoAtual;
            SegmentoTipo tipoSegmento = TipoSegmento(segmentoAtual, indice);
            BaseSegmento segmento = GerarSegmentoPorSegmento(portaDeEntrada, tipoSegmento, indice);

            return segmento;
        }

        private static SegmentoTipo TipoSegmento(BaseSegmento segmentoAtual, int indice)
        {
            switch (segmentoAtual.GetType().Name)
            {
                case "Sala":
                    return masmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento;
                case "Corredor":
                    return masmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[indice].Segmento;
                case "Escadaria":
                    return masmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Segmento;
                default:
                    return masmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento;
            }
        }

        private static BaseSegmento GerarSegmentoPorSegmento(IPortaComum portaDeEntrada, SegmentoTipo tipoSegmento, int indice)
        {
            BaseSegmento segmento = null;
            string descricao = null;
            int qtdPortas = 0;
            switch (tipoSegmento.ToString())
            {
                case "sala":
                    descricao = masmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Descricao;
                    qtdPortas = masmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].QtdPortas;
                    segmento = new Sala(portaDeEntrada, descricao, qtdPortas);
                    segmento = ((Sala)segmento).AdicionaMonstros(GeraMonstros((TabelaMonstro)masmorraData.TabelaMonstro[D6.Rolagem(2)]));
                    break;
                case "corredor":
                    descricao = masmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[indice].Descricao;
                    qtdPortas = masmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[indice].QtdPortas;
                    segmento = new Corredor(portaDeEntrada, descricao, qtdPortas);
                    break;
                case "escadaria":
                    descricao = masmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Descricao;
                    qtdPortas = masmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[indice].QtdPortas;
                    segmento = new Escadaria(portaDeEntrada, descricao, qtdPortas);
                    break;
            }
            return segmento;
        }

        private static List<Monstro> GeraMonstros(TabelaMonstro tabelaMonstro)
        {
            Monstro monstro = new(tabelaMonstro.Nome, tabelaMonstro.Dano, tabelaMonstro.Pvs)
            {
                Caracteristicas = new string[] { tabelaMonstro.Caracteristicas }
            };
            List<Monstro> monstros = new();
            for (int i = 0; i < ConverteQtdMonstros(tabelaMonstro.Qtd); i++)
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

        private static int ConverteQtdMonstros(string qtd)
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
                case null:
                    return 0;
                default:
                    return Int32.Parse(qtd);
            }
        }
    }
}