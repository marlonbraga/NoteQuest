using Alba.CsConsoleFormat;
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
        private static string PortaQuebrada = "░░";
        private static string MonstroVivo = "ᴥ";
        private static string MonstroMorto = "ᴕ";
        private static string OpcaoDeVasculhar = "●";

        private static IDictionary<(string,int), StringBuilder[]> mapaGeral;

        public static string DesenharSala(BaseSegmento segmento)
        {
            string escada = @"██████/n██  ██/n██≡≡██/n██≡≡██/n██≡≡██/n██≡≡██/n██≡≡██/n██≡≡██/n██≡≡██/n██≡≡██/n██≡≡██/n██≡≡██/n██  ██/n██████";
            string[] escadaArray = escada.Split("/n");
            StringBuilder[] escada0 = new StringBuilder[escadaArray.Length];
            for (int i = 0; i < escadaArray.Length; i++)
            {
                escada0[i] = new StringBuilder(escadaArray[i].Replace("\n", ""));
            }

            if (mapaGeral is null)
            {
                mapaGeral = new Dictionary<(string, int), StringBuilder[]>();
                string salaInicial = @"█████████████████████████████████/n██           ║[#444]▓▓▓▓▓[/]║           ██/n██    [#333]██[/]     ║[#555]▒▒▒▒▒[/]║     [#333]██[/]    ██/n██           ║[#666]░░░░░[/]║           ██/n██           ║[#222]░░░░░[/]║           ██/n██              [cyan]▲[/]              ██/n██                             ██/n??  [cyan]◄[/]                       [cyan]►[/]  ??/n██                             ██/n██                             ██/n██    [#333]██[/]                 [#333]██[/]    ██/n██              [bold yellow]▼[/]              ██/n██████████████▬▬▬▬▬██████████████";
                string[] salaArray = salaInicial.Split("/n");
                StringBuilder[] sala0 = new StringBuilder[salaArray.Length];
                for (int i = 0; i < salaArray.Length; i++)
                {
                    sala0[i] = new StringBuilder(salaArray[i].Replace("\n",""));
                }
                mapaGeral[(segmento.Masmorra.Nome, segmento.IdSegmento)] = sala0;
            }

            int altura = 7;
            int largura = 7;
            bool ehSalaFinal = segmento.Masmorra.SalaFinal == segmento;
            bool ehEscada = false;
            StringBuilder[] salaF = null;
            if (ehSalaFinal)
            {
                string salaFinal = @"████████████████████████████████/n██                            ██/n██    ██                ██    ██/n██                            ██/n██                            ██/n██                            ██/n██    ██                ██    ██/n██                            ██/n████████████████████████████████";
                string[] salaArray = salaFinal.Split("/n");
                salaF = new StringBuilder[salaArray.Length];
                for (int i = 0; i < salaArray.Length; i++)
                {
                    salaF[i] = new StringBuilder(salaArray[i].Replace("\n", ""));
                }
            }

            int qtdMonstros = 0;

            if (segmento.GetType() == typeof(PortaEntrada))
                return string.Empty;
            if (segmento.GetType() == typeof(Sala))
            {
                if(segmento.Descricao.Contains("SALA FINAL")) { altura = 9; largura = 16; }
                else if(segmento.Descricao.Contains("Pequena sala")) { altura = 5; largura = 4; }
                else if(segmento.Descricao.Contains("Sala mediana")) { altura = 7; largura = 7; }
                else if(segmento.Descricao.Contains("Sala comprida")) { altura = 9; largura = 5; }
                else if (segmento.Descricao.Contains("Grande salão")) { altura = 9; largura = 9; }
                else if (segmento.Descricao.Contains("Salão comprido")) { altura = 12; largura = 7; }
                else if (segmento.Descricao.Contains("Grande salão com pilares")) { altura = 12; largura = 9; }
                else { altura = 7; largura = 7; }
                qtdMonstros = ((Sala)segmento).Monstros?.Count ?? 0;
            }

            if (segmento.GetType() == typeof(Corredor)) { altura = 7; largura = 3; }
            if (segmento.GetType() == typeof(Escadaria)) { altura = 14; largura = 3; ehEscada = true; }

            StringBuilder[] sala;
            if (mapaGeral.ContainsKey((segmento.Masmorra.Nome, segmento.IdSegmento)))
                sala = mapaGeral[(segmento.Masmorra.Nome, segmento.IdSegmento)];
            else
            {
                sala = ehSalaFinal ? salaF : ehEscada ? escada0 : CriarSala(altura, largura);
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
                }
            }

            sala = MatarInimigo(sala, qtdMonstros);

            mapaGeral[(segmento.Masmorra.Nome, segmento.IdSegmento)] = sala;
            string cor = "grey";
            if(segmento.IdSegmento == 0)
                cor = "#daa520";
            if(segmento.Masmorra.SalaFinal == segmento)
                cor = "red";
            return DesenharSala(sala, cor);
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
                        if (!sala[I][0]
                                .ToString()
                                .Contains(PortaInverificada[0]))
                            continue;
                        sala[I] = new StringBuilder(novaPorta + sala[I].ToString().Substring(2));
                        break;
                    }     
                    break;
                case Posicao.direita:
                    for (int I = 1; I < sala.Length; I++)
                    {
                        var stringo = sala[I][sala[I].Length - 1].ToString();
                        if (!sala[I][sala[I].Length - 1]
                                .ToString()
                                .Replace("[#daa520]", "")
                                .Replace("[cyan]", "")
                                .Replace("[#333]", "")
                                .Replace("[bold yellow]", "")
                                .Replace("[#444]", "")
                                .Replace("[#555]", "")
                                .Replace("[#666]", "")
                                .Replace(@"[/]", "")
                                .Contains(PortaInverificada[0]))
                            continue;
                        string linha = sala[I].ToString();
                        sala[I] = new StringBuilder(sala[I].ToString().Substring(0, linha.Length - 2) + novaPorta);
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
                        if (!sala[I][1].ToString().Contains(PortaInverificada[0]) && !sala[I][0].ToString().Contains(PortaTrancada[0]))
                            continue;

                        sala[I] = new StringBuilder(PortaQuebrada + sala[I].ToString().Substring(2));
                        break;
                    }
                    break;
                case Posicao.direita:
                    for (int I = 1; I < sala.Length; I++)
                    {
                        if (!sala[I][sala[I].Length - 2].ToString().Contains(PortaInverificada[0]) && !sala[I][sala[I].Length - 2].ToString().Contains(PortaTrancada[0]))
                            continue;

                        string linha = sala[I].ToString();
                        sala[I] = new StringBuilder(sala[I].ToString().Substring(0, linha.Length - 2) + PortaQuebrada);
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

                        sala[I] = new StringBuilder(PortaEsquerda + sala[I].ToString().Substring(2));
                        break;
                    }
                    break;
                case Posicao.direita:
                    for (int I = 1; I < sala.Length; I++)
                    {
                        if (!sala[I][sala[I].Length - 2].ToString().Contains(PortaInverificada[0]) && !sala[I][sala[I].Length - 2].ToString().Contains(PortaTrancada[0]))
                            continue;

                        string linha = sala[I].ToString();
                        sala[I] = new StringBuilder(sala[I].ToString().Substring(0, linha.Length - 2) + PortaDireita);
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

        public static string DesenharSala(StringBuilder[] sala, string cor = "grey")
        {
            string result = string.Empty;
            string linha;
            for (int i = 0; i < sala.Length; i++)
            {
                linha = sala[i].ToString().Replace(MonstroVivo, $"[red]{MonstroVivo}[/]");
                linha = linha.Replace(OpcaoDeVasculhar, $"[white]{OpcaoDeVasculhar}[/]");
                result += linha + "\n";
            }
            result = result.Replace(PortaFrenteTras[0].ToString(), $"[green]{PortaFrenteTras[0]}[/]");
            result = result.Replace(PortaEsquerda, $"[green]{PortaEsquerda}[/]");
            result = result.Replace(PortaDireita, $"[green]{PortaDireita}[/]");
            result = result.Replace(PortaInverificada, $"[yellow]{PortaInverificada}[/]");
            result = result.Replace(PortaTrancada, $"[red]{PortaTrancada}[/]");
            result = result.Replace(PortaQuebrada[0].ToString(), $"[green]{PortaQuebrada[0]}[/]");
            return $"{result}";
            //return $"[{cor}]{result}[/]";
        }

        public static string DesenharLinhaDeSala(string linhaDeSala)
        {
            linhaDeSala = linhaDeSala.Replace(MonstroVivo, $"[red]{MonstroVivo}[/]");
            linhaDeSala = linhaDeSala.Replace(OpcaoDeVasculhar, $"[white]{OpcaoDeVasculhar}[/]");
 
            return linhaDeSala;
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
