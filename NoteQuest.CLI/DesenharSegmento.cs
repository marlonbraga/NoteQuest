using NoteQuest.Domain.Core.Interfaces.Masmorra;
using System;
using System.Collections.Generic;

namespace NoteQuest.CLI
{
    public static class DesenharSegmento
    {
        public static string Desenha(ISegmento segmento)
        {
            string result = String.Empty;
            int verticalLimit = segmento.Dimensoes.Item1;
            int horizontalLimit = segmento.Dimensoes.Item2;
            IDictionary<Direcao, Tuple<int, IPorta>> passagens = segmento.Passagens;

            for (int i = 0; i < verticalLimit; i++)
            {
                for (int j = 0; j < horizontalLimit; j++)
                {
                    if (HaPortaAFrente(i, j, passagens))
                        result += "▬▬";//▬▬── │ / ▌▐
                    else if(HaPortaATras(i, j, verticalLimit, passagens))
                        result += "──"; //"─";
                    else if(HaPortaADireita(i, j, horizontalLimit, passagens))
                        result += " ▐"; //"▌";
                    else if(HaPortaAEsquerda(i, j, passagens))
                        result += "▌ "; //"▐";
                    
                    else if (EhCantoSuperiorEsquerdo(i, j))
                        result += "╔═";//╔═
                    else if (EhCantoSuperiorDireito(i, j, horizontalLimit))
                        result += "═╗";//═╗
                    else if (EhCantoInferiorEsquerdo(i, j, verticalLimit))
                        result += "╚═";//╚═
                    else if (EhCantoInferiorDireito(i, j, verticalLimit, horizontalLimit))
                        result += "═╝";//═╝
                    else if (EhParedeInferior(i, j, verticalLimit))
                        result += "══";
                    else if (EhParedeEsquerda(i, j))
                        result += "║ ";
                    else if (EhParedeDireita(i, j, horizontalLimit))
                        result += " ║";
                    else if (EhParedeSuperior(i, j))
                        result += "══";//══
                    else
                        result += "  ";
                }
                result += "\n";
            }

            return result;
        }

        private static bool EhCantoSuperiorEsquerdo(int i, int j)
        {
            return i == 0 && j == 0;
        }
        private static bool EhCantoSuperiorDireito(int i, int j, int horizontalLimit)
        {
            return i == 0 && j == horizontalLimit - 1;
        }
        private static bool EhCantoInferiorEsquerdo(int i, int j, int verticalLimit)
        {
            return i == verticalLimit - 1 && j == 0;
        }
        private static bool EhCantoInferiorDireito(int i, int j, int verticalLimit, int horizontalLimit)
        {
            return i == verticalLimit - 1 && j == horizontalLimit - 1;
        }
        private static bool EhParedeSuperior(int i, int j)
        {
            return i == 0;
        }
        private static bool EhParedeEsquerda(int i, int j)
        {
            return j == 0;
        }
        private static bool EhParedeInferior(int i, int j, int verticalLimit)
        {
            return i == verticalLimit - 1;
        }
        private static bool EhParedeDireita(int i, int j, int horizontalLimit)
        {
            return j == horizontalLimit - 1;
        }

        private static bool HaPortaAFrente(int i, int j, IDictionary<Direcao, Tuple<int, IPorta>> passagens)
        {
            bool result =
                EhExtremidadeDeFrente(i)
                && passagens.TryGetValue(Direcao.frente, out Tuple<int, IPorta> tupla)
                && EhPosicaoCorreta(j, tupla.Item1);
            return result;
        }
        private static bool HaPortaATras(int i, int j, int verticalLimit, IDictionary<Direcao, Tuple<int, IPorta>> passagens)
        {
            bool result =
                EhExtremidadeDeTras(i, verticalLimit)
                && passagens.TryGetValue(Direcao.tras, out Tuple<int, IPorta> tupla)
                && EhPosicaoCorreta(j, tupla.Item1);
            return result;
        }
        private static bool HaPortaADireita(int i, int j, int horizontalLimit, IDictionary<Direcao, Tuple<int, IPorta>> passagens)
        {
            bool result =
                EhExtremidadeDeDireita(j, horizontalLimit)
                && passagens.TryGetValue(Direcao.direita, out Tuple<int, IPorta> tupla)
                && EhPosicaoCorreta(i, tupla.Item1);
            return result;
        }
        private static bool HaPortaAEsquerda(int i, int j, IDictionary<Direcao, Tuple<int, IPorta>> passagens)
        {
            bool result =
                EhExtremidadeDeEsquerda(j)
                && passagens.TryGetValue(Direcao.esquerda, out Tuple<int, IPorta> tupla)
                && EhPosicaoCorreta(i, tupla.Item1);
            return result;
        }

        private static bool EhExtremidadeDeFrente(int i)
        {
            return i == 0;
        }
        private static bool EhExtremidadeDeDireita(int j, int horizontalLimit)
        {
            return j == horizontalLimit - 1;
        }
        private static bool EhExtremidadeDeTras(int i, int verticalLimit)
        {
            return i == verticalLimit - 1;
        }
        private static bool EhExtremidadeDeEsquerda(int j)
        {
            return j == 0;
        }

        private static bool EhPosicaoCorreta(int posicaoAtual, int posicaoDaPorta)
        {
            return posicaoAtual == posicaoDaPorta;
        }
    }
}
