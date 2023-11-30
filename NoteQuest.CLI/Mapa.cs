using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteQuest.CLI
{
    public class Mapa
    {
        private static string ParedeSimples = "██";
        private static string PortaFrenteTras = "▬▬";
        private static string PortaEsquerda = "▐ ";
        private static string PortaDireita = " ▌";
        private static string PortaInverificada = "??";
        private static string PortaTrancada = "XX";
        private static string PortaQuebrada = "  ";
        private static string MonstroVivo = "ᴥ";
        private static string MonstroMorto = "ᴕ";
        private static string OpcaoDeVasculhar = "●";

        private static IDictionary<(string,int), StringBuilder[]> mapaGeral;

        public static void DesenharSala(BaseSegmento segmento)
        {
            if (mapaGeral is null)
                mapaGeral = new Dictionary<(string,int), StringBuilder[]>();

            int altura = 7;
            int largura = 7;
            int qtdMonstros = 0;
            if (segmento.GetType() == typeof(PortaEntrada))
                return;
            if (segmento.GetType() == typeof(Sala))
            {
                altura = 7;
                largura = 7;
                qtdMonstros = ((Sala)segmento).Monstros?.Count ?? 0;
            }
            if (segmento.GetType() == typeof(Corredor))
            {
                altura = 7;
                largura = 3;
            }
            if (segmento.GetType() == typeof(Escadaria))
            {
                altura = 9;
                largura = 3;
            }

            StringBuilder[] sala;
            if (mapaGeral.ContainsKey((segmento.Masmorra.Nome, segmento.IdSegmento)))
                sala = mapaGeral[(segmento.Masmorra.Nome, segmento.IdSegmento)];
            else
            {
                sala = CriarSala(altura, largura);
                foreach (var porta in segmento.Portas)
                {
                    sala = CriarPortas(altura, largura, sala, porta.Posicao);
                }
                sala = CriarMonstros(qtdMonstros, sala, true);
            }

            foreach (var porta in segmento.Portas)
            {
                switch (porta.EstadoDePorta)
                {
                    case EstadoDePorta.aberta:
                        sala = AbrirPorta(sala, porta.Posicao);
                        break;
                    case EstadoDePorta.fechada:
                        sala = VerificaPorta(sala, porta.Posicao, false);
                        break;
                    case EstadoDePorta.quebrada:
                        sala = QuebrarPorta(sala, porta.Posicao);
                        break;
                    default:
                        break;
                }
            }

            sala = MatarInimigo(sala, qtdMonstros);

            mapaGeral[(segmento.Masmorra.Nome, segmento.IdSegmento)] = sala;
            DesenharSala(sala);
        }

        public static StringBuilder[] CriarSala(int altura, int largura)
        {
            StringBuilder[] result = new StringBuilder[altura];

            for (int i = 0; i < altura; i++)
            {
                result[i] = new StringBuilder("");
                for (int j = 0; j < largura; j++)
                {
                    if (i == 0 || j == 0 || i == altura - 1 || j == largura - 1)
                        result[i].Append(ParedeSimples);
                    else
                        result[i].Append("  ");
                }
            }

            return result;
        }

        public static StringBuilder[] VerificaPorta(StringBuilder[] sala, Posicao parede = Posicao.frente, bool aberta = true) //frente-esq-dir-tras
        {
            string novaPorta = aberta ? null : PortaTrancada;
            switch (parede)
            {
                case Posicao.frente:
                    sala[0] = new StringBuilder(sala[0].ToString().Replace(PortaInverificada, novaPorta ?? PortaFrenteTras));
                    break;
                case Posicao.esquerda:
                    for (int I = 0; I < sala.Length; I++)
                    {
                        if (!sala[I][0].ToString().Contains(PortaInverificada[0]))
                            continue;

                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaInverificada, novaPorta ?? PortaEsquerda));
                        break;
                    }
                    break;
                case Posicao.direita:
                    for (int I = 1; I < sala.Length; I++)
                    {
                        if (!sala[I][sala.Length - 2].ToString().Contains(PortaInverificada[0]))
                            continue;

                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaInverificada, novaPorta ?? PortaDireita));
                        break;
                    }
                    break;
                default:
                    sala[sala.Length - 1] = new StringBuilder(sala[sala.Length - 1].ToString().Replace(PortaInverificada, novaPorta ?? PortaFrenteTras));
                    break;
            }

            return sala;
        }

        public static StringBuilder[] QuebrarPorta(StringBuilder[] sala, Posicao parede = Posicao.frente) //frente-esq-dir-tras
        {
            switch (parede)
            {
                case Posicao.frente:
                    sala[0] = new StringBuilder(sala[0].ToString().Replace(PortaInverificada, PortaQuebrada));
                    sala[0] = new StringBuilder(sala[0].ToString().Replace(PortaTrancada, PortaQuebrada));
                    break;
                case Posicao.esquerda:
                    for (int I = 0; I < sala.Length; I++)
                    {
                        if (!sala[I][0].ToString().Contains(PortaInverificada[0]) && !sala[I][0].ToString().Contains(PortaTrancada[0]))
                            continue;

                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaInverificada, PortaQuebrada));
                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaTrancada, PortaQuebrada));
                        break;
                    }
                    break;
                case Posicao.direita:
                    for (int I = 1; I < sala.Length; I++)
                    {
                        if (!sala[I][sala.Length - 2].ToString().Contains(PortaInverificada[0]) && !sala[I][sala.Length - 2].ToString().Contains(PortaTrancada[0]))
                            continue;

                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaInverificada, PortaQuebrada));
                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaTrancada, PortaQuebrada));
                        break;
                    }
                    break;
                default:
                    sala[sala.Length - 1] = new StringBuilder(sala[sala.Length - 1].ToString().Replace(PortaInverificada, PortaQuebrada));
                    sala[sala.Length - 1] = new StringBuilder(sala[sala.Length - 1].ToString().Replace(PortaTrancada, PortaQuebrada));
                    break;
            }

            return sala;
        }

        public static StringBuilder[] AbrirPorta(StringBuilder[] sala, Posicao parede = Posicao.frente) //frente-esq-dir-tras
        {
            switch (parede)
            {
                case Posicao.frente:
                    sala[0] = new StringBuilder(sala[0].ToString().Replace(PortaInverificada, PortaFrenteTras));
                    sala[0] = new StringBuilder(sala[0].ToString().Replace(PortaTrancada, PortaFrenteTras));
                    break;
                case Posicao.esquerda:
                    for (int I = 0; I < sala.Length; I++)
                    {
                        if (!sala[I][0].ToString().Contains(PortaInverificada[0]) && !sala[I][0].ToString().Contains(PortaTrancada[0]))
                            continue;

                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaInverificada, PortaEsquerda));
                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaTrancada, PortaEsquerda));
                        break;
                    }
                    break;
                case Posicao.direita:
                    for (int I = 1; I < sala.Length; I++)
                    {
                        if (!sala[I][sala.Length - 2].ToString().Contains(PortaInverificada[0]) && !sala[I][sala.Length - 2].ToString().Contains(PortaTrancada[0]))
                            continue;

                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaInverificada, PortaDireita));
                        sala[I] = new StringBuilder(sala[I].ToString().Replace(PortaTrancada, PortaDireita));
                        break;
                    }
                    break;
                default:
                    sala[sala.Length - 1] = new StringBuilder(sala[sala.Length - 1].ToString().Replace(PortaInverificada, PortaFrenteTras));
                    sala[sala.Length - 1] = new StringBuilder(sala[sala.Length - 1].ToString().Replace(PortaTrancada, PortaFrenteTras));
                    break;
            }

            return sala;
        }

        public static StringBuilder[] CriarPortas(int altura, int largura, StringBuilder[] result, Posicao parede = Posicao.frente)
        {
            Random random = new();
            int position;
            switch (parede)
            {
                case Posicao.frente:
                    position = random.Next(2, (largura * 2) - 4);
                    result[0][position] = PortaInverificada[0];
                    result[0][position + 1] = PortaInverificada[1];
                    break;
                case Posicao.esquerda:
                    position = random.Next(1, altura - 1);
                    result[position][0] = PortaInverificada[0];
                    result[position][1] = PortaInverificada[1];
                    break;
                case Posicao.direita:
                    position = random.Next(1, altura - 1);
                    result[position][(largura * 2) - 2] = PortaInverificada[0];
                    result[position][(largura * 2) - 1] = PortaInverificada[1];
                    break;
                default:
                    //Porta tras
                    position = random.Next(2, (largura * 2) - 4);
                    result[altura - 1][position] = PortaInverificada[0];
                    result[altura - 1][position + 1] = PortaInverificada[1];
                    break;
            }

            return result;
        }

        public static StringBuilder[] CriarMonstros(int qtdMonstros, StringBuilder[] sala, bool haConteudo = false)
        {
            //Pegar todos os lugares vagos
            int altura = sala.Length;
            int largura = sala[0].Length;
            List<(int, int)> lugaresVagos = new(altura * largura);
            for (int i = 0; i < altura; i++)
            {
                for (int j = 2; j < largura - 2; j++)
                {
                    if (sala[i][j] == ' ')
                    {
                        lugaresVagos.Add((i, j));
                    }
                }
            }
            Shuffle(lugaresVagos);

            //Substituir espaço vago por Conteúdo e Monstros 
            if (haConteudo)
                sala[lugaresVagos[0].Item1][lugaresVagos[0].Item2] = OpcaoDeVasculhar[0];
            for (int i = 1; lugaresVagos.Count > 0 && qtdMonstros > 0; i++, qtdMonstros--)
            {
                sala[lugaresVagos[i].Item1][lugaresVagos[i].Item2] = MonstroVivo[0];
            }

            return sala;
        }

        private static void Shuffle<T>(IList<T> list)
        {
            Random random = new();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void DesenharSala(StringBuilder[] sala)
        {
            string linha;
            for (int i = 0; i < sala.Length; i++)
            {
                linha = sala[i].ToString().Replace(MonstroVivo, $"[red]{MonstroVivo}[/]");
                linha = linha.Replace(OpcaoDeVasculhar, $"[white]{OpcaoDeVasculhar}[/]");
                AnsiConsole.MarkupLine(linha);
            }
        }

        public static StringBuilder[] MatarInimigo(StringBuilder[] sala, int qtdMonstroVivo = 1)
        {
            for (int i = 0; i < sala.Length; i++)
            {
                if (sala[i].ToString().Contains(MonstroVivo) && qtdMonstroVivo > 0)
                {
                    qtdMonstroVivo--;
                    continue;
                }

                sala[i] = sala[i].Replace(MonstroVivo, $"[grey]{MonstroMorto}[/]");
            }
            return sala;
        }
    }
}
