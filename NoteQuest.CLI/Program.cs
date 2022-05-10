using NoteQuest.Application;
using NoteQuest.Domain.Core.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
                                                                                                                  
                                    (                                                                @              
.#@@@@@@/           @@&    @@@@@@@@@@@@@@@@@@@%.     @@@@@@                                @@@@@@@@@@@@@@@@@@@@,    
     @@@@@@@     @@@(  /@@@.     @@@        *@@*    @@@@@@@@@   @@&    @@@@      ,@@&     &@@&*   @@@@              
     /@@@@@@@  @@@@    @@@@@@@  @@@@    @@@ @@@   @@@@    *@@@@ .@@@  @@@@   @@@ @@@   @@@@   @   @@@@              
     @@@@@@@@@@@@@  @@@@    @@@ @@@@  @@@.@@@@  *@@@   @,  @@@@ @@@   @@@# @@@&%@@@     (@@@@@,   @@@@              
     @@@@  @@@@@@   /@@@@* @@,  @@@@  @@@@       @@@@    @@@@@ &@@@   @@@# @@@@       @@@ @@@@@#  @@@@.,@@.         
    @@@@    @@@@@&    @@@@@@    @@@@@  &@@@@#@@@   @@@@@@@@@@@  @@@@@@@@@@@ #@@@@&&@@ @@@@@@@*    @@@@@*            
   @@@        @@@@@*               ,@#              *@@@@, @@@@                                   @@@               
&@%             @@@@@@@,                             @%   (@@@@@@@@@@@@@@@@@@@@@@@%,          &@                    
                       .*.                                     .&@@@@(                                             
           
");
            CriarNovoJogo();
        }

        static void CriarNovoJogo()
        {
            IAcao acao = null;
            Masmorra masmorra = null;
            Console.WriteLine("→↓↔←↑▲►▼◄▓▒▐░█▌");
            IResultadoAcao resultado = null;
            do
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Backspace:
                        break;
                    case ConsoleKey.Tab:
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        masmorra = new Masmorra();
                        acao = masmorra.EntrarEmMasmorra(1);
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("◄");
                        break;
                    case ConsoleKey.UpArrow:
                        Console.WriteLine("▲");
                        break;
                    case ConsoleKey.RightArrow:
                        Console.WriteLine("►");
                        break;
                    case ConsoleKey.DownArrow:
                        Console.WriteLine("▼");
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        break;
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        break;
                    default:
                        break;
                }

                masmorra = new Masmorra();
                acao = masmorra.EntrarEmMasmorra(1);
                resultado = acao.executar();
                Console.WriteLine(resultado.Descrição);

            } while (true);
        }
    }
}
