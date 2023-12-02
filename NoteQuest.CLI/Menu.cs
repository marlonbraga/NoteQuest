using NoteQuest.Domain.Core;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using NoteQuest.Domain.Core.ObjectValue;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace NoteQuest.CLI
{
    public enum TipoMenu
    {
        None = 0,
        Sala = 1,
        Inventário = 2,
        Magias = 3,
        Porta1 = 4,
        Porta2 = 5,
        Porta3 = 6,
        Porta4 = 7,
        Mochila = 8,
        Equipamentos = 9
    }
    public class Menu
    {
        /*
        //Console.WriteLine("Hello, World!");

        //IDictionary<int, string[]> escolhas = new Dictionary<int, string[]>();
        //escolhas[0] = new []{" x ", "", "(Voltar)" };
        //escolhas[1] = new []{"Goblin Gordo", "●○○", "PV: 1/3" };
        //escolhas[2] = new[] { "Goblin Caolho", "●●●", "PV: 3/3" };
        //escolhas[3] = new[] { "Goblin Feio", "●●○", "PV: 2/3" };
        //escolhas[4] = new[] { "Goblin Caolha", "●●●", "PV: 3/3" };

        //ConsoleColor cor1 = ConsoleColor.DarkRed;
        //ConsoleColor cor2 = ConsoleColor.Red;
        //Console.WriteLine($"Opção {MenuVertical(escolhas, cor1, cor2, cor5: cor1)} escolhida");

        //IDictionary<int, string> escolhasHorizontais = new Dictionary<int, string>();
        //escolhasHorizontais[0] = "Goblin Gordo";
        //escolhasHorizontais[1] = "Goblin Caolho";
        //escolhasHorizontais[2] = "Goblin Feio";
        //escolhasHorizontais[3] = "Goblin Caolha";
        //escolhasHorizontais[4] = " x ";
        //Console.WriteLine($"Opção {MenuHorizontal(escolhasHorizontais)} escolhida");
        */
        public static TipoMenu MenuSegmento(BaseSegmento segmento, ConsoleColor[] cor = null)
        {
            cor = cor ?? new ConsoleColor[]
            {
                ConsoleColor.Gray,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.White,
                ConsoleColor.DarkGray
            };

            string mapa = Mapa.DesenharSala(segmento);
            string[] linhaDeMapa = mapa.Split("\n");

            IDictionary<int, string[]> escolhas = new Dictionary<int, string[]>();
            escolhas[0] = new[] { Mapa.DesenharLinhaDeSala(linhaDeMapa[0]), "[[Esc]]", "Inventário", "" };
            escolhas[1] = new[] { Mapa.DesenharLinhaDeSala(linhaDeMapa[1]), "[[Space]]", "Vasculhar", "" };
            escolhas[2] = new[] { Mapa.DesenharLinhaDeSala(linhaDeMapa[2]), "[[0]]", "Magias", "" };
            int numLinha = 3;
            for (int indexPortas = 0; indexPortas < 4 || numLinha < linhaDeMapa.Length; indexPortas++, numLinha++)
            {
                string linhaDesenhada = string.Empty;
                if (numLinha < linhaDeMapa.Length)
                {
                    linhaDesenhada = Mapa.DesenharLinhaDeSala(linhaDeMapa[numLinha]);
                }
                if (indexPortas < 4)
                {
                    IPorta porta = segmento.Portas.Where(porta => porta.Posicao == (Posicao)indexPortas).FirstOrDefault();
                    if (porta != null)
                        escolhas[numLinha] = new[] { linhaDesenhada, $"[[{indexPortas+1}]][[{Seta(porta.Posicao)}]]", "Porta", porta.EstadoDePorta.ToString() };
                    else
                        numLinha--;
                }
                else
                {
                    escolhas[numLinha] = new[] { linhaDesenhada, "", "", "" };
                }
            }

            int portaIndex = 0;
            

            //Pegar a porta selecionada
            portaIndex = MenuVertical2(escolhas)+3;
            return (TipoMenu)portaIndex;
        }

        public static string Seta(Posicao posicao)
        {
            return posicao switch
            {
                Posicao.frente => "↑",
                Posicao.esquerda => "←",
                Posicao.direita => "→",
                Posicao.tras => "↓",
                _ => "↓",
            };
        }

        public static string Remove(string baseText, string startMark, string finalMark)
        {
            string text = baseText;
            text = baseText.Replace("[[", "{");
            text = text.Replace("]]", "}");
            string auxText;
            do
            {
                auxText = text;
                int start = text.LastIndexOf(startMark) + startMark.Length;
                if (start < 0) break;
                int end = text.IndexOf(finalMark, start);
                if (end < 0) break;
                text = text.Remove(start - 1, end - start + 2);

            } while (text != auxText);

            _ = text.Length;
            return text;
            //[blue][/]  {Dano [red]+4[/]}
        }

        public static int MenuVertical2(IDictionary<int, string[]> escolhas, string[] cores = null)
        {
            string defaultBackgroundColor = cores?[1] ?? "default";
            string corColuna1 = cores?[2] ?? "default";
            string corColuna2 = cores?[3] ?? "#333";
            string corColuna3 = cores?[4] ?? "default";
            bool exibirDescricao = false;
            for (int i = 0; i < escolhas.Count + 2; i++)
                Console.WriteLine();
            var currentLineNumber = Console.CursorTop - (escolhas.Count + 2);
            int selecao = 1;
            int numeroDeEscolha = 0;
            int[] largurDeColuna = new int[5];
            for (int i = 0; i < escolhas.Count; i++)
            {
                for (int j = 0; j < escolhas[i].Length; j++)
                    largurDeColuna[j] = escolhas.Max(x => Remove(x.Value[j], "[", "]").Length);
            }
            do
            {
                Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
                foreach (var escolha in escolhas)
                {
                    //Formata colunas
                    for (int i = 0; i < escolha.Value.Length; i++)
                    {
                        string valor = escolha.Value[i];
                        while (valor.Replace("[[", "[").Replace("]]", "]").Length < largurDeColuna[i])
                            valor += " ";
                        escolha.Value[i] = valor;
                    }

                    //Coluna 1
                    string invert = "";
                    if (selecao == escolha.Key)
                        invert = "invert ";

                    AnsiConsole.Markup($"[{corColuna1} on {defaultBackgroundColor}] {escolha.Value[0]}[/]");

                    //Coluna 2
                    AnsiConsole.Markup($"[{corColuna2}] {escolha.Value[1]}[/] ");

                    //Coluna 3
                    string corColuna4 = corColuna2;
                    if (escolha.Value[3].TrimEnd() == "inverificada") { corColuna4 = "yellow"; }
                    else if (escolha.Value[3].TrimEnd() == "fechada") { corColuna4 = "red"; }
                    else if (escolha.Value[3].TrimEnd() == "aberta") { corColuna4 = "green"; }
                    else if (escolha.Value[3].TrimEnd() == "quebrada") { corColuna4 = "green"; }
                    else corColuna4 = corColuna2;
                    AnsiConsole.Markup($"[{corColuna3}] {escolha.Value[2].Trim()}[/] [{corColuna4}]{escolha.Value[3]}[/] ");

                    //Coluna 4
                    //if (selecao == escolha.Key || exibirDescricao)
                    //    AnsiConsole.Markup($"[{corColuna2}] {escolha.Value[3]}[/] ");
                    AnsiConsole.MarkupLine("");
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        AnsiConsole.Markup("╔");
                        AnsiConsole.Markup(CharacterProfile.ExibirFicha(linhas: (escolhas.Count + 2)));
                        continue;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        numeroDeEscolha = 0;
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.UpArrow:
                        numeroDeEscolha = 1;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.LeftArrow:
                        numeroDeEscolha = 2;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.RightArrow:
                        numeroDeEscolha = 3;
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.DownArrow:
                        numeroDeEscolha = 4;
                        break;
                    default:
                        continue;
                }
                Console.Write($"\r ");
                if (numeroDeEscolha < escolhas.Count)
                    break;
            } while (true);
            return numeroDeEscolha;
        }


        public static int MenuVertical(IDictionary<int, string[]> escolhas, ConsoleColor cor1 = ConsoleColor.Gray, ConsoleColor cor2 = ConsoleColor.Gray, ConsoleColor cor3 = ConsoleColor.DarkGray, ConsoleColor cor4 = ConsoleColor.White, ConsoleColor cor5 = ConsoleColor.DarkGray)
        {
            ConsoleColor defaultForegroundColor = Console.ForegroundColor;
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ConsoleColor corColuna1 = cor1;
            ConsoleColor corColuna2 = cor2;
            ConsoleColor corColuna3 = cor3;
            ConsoleColor corDeSelecaoBg = cor5;
            ConsoleColor corDeSelecaoFg = cor4;
            bool exibirDescricao = false;
            for (int i = 0; i < escolhas.Count + 2; i++)
                Console.WriteLine();
            var currentLineNumber = Console.CursorTop - (escolhas.Count + 2);
            int selecao = 1;
            int numeroDeEscolha;
            int[] largurDeColuna = new int[5];
            for (int i = 0; i < escolhas.Count; i++)
                for (int j = 0; j < escolhas[i].Length; j++)
                    largurDeColuna[j] = escolhas.Max(x => x.Value[j].Length);
            do
            {
                Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
                Console.WriteLine($"  ▲");
                foreach (var escolha in escolhas)
                {
                    if (escolha.Key is not 0)
                    {
                        //Formata colunas
                        for (int i = 0; i < escolha.Value.Length; i++)
                        {
                            string valor = escolha.Value[i];
                            while (valor.Length < largurDeColuna[i])
                                valor += " ";
                            escolha.Value[i] = valor;
                        }
                    }

                    //Índice
                    Console.Write($" [{escolha.Key}] ");

                    //Coluna 1
                    Console.BackgroundColor = selecao == escolha.Key ? corDeSelecaoBg : defaultBackgroundColor;
                    Console.ForegroundColor = selecao == escolha.Key ? corDeSelecaoFg : corColuna1;
                    Console.Write($" {escolha.Value[0]} ");
                    Console.ForegroundColor = defaultForegroundColor;
                    Console.BackgroundColor = defaultBackgroundColor;

                    //Coluna 2
                    Console.ForegroundColor = corColuna2;
                    Console.Write($" {escolha.Value[1]} ");
                    Console.ForegroundColor = defaultForegroundColor;

                    //Coluna 3
                    if (selecao == escolha.Key || exibirDescricao)
                    {
                        Console.ForegroundColor = corColuna3;
                        Console.Write($" {escolha.Value[2]} ");
                        Console.ForegroundColor = defaultForegroundColor;
                    }
                    else
                    {
                        Console.ForegroundColor = defaultBackgroundColor;
                        Console.Write($" {escolha.Value[2]} ");
                        Console.ForegroundColor = defaultForegroundColor;
                    }
                    Console.WriteLine();

                }
                Console.WriteLine($"  ▼");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        Console.Write("╔");
                        AnsiConsole.Markup(CharacterProfile.ExibirFicha(linhas: (escolhas.Count + 2)));
                        continue;
                    case ConsoleKey.UpArrow:
                        selecao = (ushort)Math.Max(0, selecao - 1);
                        continue;
                    case ConsoleKey.DownArrow:
                        selecao = (ushort)Math.Min(escolhas.Count - 1, selecao + 1);
                        continue;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        numeroDeEscolha = selecao;
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        numeroDeEscolha = 0;
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        numeroDeEscolha = 1;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        numeroDeEscolha = 2;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        numeroDeEscolha = 3;
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        numeroDeEscolha = 4;
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        numeroDeEscolha = 5;
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        numeroDeEscolha = 6;
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        numeroDeEscolha = 7;
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        numeroDeEscolha = 8;
                        break;
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        numeroDeEscolha = 9;
                        break;
                    default:
                        continue;
                }
                Console.Write($"\r ");
                if (numeroDeEscolha < escolhas.Count)
                    break;
            } while (true);
            return numeroDeEscolha;
        }

        public static int MenuHorizontal(IDictionary<int, string> escolhas, ConsoleColor cor1 = ConsoleColor.Gray, ConsoleColor cor4 = ConsoleColor.White, ConsoleColor cor5 = ConsoleColor.DarkGray)
        {
            ConsoleColor defaultForegroundColor = Console.ForegroundColor;
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ConsoleColor corColuna1 = cor1;
            ConsoleColor corDeSelecaoBg = cor5;
            ConsoleColor corDeSelecaoFg = cor4;
            bool exibirDescricao = false;

            Console.WriteLine();
            var currentLineNumber = Console.CursorTop - 1;
            int selecao = 0;
            int numeroDeEscolha;
            string invert;
            do
            {
                Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
                AnsiConsole.Markup($" ◄ ");
                foreach (var escolha in escolhas)
                {
                    string escolhaValue = "";
                    invert = "";
                    if(selecao == escolha.Key)
                        invert = "invert ";
                    escolhaValue = (escolha.Key == escolhas.Last().Key) ? escolha.Value : $" [[{escolha.Value}]] ";
                    AnsiConsole.Markup($"[{invert}default on default]{escolhaValue}[/]");
                }

                AnsiConsole.Markup($" ► ");
                Console.WriteLine();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        continue;
                    case ConsoleKey.LeftArrow:
                        selecao = (ushort)Math.Max(0, selecao - 1);
                        continue;
                    case ConsoleKey.RightArrow:
                        selecao = (ushort)Math.Min(escolhas.Count - 1, selecao + 1);
                        continue;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        numeroDeEscolha = selecao;
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        numeroDeEscolha = 0;
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        numeroDeEscolha = 1;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        numeroDeEscolha = 2;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        numeroDeEscolha = 3;
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        numeroDeEscolha = 4;
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        numeroDeEscolha = 5;
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        numeroDeEscolha = 6;
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        numeroDeEscolha = 7;
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        numeroDeEscolha = 8;
                        break;
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        numeroDeEscolha = 9;
                        break;
                    default:
                        continue;
                }
                Console.Write($"\r ");
                if (numeroDeEscolha < escolhas.Count)
                    break;
            } while (true);
            return numeroDeEscolha;
        }

        public static IAcao MenuPorta(IPorta porta)
        {
            if (porta == null) return null;
            IDictionary<int, string> escolhasHorizontais = new Dictionary<int, string>();
            int max = porta.Escolhas.Count;
            for (int i = 0; i < max; i++)
            {
                escolhasHorizontais[i] = porta.Escolhas[i].Acao.Titulo;
            }
            escolhasHorizontais[max] = "[red][[X]][/]";

            int numPorta = Menu.MenuHorizontal(escolhasHorizontais);

            return porta.Escolhas[numPorta].Acao;
        }
    }
}
