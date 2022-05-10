using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.ObjectValue;
using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using BaseSegmento = NoteQuest.Domain.MasmorraContext.Entities.BaseSegmento;

namespace NoteQuest.Domain.MasmorraContext.Services
{
    public class SegmentoFactory : ISegmentoFactory
    {
        readonly IMasmorraData masmorraData;
        readonly IMasmorraRepository MasmorraRepository;

        public SegmentoFactory(IMasmorraRepository masmorraRepository, int D6 = 1)
        {
            //masmorraData = new(D6);
            MasmorraRepository = masmorraRepository;
            masmorraData = masmorraRepository.PegarDadosMasmorra("Palacio");
        }

        public BaseSegmento GeraSegmentoInicial()
        {
            //SegmentoTipo tipoSegmento = TipoSegmento(portaDeEntrada.SegmentoAtual, indice);
            BaseSegmento segmento = (BaseSegmento)masmorraData.SegmentoInicial;

            return segmento;
        }

        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice)
        {
            SegmentoTipo tipoSegmento = TipoSegmento(portaDeEntrada.SegmentoAtual, indice);
            BaseSegmento segmento = GerarSegmentoPorSegmento(portaDeEntrada, tipoSegmento);

            return segmento;
        }

        private SegmentoTipo TipoSegmento(BaseSegmento segmentoAtual, int indice)
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

        private BaseSegmento GerarSegmentoPorSegmento(IPortaComum portaDeEntrada, SegmentoTipo tipoSegmento)
        {
            BaseSegmento segmento = null;
            string descricao = null;
            switch (tipoSegmento.ToString())
            {
                case "sala":
                    descricao = masmorraData.TabelaSegmentos.TabelaAPartirDeSala[6].Descricao;
                    segmento = new Sala(portaDeEntrada, descricao);
                    segmento = ((Sala)segmento).AdicionaMonstros(GeraMonstros((TabelaMonstro)masmorraData.TabelaMonstro[12]));
                    break;
                case "corredor":
                    descricao = masmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[1].Descricao;
                    segmento = new Corredor(portaDeEntrada, descricao);
                    break;
                case "escadaria":
                    descricao = masmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[1].Descricao;
                    segmento = new Escadaria(portaDeEntrada, descricao);
                    break;
            }
            return segmento;
        }
        
        private List<Monstro> GeraMonstros(TabelaMonstro tabelaMonstro)
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
