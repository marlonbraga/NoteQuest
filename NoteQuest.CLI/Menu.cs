using NoteQuest.Domain.Core;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteQuest.CLI
{
    public class Menu
    {

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
            int selecao = 1;
            int numeroDeEscolha;
            do
            {
                Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
                Console.Write($" ◄ ");
                foreach (var escolha in escolhas)
                {
                    Console.BackgroundColor = selecao == escolha.Key ? corDeSelecaoBg : defaultBackgroundColor;
                    Console.ForegroundColor = selecao == escolha.Key ? corDeSelecaoFg : corColuna1;
                    Console.Write($" [{escolha.Value}] ");
                    Console.ForegroundColor = defaultForegroundColor;
                    Console.BackgroundColor = defaultBackgroundColor;
                }

                Console.Write($" ► ");
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
    }
}
