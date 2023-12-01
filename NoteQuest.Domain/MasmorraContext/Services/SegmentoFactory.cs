using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;

namespace NoteQuest.Domain.MasmorraContext.Services
{
    public class SegmentoFactory
    {
        private static IDictionary<int, IMasmorraData> masmorraData;
        private static SegmentoFactory Singleton;
        private static int IndiceMasmorraAtual;

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

        public (string descricao, BaseSegmento segmentoInicial) GeraSegmentoInicial(IMasmorra masmorra, int indice = 1)
        {
            IndiceMasmorraAtual = indice;
            BaseSegmento segmento = masmorraData[IndiceMasmorraAtual].SegmentoInicial;
            segmento.Masmorra = masmorra;
            foreach (var porta in segmento.Portas)
            {
                porta.Masmorra = masmorra;
            }
            switch (indice)
            {
                case 1:
                    IAcao novaAcao = new EntrarPelaPorta((IPortaComum)segmento.Portas[0], 5);
                    novaAcao.Titulo = "Descer escadaria";
                    segmento.Portas[0].Escolhas[0].Acao = novaAcao;
                    masmorra.QtdPortasInexploradas = 3;
                    break;
                default:
                    break;
            }    
            var entradaEmMasmorra = (descricao: masmorraData[indice].Descricao, segmentoInicial:segmento);
            return entradaEmMasmorra;
        }

        public static BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice)
        {
            portaDeEntrada.Masmorra.QtdPortasInexploradas--;

            if (EhSalaFinal(portaDeEntrada))
                return GeraSalaFinal(portaDeEntrada, indice);

            return GerarSegmentoPorSegmento(portaDeEntrada, indice);
        }

        public static bool EhSalaFinal(IPortaComum portaDeEntrada)
        {
            int andar = portaDeEntrada.Andar;
            BaseSegmento salaFinal = portaDeEntrada.Masmorra.SalaFinal;
            if (andar == -2 && salaFinal is null)
                return true;

            return false;
        }

        public static BaseSegmento GeraSalaFinal(IPortaComum portaDeEntrada, int? indice = null)
        {
            indice ??= D6.Rolagem(1, true);
            portaDeEntrada.Masmorra.QtdPortasInexploradas--;

            string descricao = "[red]SALA FINAL[/]\nA Grande sala sem portas e cheia de tesouros guardados pelo chefe da masmorra.\n";
            descricao += masmorraData[IndiceMasmorraAtual].TabelaChefeDaMasmorra[(int)indice].Descricao;
            int qtdPortas = 0;
            BaseSegmento segmento = new Sala(portaDeEntrada, descricao, qtdPortas);
            segmento.DetalhesDescricao = masmorraData[IndiceMasmorraAtual].TabelaChefeDaMasmorra[(int)indice].Descricao;
            portaDeEntrada.Masmorra.SalaFinal = segmento;
            //segmento = ((Sala)segmento).AdicionaMonstros(GeraChefe((TabelaChefeDaMasmorra)masmorraData[IndiceMasmorraAtual].TabelaChefeDaMasmorra[D6.Rolagem(1, deslocamento: true)]));

            return segmento;
        }

        private static (SegmentoTipo segmentoTipo, string descricao, int qtdPortas) TipoSegmento(BaseSegmento segmentoAtual, int indice)
        {
            string descricao;
            int qtdPortas;
            SegmentoTipo segmento;
            try
            {
                switch (segmentoAtual.GetType().Name)
                {
                    case "Corredor":
                        descricao = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeCorredor[indice].Descricao;
                        qtdPortas = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeCorredor[indice].QtdPortas;
                        segmento = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeCorredor[indice].Segmento;
                        return (segmento, descricao, qtdPortas);
                    case "Escadaria":
                        descricao = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Descricao;
                        qtdPortas = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeEscadaria[indice].QtdPortas;
                        segmento = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Segmento;
                        return (segmento, descricao, qtdPortas);
                    default:
                    case "Sala":
                        descricao = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeSala[indice].Descricao;
                        qtdPortas = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeSala[indice].QtdPortas;
                        segmento = masmorraData[IndiceMasmorraAtual].TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento;
                        return (segmento, descricao, qtdPortas);
                }
            }
            catch
            {
                new Exception($"indice: {indice}");
            }

            return (SegmentoTipo.escadaria, "-", 0);
        }

        private static BaseSegmento GerarSegmentoPorSegmento(IPortaComum portaDeEntrada, int indice)
        {
            BaseSegmento segmento = null;
            BaseSegmento segmentoAtual = portaDeEntrada.SegmentoAtual;

            var segmentoData = TipoSegmento(segmentoAtual, indice);

            switch (segmentoData.segmentoTipo.ToString())
            {
                case "sala":
                    segmento = new Sala(portaDeEntrada, segmentoData.descricao, segmentoData.qtdPortas);
                    segmento = ((Sala)segmento).AdicionaMonstros(GeraMonstros((TabelaMonstro)masmorraData[IndiceMasmorraAtual].TabelaMonstro[D6.Rolagem(2, deslocamento: true)]));
                    segmento = ((Sala)segmento).AdicionaConteudo(GeraConteudo((TabelaConteudo)masmorraData[IndiceMasmorraAtual].TabelaConteudo[D6.Rolagem(2, deslocamento: true)]));
                    break;
                case "corredor":
                    segmento = new Corredor(portaDeEntrada, segmentoData.descricao, segmentoData.qtdPortas);
                    break;
                case "escadaria":
                    segmento = new Escadaria(portaDeEntrada, segmentoData.descricao, segmentoData.qtdPortas);
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

        private static List<Monstro> GeraChefe(TabelaChefeDaMasmorra tabelaChefeDaMasmorra)
        {
            Monstro monstro = new(tabelaChefeDaMasmorra.Nome, tabelaChefeDaMasmorra.Dano, tabelaChefeDaMasmorra.Pvs)
            {
                Caracteristicas = new string[] { tabelaChefeDaMasmorra.Descricao }
            };
            List<Monstro> monstros = new();
            for (int i = 0; i < tabelaChefeDaMasmorra.Qtd; i++)
            {
                monstros.Add(monstro);
            }

            return monstros;
        }

        private static IConteudo GeraConteudo(TabelaConteudo tabelaConteudo)
        {
            return new Conteudo(tabelaConteudo);
        }

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
                    return int.Parse(qtd);
            }
        }
    }
}
