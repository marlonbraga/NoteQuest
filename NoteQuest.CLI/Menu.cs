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
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.ItensContext.Entities;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Racas;

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
        public static TipoMenu MenuSegmento(BaseSegmento segmento, string cor = "default")
        {
            if (segmento.IdSegmento == 0)
                cor = "#daa520";
            if (segmento.Masmorra.SalaFinal == segmento)
                cor = "red";
            string mapa = Mapa.DesenharSala(segmento);
            
            string[] linhaDeMapa = mapa.Split("\n");
            linhaDeMapa = linhaDeMapa.Select(linha => $"[{cor}]{linha}[/]").ToArray();

            IDictionary<TipoMenu, string[]> escolhas = new Dictionary<TipoMenu, string[]>();
            escolhas[TipoMenu.Inventário] = new[] { Mapa.DesenharLinhaDeSala(linhaDeMapa[0]), "[[Esc]]", "Inventário", "", "" };
            escolhas[TipoMenu.Sala] = new[] { Mapa.DesenharLinhaDeSala(linhaDeMapa[1]), "[[Space]]", "Vasculhar", "", "" };
            escolhas[TipoMenu.Magias] = new[] { Mapa.DesenharLinhaDeSala(linhaDeMapa[2]), "[[0]]", "Magias", "", "" };
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
                    {
                        string descricaoPorta = string.Empty;
                        if (TryCast(porta, out IPortaComum portaComum))
                            if (TryCast(portaComum.SegmentoAlvo, out Sala sala))
                            {
                                int? count = sala.Monstros?.Count;
                                string inimigos = string.Empty;
                                for (; count > 0; count--)
                                {
                                    inimigos += "ᴥ";
                                }
                                descricaoPorta += sala.Conteudo is not null? "[blue]●[/]" : "";
                                descricaoPorta += $"[red]{inimigos}[/]";
                                if (descricaoPorta != string.Empty)
                                    descricaoPorta = $"[#333]({descricaoPorta})[/]";
                            }
                        escolhas[(TipoMenu)numLinha+1] = new[] { linhaDesenhada, $"[[{indexPortas+1}]][[{Seta(porta.Posicao)}]]", $"Porta {porta.EstadoDePorta.ToString()}", $"{descricaoPorta}", "" };
                    }
                    else
                        numLinha--;
                }
                else
                {
                    escolhas[(TipoMenu)numLinha+1] = new[] { linhaDesenhada, "", "", "", "" };
                }
            }

            int portaIndex = 0;


            //Pegar a porta selecionada
            return MenuVerticalDeMasmorra(escolhas);
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
            
            return text;
        }

        public static TipoMenu MenuVerticalDeMasmorra(IDictionary<TipoMenu, string[]> escolhas, string[] cores = null)
        {
            string defaultBackgroundColor = cores?[1] ?? "default";
            string corColuna1 = cores?[2] ?? "default";
            string corColuna2 = cores?[3] ?? "#333";
            string corColuna3 = cores?[4] ?? "default";
            bool exibirDescricao = false;
            for (int i = 0; i < escolhas.Count + 2; i++)
                Console.WriteLine();
            var currentLineNumber = Console.CursorTop - (escolhas.Count + 2);
            TipoMenu selecao = TipoMenu.None;
            TipoMenu opcaoMenu;
            int[] largurDeColuna = new int[6];

            for (int i = 0; i < escolhas.Count; i++)
            {
                for (int j = 0; j < escolhas[(TipoMenu)i+1].Length; j++)
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
                    //string corColuna4 = corColuna2;
                    if (escolha.Value[2].Contains("none")) { escolha.Value[2] = escolha.Value[2].Replace("none", "[yellow]???[/]");}
                    else if (escolha.Value[2].Contains("fechada")) { escolha.Value[2] = escolha.Value[2].Replace("fechada", "[red]trancada[/]"); }
                    else if (escolha.Value[2].Contains("aberta")) { escolha.Value[2] = escolha.Value[2].Replace("aberta", "[green]aberta[/]"); }
                    else if (escolha.Value[2].Contains("quebrada")) { escolha.Value[2] = escolha.Value[2].Replace("quebrada", "[green]quebrada[/]"); }
                    //else corColuna4 = corColuna2;
                    AnsiConsole.Markup($"[{corColuna3}] {escolha.Value[2]}[/] {escolha.Value[3]}");

                    //Coluna 4
                    if (selecao == escolha.Key || exibirDescricao)
                        AnsiConsole.Markup($"[{corColuna2}]{escolha.Value[4]}[/]");
                    AnsiConsole.MarkupLine("");
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        opcaoMenu = TipoMenu.Inventário;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        opcaoMenu = TipoMenu.Sala;
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        opcaoMenu = TipoMenu.Magias;
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.UpArrow:
                        opcaoMenu = TipoMenu.Porta1;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.LeftArrow:
                        opcaoMenu = TipoMenu.Porta2;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.RightArrow:
                        opcaoMenu = TipoMenu.Porta3;
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.DownArrow:
                        opcaoMenu = TipoMenu.Porta4;
                        break;
                    default:
                        continue;
                }
                Console.Write($"\r ");
                if (escolhas.ContainsKey(opcaoMenu))
                    break;
            } while (true);
            return opcaoMenu;
        }

        public static int MenuVertical(IDictionary<int, string[]> escolhas, string[] cor = null, bool exibirDescricao = false)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            string cor0 = cor?[0] ?? "white";
            string cor1 = cor?[1] ?? "yellow";
            string cor2 = cor?[2] ?? "white";
            string cor3 = cor?[3] ?? "grey";
            string cor4 = cor?[4] ?? "blue";
            string cor5 = cor?[5] ?? "white";
            for (int i = 0; i < escolhas.Count + 2; i++)
                Console.WriteLine($"║");
            var currentLineNumber = Console.CursorTop - (escolhas.Count + 2);
            int selecao = 1;
            int numeroDeEscolha;
            int[] largurDeColuna = new int[5];
            for (int i = 0; i < escolhas.Count; i++)
                for (int j = 0; j < escolhas[i].Length; j++)
                    largurDeColuna[j] = escolhas.Max(x => Remove(x.Value[j], "[", "]").Length);
            do
            {
                Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
                AnsiConsole.MarkupLine($"║  ▲");
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
                    AnsiConsole.Markup($"║ [{cor0}] [[{escolha.Key}]] [/]");

                    string invert = "";
                    if(selecao == escolha.Key)
                        invert = "invert ";
                    AnsiConsole.Markup($"[{cor1}] {escolha.Value[0]} [/]");
                    AnsiConsole.Markup($"[{invert}{cor2}] {escolha.Value[1]} [/]");
                    if (escolha.Value.Length > 2)
                        AnsiConsole.Markup($"[{cor3}] {escolha.Value[2]} [/]");
                    if (escolha.Value.Length > 3)
                        AnsiConsole.Markup($"[{cor4}] {escolha.Value[3]} [/]");
                    if (escolha.Value.Length > 4 && (selecao == escolha.Key || exibirDescricao))
                        AnsiConsole.MarkupLine($"[{cor5}] {escolha.Value[4]} [/]");
                    else
                    {
                        //Console.ForegroundColor = defaultBackgroundColor;
                        if (escolha.Value.Length > 4)
                            AnsiConsole.MarkupLine($"[{cor5}] {escolha.Value[4]} [/]");
                        else
                            AnsiConsole.MarkupLine("");
                    }
                }
                AnsiConsole.Markup($"║  ▼");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        numeroDeEscolha = 0;
                        break;
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
                AnsiConsole.Markup($"\r║");
                if (numeroDeEscolha <= escolhas.Count)
                {
                    if (numeroDeEscolha == 0)
                    {
                        ClearMultipleConsoleLines(currentLineNumber-3, currentLineNumber + escolhas.Count + 3);
                        Console.SetCursorPosition(0, Math.Max(0, currentLineNumber-3));
                    }
                    break;
                }
            } while (true);
            return numeroDeEscolha;
        }

        public static int MenuVerticalSimples(IDictionary<int, string[]> escolhas, string[] cor = null, bool exibirDescricao = false, IInventario inventario = null)
        {
            string cor0 = cor?[0] ?? "white";
            string cor1 = cor?[1] ?? "yellow";
            string cor2 = cor?[2] ?? "default";
            string cor3 = cor?[3] ?? "grey";
            for (int i = 0; i < escolhas.Count + 2; i++)
                Console.WriteLine($"║");
            var currentLineNumber = Console.CursorTop - (escolhas.Count + 2);
            int selecao = 1;
            int numeroDeEscolha;
            int[] largurDeColuna = new int[5];
            for (int i = 0; i < escolhas.Count; i++)
                if (escolhas.ContainsKey(i))
                    for (int j = 0; j < escolhas[i]?.Length; j++)
                        largurDeColuna[j] = escolhas.Max(x => Remove(x.Value[j], "[", "]").Length);


            IList<IItem> mochila = inventario.Mochila;
            int moedas = inventario.Moedas;
            do
            {
                int higherKey = escolhas.Max(x => x.Key);
                ClearMultipleConsoleLines(currentLineNumber - 1, currentLineNumber + higherKey + 5);
                Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
                if (mochila is not null)
                {
                    string color = mochila.Count == 10 ? "red" : "default";
                    AnsiConsole.MarkupLine($"Mochila: [{color}]{CharacterProfile.GetMochilaBar(mochila)} {mochila.Count}/10[/]  ▪  [#daa520]{moedas}[/] Moedas");
                }
                AnsiConsole.MarkupLine($"║  ▲");
                for (int key = 0; key <= higherKey; key++)
                //foreach (var escolha in escolhas)
                {
                    var escolha = escolhas.ContainsKey(key) ? escolhas[key] : new []{ "", "" , "" , "" };
                    if (key is not 0)
                    {
                        //Formata colunas
                        for (int i = 0; i < escolha.Length; i++)
                        {
                            string valor = escolha[i];
                            while (valor.Length < largurDeColuna[i])
                                valor += " ";
                            escolha[i] = valor;
                        }
                    }

                    //Índice
                    AnsiConsole.Markup($"║ [{cor0}] [[{key}]] [/]");

                    string invert = "";
                    if (selecao == key)
                        invert = "invert ";
                    AnsiConsole.Markup($"[{invert}{cor1}] {escolha[0]} [/]");
                    if (escolha.Length > 1 && (selecao == key || exibirDescricao))
                        AnsiConsole.MarkupLine($"[{cor2}] {escolha[1]} [/]");
                    else
                    {
                        if (escolha.Length > 1)
                            AnsiConsole.MarkupLine($"[{cor3}] {escolha[1]} [/]");
                        else
                            AnsiConsole.MarkupLine("");
                    }
                }
                AnsiConsole.Markup($"║  ▼");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        numeroDeEscolha = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        selecao = (ushort)Math.Max(0, selecao - 1);
                        continue;
                    case ConsoleKey.DownArrow:
                        selecao = (ushort)Math.Min(higherKey, selecao + 1);
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
                AnsiConsole.Markup($"\r║");
                if (numeroDeEscolha <= higherKey)
                {
                    if (numeroDeEscolha == 0)
                    {
                        ClearMultipleConsoleLines(currentLineNumber - 3, currentLineNumber + higherKey + 3);
                        Console.SetCursorPosition(0, Math.Max(0, currentLineNumber - 3));
                    }
                    break;
                }
            } while (true); 
            Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
            
            return numeroDeEscolha;
        }

        public static int MenuHorizontal(IDictionary<int, string[]> escolhas, string titulo = "")
        {
            var currentLineNumber = Console.CursorTop - 1;
            int selecao = 0;
            int numeroDeEscolha;
            int max = escolhas.Count-1;
            string invert;
            do
            {
                Console.SetCursorPosition(0, Math.Max(0, currentLineNumber));
                ClearCurrentConsoleLine();
                AnsiConsole.Markup($"║ {titulo} ◄ ");
                foreach (var escolha in escolhas)
                {
                    string escolhaValue = "";
                    invert = "";
                    if(selecao == escolha.Key)
                        invert = "invert ";
                    escolhaValue = (escolha.Key == escolhas.Last().Key) ? escolha.Value[0] : $" [[{escolha.Value[0]}]] ";
                    AnsiConsole.Markup($"[{invert}default on default]{escolhaValue}[/]");
                }

                AnsiConsole.Markup($" ► ");
                  AnsiConsole.Markup($" [#333]| {escolhas[selecao][1]}[/]");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        numeroDeEscolha = max;
                        break;
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
                Console.Write($"\r║");
                if (numeroDeEscolha < escolhas.Count)
                    break;
            } while (true);
            Console.WriteLine($"");
            return numeroDeEscolha;
        }

        public static IEvent MenuPorta(IPorta porta)
        {
            if (porta == null) return null;
            AnsiConsole.MarkupLine($" Porta {porta.EstadoDePorta} à {porta.Posicao}:\n");
            IDictionary<int, string[]> escolhasHorizontais = new Dictionary<int, string[]>();
            int max = porta.Escolhas.Count;
            for (int i = 0; i < max; i++)
            {
                escolhasHorizontais[i] = new[] { porta.Escolhas[i].Acao.Titulo, porta.Escolhas[i].Acao.Descricao};
            }
            escolhasHorizontais[max] = new [] { "[red][[X]][/]", $"" };

            int numPorta = Menu.MenuHorizontal(escolhasHorizontais);
            if (numPorta == max)
                return null;
            return porta.Escolhas[numPorta].Acao;
        }

        public static TipoMenu MenuInventario(IPersonagem personagem)
        {
            if (personagem.Inventario == null) return TipoMenu.None;
            IDictionary<int, string[]> escolhasHorizontais = new Dictionary<int, string[]>();
            int max = 3;
            IList<IItem> mochila = personagem.Inventario.Mochila;
            escolhasHorizontais[0] = new[] { "Mochila", $"(Capacidade: {CharacterProfile.GetMochilaBar(mochila)} {mochila.Count}/10" };
            escolhasHorizontais[1] = new[] { "Equipamentos", "Armas e Armaduras" };
            escolhasHorizontais[2] = new[] { "Grimório", "Livro de Magias" };
            escolhasHorizontais[max] = new[] { "[red][[X]][/]", "(voltar)" };

            AnsiConsole.MarkupLine($"║\n║ [yellow]■ Inventário:[/]\n");
            return Menu.MenuHorizontal(escolhasHorizontais) switch
            {
                0 => TipoMenu.Mochila,
                1 => TipoMenu.Equipamentos,
                2 => TipoMenu.Magias,
                _ => TipoMenu.None,
            };
        }

        public static void MenuMochila(IInventario inventario)
        {
            IList<IItem> mochila = inventario.Mochila;
            string descricao = "";
            if (mochila.Count is 0)
            {
                descricao="[#333](vazia)[/]";
            }

            IDictionary<int, string[]> escolhasVerticais = new Dictionary<int, string[]>();
            escolhasVerticais[0] = new[] { $"", $"[red] [[X]] [/]", $"{descricao}", $"", $""};
            for (int i = 1; i <= mochila.Count; i++)
            {
                int indiceItem = i - 1;
                escolhasVerticais[i] = new [] {$"{mochila.ElementAt(indiceItem).Nome}",$"{mochila.ElementAt(indiceItem).Descricao}", $"", $"", $"" };
            }

            int max;
            int acaoItem;
            do {
                int indiceItem = Menu.MenuVertical(escolhasVerticais) -1;
                if (indiceItem < 0) break;
                IItem? item = mochila.ElementAt(indiceItem);
                string nomeItem = item?.Nome;
                AnsiConsole.MarkupLine($"║ {nomeItem}");
                IDictionary<int, string[]> escolhasHorizontais = new Dictionary<int, string[]>();
                if (item is IItemEfeitoAtivo)
                    escolhasHorizontais[(int)AcaoItem.Usar] = new [] {$"Usar", $""};
                if (item is IEquipamento)
                    escolhasHorizontais[(int)AcaoItem.Equipar] = new [] {$"Equipar", "Vestir/Segurar" };
                escolhasHorizontais[(int)AcaoItem.Descartar] = new [] {$"Descartar", "Jogar fora" };
                escolhasHorizontais[(int)AcaoItem.None] = new [] {$"[red][[X]][/]", "(voltar)" };

                max = escolhasHorizontais.Count;
                acaoItem = Menu.MenuHorizontal(escolhasHorizontais, nomeItem);
                switch (acaoItem)
                {
                    case (int)AcaoItem.Usar://Usar
                        break;
                    case (int)AcaoItem.Equipar://Equipar
                        inventario.Equipar(item as IEquipamento);
                        break;
                    case (int)AcaoItem.Descartar://Descartar
                        inventario.RemoverItem(item);
                        escolhasVerticais.Remove(indiceItem);
                        break;
                    default:
                        break;
                }
            }
            while (acaoItem == max);
        }

        public static void MenuEquipamentos(IInventario inventario)
        {
            IItensEquipados equipamentos = inventario.Equipamentos;
            IDictionary<int, string[]> escolhasVerticais = new Dictionary<int, string[]>();

            escolhasVerticais[0] = new[] { $"", $"[red][[X]][/]", $"", $"", $"" };
            escolhasVerticais[1] = new[] { $"Mão 1", $"{equipamentos.MaoDireita?.Nome}", $"{equipamentos.MaoDireita?.Descricao}", $"", $"" };
            escolhasVerticais[2] = new[] { $"Mão 2", $"{equipamentos.MaoEsquerda?.Nome}", $"{equipamentos.MaoEsquerda?.Descricao}", $"", $"" };
            escolhasVerticais[3] = new[] { $"Peito", $"{equipamentos.Peitoral?.Nome}", $"{CharacterProfile.GetPvBar(equipamentos.Peitoral?.Pv)}", $"{equipamentos.Peitoral?.Descricao}", $"" };
            escolhasVerticais[4] = new[] { $"Cabeça", $"{equipamentos.Elmo?.Nome}", $"{CharacterProfile.GetPvBar(equipamentos.Elmo?.Pv)} {equipamentos.Elmo?.Pv.Pv}", $"{equipamentos.Elmo?.Descricao}", $"" };
            escolhasVerticais[5] = new[] { $"Ombros", $"{equipamentos.Ombreiras?.Nome}", $"{CharacterProfile.GetPvBar(equipamentos.Ombreiras?.Pv)} {equipamentos.Ombreiras?.Pv.Pv}", $"{equipamentos.Ombreiras?.Descricao}", $"" };
            escolhasVerticais[6] = new[] { $"Pernas", $"{equipamentos.Botas?.Nome}", $"{CharacterProfile.GetPvBar(equipamentos.Botas?.Pv)} {equipamentos.Botas?.Pv.Pv}", $"{equipamentos.Botas?.Descricao}", $"" };
            escolhasVerticais[7] = new[] { $"Braços", $"{equipamentos.Braceletes?.Nome}", $"{CharacterProfile.GetPvBar(equipamentos.Braceletes?.Pv)} {equipamentos.Braceletes?.Pv.Pv}", $"{equipamentos.Braceletes?.Descricao}", $"" };
            //escolhasVerticais[7] = new[] { $"{equipamentos.Amuletos.Nome}", $"{equipamentos.Amuletos.Descricao}" };
            //escolhasVerticais[8] = new[] { $"{equipamentos.MaoDireita.Nome}", $"{equipamentos.MaoDireita.Descricao}" };

            string[] cor = { "#333", "yellow", "default", "default", "#d00", "#333" };
            int max;
            int acaoItem;
            do {
                int indiceEquipamento = Menu.MenuVertical(escolhasVerticais, cor);
                if (indiceEquipamento == 0)
                    return;
                IDictionary<int, string[]> escolhasHorizontais = new Dictionary<int, string[]>();
                if (escolhasVerticais[indiceEquipamento][1].Trim() == string.Empty)
                {
                    AnsiConsole.MarkupLine($"\n\n{escolhasVerticais[indiceEquipamento][1]}");
                    escolhasHorizontais[0] = new [] {$"Equipar","Vestir/Segurar"};
                    escolhasHorizontais[1] = new [] {"[red][[X]][/]", "(voltar)" };
                }
                else
                {
                    AnsiConsole.MarkupLine($"\n\n{escolhasVerticais[indiceEquipamento][1]}");
                    escolhasHorizontais[0] = new [] {$"Desequipar","Colocar na mochila"};
                    escolhasHorizontais[1] = new [] {$"Descartar","Jogar fora"};
                    escolhasHorizontais[2] = new [] {$"[red][[X]][/]", "(voltar)" };
                }
                max = escolhasHorizontais.Count-1;
                acaoItem = Menu.MenuHorizontal(escolhasHorizontais);
            } while (acaoItem == max);
        }

        public static void MenuMagias(IInventario inventario)
        {
            IList<IItem> mochila = inventario.Mochila;
            IDictionary<int, string[]> escolhasVerticais = new Dictionary<int, string[]>();
            for (int i = 0; i < mochila.Count; i++)
            {
                escolhasVerticais[i] = new[] { $"{mochila.ElementAt(i).Nome}", $"{mochila.ElementAt(i).Descricao}" };
            }

            int indiceItem = Menu.MenuVertical(escolhasVerticais);

            AnsiConsole.MarkupLine($"{mochila.ElementAt(indiceItem).Nome}");
            IDictionary<int, string[]> escolhasHorizontais = new Dictionary<int, string[]>();
            escolhasHorizontais[0] = new[] {$"Conjurar", "Lançar magia"};
            escolhasHorizontais[1] = new[] {$"[red][[X]][/]", "(voltar)"};
            int max = escolhasHorizontais.Count;
            int acaoItem = Menu.MenuHorizontal(escolhasHorizontais);
        }

        public static IEvent MenuSala(BaseSegmento sala)
        {
            var opcoes = sala.Escolhas;
            if (opcoes is null || opcoes?.Count == 0)
            {
                AnsiConsole.MarkupLine("\n  [grey]Não há nada relevante aqui[/]");
                return null;
            }

            IDictionary<int, string[]> escolhasHorizontais = new Dictionary<int, string[]>();
            for (int i = 0; i < opcoes.Count; i++)
            {
                if (opcoes.ElementAt(i).Key == OpcaoSala.saquear)
                {
                    VasculharRepositorio evento = (VasculharRepositorio)opcoes.ElementAt(i).Value.Acao;
                    if (evento.RepositorioDeItens.Conteudo.Count == 0)
                    {
                        evento.RepositorioDeItens = null;
                        opcoes.Remove(opcoes.ElementAt(i).Key);
                        i--;
                        continue;
                    } 
                    
                    if (evento.RepositorioDeItens.Conteudo.Count == 1)
                    {
                        escolhasHorizontais[i] = new[]
                        {
                            $"[yellow]{evento.RepositorioDeItens.Conteudo.Single().Value.Nome}[/]", $"{evento.RepositorioDeItens.Conteudo.Single().Value.Descricao}"
                        };
                    }
                    else
                    {
                        escolhasHorizontais[i] = new[]
                        {
                            $"{evento.Titulo}", $"{evento.Descricao}"
                        };
                    }
                }
                else if (opcoes.ElementAt(i).Key == OpcaoSala.bau)
                {
                    AbrirUmBau evento = (AbrirUmBau)opcoes.ElementAt(i).Value.Acao;
                    if (evento.Bau.EstaFechado)
                    {
                        escolhasHorizontais[i] = new[] { $"{opcoes.ElementAt(i).Value.Acao.Titulo}", $"{opcoes.ElementAt(i).Value.Acao.Descricao}" };
                        continue;
                    }
                    var conteudo = evento.Bau.Conteudo;
                    int qtdItens = conteudo.Count;
                    if (qtdItens == 0)
                    {
                        evento.Bau = null;
                        opcoes.Remove(opcoes.ElementAt(i).Key);
                        continue;
                    }
                    if (qtdItens == 1)
                    {
                        escolhasHorizontais[i] = new[]
                        {
                            $"[yellow]{conteudo.Single().Value.Nome}[/]", $"{conteudo.Single().Value.Descricao}"
                        };
                    }
                }
                else if (opcoes.ElementAt(i).Key == OpcaoSala.desarmar)
                {
                    IEvent evento = (DesarmarArmadilhas)opcoes.ElementAt(i).Value.Acao;
                    escolhasHorizontais[i] = new[]
                    {
                        $"{evento.Titulo}", $"{evento.Descricao}"
                    };
                }
                else if (opcoes.ElementAt(i).Key == OpcaoSala.procurar)
                {
                    IEvent evento = (AcharPassagemSecreta)opcoes.ElementAt(i).Value.Acao;
                    escolhasHorizontais[i] = new[]
                    {
                        $"{evento.Titulo}", $"{evento.Descricao}"
                    };
                }
            }

             int exitPosition = escolhasHorizontais.Count;
            escolhasHorizontais[exitPosition] = new[] { $"[red][[X]][/]", "(voltar)" };
            AnsiConsole.MarkupLine($"  Você vasculha a sala");
            int acaoItem = Menu.MenuHorizontal(escolhasHorizontais);
            if (acaoItem == exitPosition)
                return null;
            return opcoes.ElementAt(acaoItem).Value.Acao;
        }

        public static IItem MenuRepositorio(IRepositorio repositorio, IPersonagem personagem)
        {
            IDictionary<int, string[]> escolhasVerticais = new Dictionary<int, string[]>();

            escolhasVerticais[0] = new[] { $"[red][[X]][/]", ""};
            foreach (var slot in repositorio.Conteudo)
            {
                escolhasVerticais[slot.Key] = new[] { slot.Value.Nome, slot.Value.Descricao};
            }

            int indiceItem;
            do
            {
                indiceItem = MenuVerticalSimples(escolhasVerticais, exibirDescricao: false, inventario: personagem.Inventario);
                if (indiceItem == 0)
                    return null;
            }
            while (!repositorio.Conteudo.ContainsKey(indiceItem));

            return repositorio.Conteudo[indiceItem];
        }

        public static bool TryCast<T>(object obj, out T result)
        {
            if (obj is T)
            {
                result = (T)obj;
                return true;
            }

            result = default(T);
            return false;
        }
        
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ClearMultipleConsoleLines(int initialLineCursor, int size)
        {
            Console.SetCursorPosition(0, initialLineCursor);
            for (int i = initialLineCursor; i < size; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, initialLineCursor);
        }
    }
}
