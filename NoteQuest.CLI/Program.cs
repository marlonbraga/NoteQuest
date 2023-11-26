using NoteQuest.Application;
using NoteQuest.CLI.IoC;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;


namespace NoteQuest.CLI
{
    public class Program
    {
        static public IPersonagem Personagem;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
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
            //Console.WriteLine("→↓↔←↑▲►▼◄█▓▒░ ▌▐");
            IContainer Container = new Container();
            //EscolhaFacade EscolhaFacade = new EscolhaFacade(Container);
            Personagem = CharacterProfile.CriarPersonagem();
            IMasmorra masmorra = Masmorra.GerarMasmorra(Container.MasmorraRepository);

            ConsequenciaDTO consequencia = masmorra.EntrarEmMasmorra();
            EscreverCabecalho(consequencia, masmorra);
            Console.WriteLine(consequencia.Descricao);
            List<IEscolha> escolhas = consequencia.Escolhas;
            for (int i = 0; i < escolhas.Count; i++)
            {
                string Titulo = escolhas[i].Acao.Titulo;
                string Descricao = escolhas[i].Acao.Descricao;
                Console.WriteLine($"{i} → {Titulo} ({Descricao})");
            }
            int numeroDeEscolha = 0;
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
                        CharacterProfile.ExibirFicha(Personagem);
                        continue;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
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
                        break;
                }

                try
                {
                    consequencia = escolhas[numeroDeEscolha].Acao.Executar();
                }
                catch
                {
                    Console.WriteLine(" [Opção inválida!]");
                    continue;
                }
                
                EscreverCabecalho(consequencia, masmorra);
                Console.WriteLine(consequencia.Descricao);
                escolhas = consequencia.Escolhas;
                for (int i = 0; i < escolhas.Count; i++)
                {
                    string Titulo = escolhas[i].Acao.Titulo;
                    string Descricao = escolhas[i].Acao.Descricao;
                    Console.WriteLine($"{i} → {Titulo}");
                }
            } while (true);
        }

        static void EscreverCabecalho(ConsequenciaDTO consequencia, IMasmorra masmorra)
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================================");
            Console.WriteLine($"SALA: {consequencia.Segmento.IdSegmento} | ANDAR: {consequencia.Segmento.Andar} | INEXPLORADAS: {masmorra.QtdPortasInexploradas}");
            Console.WriteLine();
            foreach (var porta in consequencia.Segmento.Portas)
            {
                Console.Write($"[{porta.Posicao.ToString()}|{porta.Andar}]");
            }
            Console.WriteLine();

        }
    }
}
