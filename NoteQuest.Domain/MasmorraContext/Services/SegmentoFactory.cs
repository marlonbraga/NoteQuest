using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;

namespace NoteQuest.Domain.MasmorraContext.Services
{
    public class SegmentoFactory
    {
        private static IDictionary<int, IMasmorraData> masmorraData;
        private static SegmentoFactory Singleton;
        private static int tipoMasmorraAtual;

        public static SegmentoFactory Instancia(IMasmorraRepository masmorraRepository)
        {//TODO: Remover a opcionalidade do indice;
            if (Singleton is null)
            {
                Singleton = new SegmentoFactory(masmorraRepository);
            }
            return Singleton;
        }

        private SegmentoFactory(IMasmorraRepository masmorraRepository)
        {
            masmorraData = new Dictionary<int, IMasmorraData>();
            masmorraData[1] = masmorraRepository.PegarDadosMasmorra("Palacio");
            masmorraData[2] = masmorraRepository.PegarDadosMasmorra("Palacio");
            masmorraData[3] = masmorraRepository.PegarDadosMasmorra("Palacio");
            masmorraData[4] = masmorraRepository.PegarDadosMasmorra("Palacio");
            masmorraData[5] = masmorraRepository.PegarDadosMasmorra("Palacio");
            masmorraData[6] = masmorraRepository.PegarDadosMasmorra("Palacio");
        }

        public (string descricao, BaseSegmento segmentoInicial) GeraSegmentoInicial(int indice = 1)
        {
            tipoMasmorraAtual = indice;
            BaseSegmento segmento = masmorraData[tipoMasmorraAtual].SegmentoInicial;
            switch (indice)
            {
                case 1:
                    IAcao novaAcao = new EntrarPelaPorta((IPortaComum)segmento.Portas[0], 5);
                    novaAcao.Titulo = "Descer escadaria e verificar porta da frente";
                    segmento.Portas[0].Escolhas[0].Acao = novaAcao;
                    break;
                default:
                    break;
            }    
            var entradaEmMasmorra = (descricao: masmorraData[indice].Descricao, segmentoInicial:segmento);
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
            try
            {
                switch (segmentoAtual.GetType().Name)
                {
                    case "Sala":
                        return masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento;
                    case "Corredor":
                        return masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeCorredor[indice].Segmento;
                    case "Escadaria":
                        return masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Segmento;
                    default:
                        return masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento;
                }
            }
            catch
            {
                new Exception($"indice: {indice}");
            }

            return SegmentoTipo.escadaria;
        }

        private static BaseSegmento GerarSegmentoPorSegmento(IPortaComum portaDeEntrada, SegmentoTipo tipoSegmento, int indice)
        {
            BaseSegmento segmento = null;
            string descricao = null;
            int qtdPortas = 0;
            switch (tipoSegmento.ToString())
            {
                case "sala":
                    descricao = masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeSala[indice].Descricao;
                    qtdPortas = masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeSala[indice].QtdPortas;
                    segmento = new Sala(portaDeEntrada, descricao, qtdPortas);
                    segmento = ((Sala)segmento).AdicionaMonstros(GeraMonstros((TabelaMonstro)masmorraData[tipoMasmorraAtual].TabelaMonstro[D6.Rolagem(2, deslocamento: true)]));
                    break;
                case "corredor":
                    descricao = masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeCorredor[indice].Descricao;
                    qtdPortas = masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeCorredor[indice].QtdPortas;
                    segmento = new Corredor(portaDeEntrada, descricao, qtdPortas);
                    break;
                case "escadaria":
                    descricao = masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Descricao;
                    qtdPortas = masmorraData[tipoMasmorraAtual].TabelaSegmentos.TabelaAPartirDeEscadaria[indice].QtdPortas;
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
