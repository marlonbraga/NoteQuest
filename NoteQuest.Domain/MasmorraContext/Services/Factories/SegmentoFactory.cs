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

namespace NoteQuest.Domain.MasmorraContext.Services.Factories
{
    public class SegmentoFactory : ISegmentoFactory
    {
        private IDictionary<int, IMasmorraData> masmorraData;
        private int IndiceMasmorraAtual;

        public SegmentoFactory(IMasmorraRepository masmorraRepository)
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
                foreach (var escolha in porta.Escolhas)
                {
                    if (escolha.Acao.GetType() == typeof(VerificarPorta))
                    {
                        ((VerificarPorta)escolha.Acao).Masmorra = masmorra;
                        escolha.Acao.ChainedEvents["Armadilha"] = masmorra.ArmadilhaFactory.GeraArmadilha(masmorra, indice);
                    }
                }
            }

            switch (indice)
            {
                case 1://Palácio
                    int index = segmento.Portas.FindIndex(s => s.Posicao == Posicao.frente);
                    IEvent novaAcao = new EntrarPelaPorta((IPortaComum)segmento.Portas[index], 5);
                    novaAcao.Titulo = "Descer escadaria";
                    segmento.Portas[index].Escolhas[0].Acao = novaAcao;
                    segmento.Portas[0].EstadoDePorta = EstadoDePorta.aberta;

                    masmorra.QtdPortasInexploradas = 3;
                    break;
                default:
                    break;
            }
            var entradaEmMasmorra = (descricao: masmorraData[indice].Descricao, segmentoInicial: segmento);
            return entradaEmMasmorra;
        }

        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice)
        {
            portaDeEntrada.Masmorra.QtdPortasInexploradas--;

            if (EhSalaFinal(portaDeEntrada))
                return GeraSalaFinal(portaDeEntrada, indice);

            if (EhEscadariaObrigatoria(portaDeEntrada))
                return GerarSegmentoPorSegmento(portaDeEntrada, 5);

            return GerarSegmentoPorSegmento(portaDeEntrada, indice);
        }

        public bool EhEscadariaObrigatoria(IPortaComum porta)
        {
            int floor = porta.Andar;
            var segmentoAlvo = porta.SegmentoAlvo;
            var salaFinalEncontrada = porta.Masmorra.SalaFinal is not null;
            var veioDeEscada = porta.SegmentoAtual.GetType() == typeof(Escadaria);
            if (floor > -2 && porta.Masmorra.QtdPortasInexploradas == 0 && segmentoAlvo is null && !salaFinalEncontrada && !veioDeEscada)
                return true;
            return false;
        }

        public bool EhSalaFinal(IPortaComum portaDeEntrada)
        {
            int andar = portaDeEntrada.Andar;
            BaseSegmento salaFinal = portaDeEntrada.Masmorra.SalaFinal;
            if (andar == -2 && salaFinal is null)
                return true;

            return false;
        }

        public BaseSegmento GeraSalaFinal(IPortaComum portaDeEntrada, int? indice = null)
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

        private (SegmentoTipo segmentoTipo, string descricao, int qtdPortas) TipoSegmento(BaseSegmento segmentoAtual, int indice)
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

        private BaseSegmento GerarSegmentoPorSegmento(IPortaComum portaDeEntrada, int indice)
        {
            BaseSegmento segmento = null;
            BaseSegmento segmentoAtual = portaDeEntrada.SegmentoAtual;

            var segmentoData = TipoSegmento(segmentoAtual, indice);

            switch (segmentoData.segmentoTipo.ToString())
            {
                case "sala":
                    segmento = new Sala(portaDeEntrada, segmentoData.descricao, segmentoData.qtdPortas);
                    segmento = ((Sala)segmento).AdicionaMonstros(GeraMonstros(masmorraData[IndiceMasmorraAtual].TabelaMonstro[D6.Rolagem(2, deslocamento: true)]));
                    segmento = ((Sala)segmento).AdicionaConteudo(GeraConteudo(masmorraData[IndiceMasmorraAtual].TabelaConteudo[D6.Rolagem(2, deslocamento: true)]));
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
