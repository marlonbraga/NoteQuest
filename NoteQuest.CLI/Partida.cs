using Alba.CsConsoleFormat;
using NoteQuest.Application.Interface;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace NoteQuest.CLI
{
    public class Partida
    {
        public IEscolhaFacade EscolhaFacade;
        public IMasmorra Masmorra;
        public IPersonagem Personagem;

        public Partida(IEscolhaFacade escolhaFacade, IMasmorra masmorra, IPersonagem personagem)
        {
            Masmorra = masmorra;
            EscolhaFacade = escolhaFacade;
            Personagem = personagem;
        }

        public void CriarNovoJogo()
        {
            var doc1 = new Document(
                new Span("\n[Entrar na Masmorra]\n") { Color = White, Background = ConsoleColor.DarkGreen }, "\n"
            );
            ConsoleRenderer.RenderDocument(doc1);
            Console.ReadKey();

            //Console.WriteLine("→↓↔←↑▲►▼◄█▓▒░ ▌▐■");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n");
            Masmorra.Build(D6.Rolagem(), D6.Rolagem(), D6.Rolagem());
            Console.WriteLine($"  {Masmorra.Nome.ToUpper()}\n");

            ConsequenciaDTO consequencia = EscolhaFacade.EntrarEmMasmorra(Masmorra);
            Console.WriteLine(consequencia.Descricao);
            List<IEscolha> escolhas = consequencia.Escolhas;
            Console.Write($"\n\n\n");
            ushort selecao = 0;
            for (int i = 0; i < escolhas.Count; i++)
            {
                string Titulo = escolhas[i].Acao.Titulo;
                string Descricao = escolhas[i].Acao.Descricao;
                if (selecao == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                else Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($" [{i}] → {Titulo} ");
            }
            RealizarEscolha(escolhas);
        }

        public ConsequenciaDTO RealizarEscolha(List<IEscolha> escolhas)
        {
            ConsequenciaDTO consequencia;
            Console.WriteLine("");
            ushort selecao = 0;
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
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        numeroDeEscolha = selecao;
                        break;
                    case ConsoleKey.LeftArrow:
                        selecao = (ushort)Math.Max(0, selecao - 1);
                        numeroDeEscolha = -1;
                        //continue; //Console.WriteLine("◄");
                        break;
                    case ConsoleKey.UpArrow:
                        Console.WriteLine("▲");
                        break;
                    case ConsoleKey.RightArrow:
                        selecao = (ushort)Math.Min(escolhas.Count - 1, selecao + 1);
                        numeroDeEscolha = -1;
                        //continue; //Console.WriteLine("►");
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
                        break;
                }
                if (numeroDeEscolha >= 0)
                {
                    consequencia = escolhas[numeroDeEscolha].Acao.Executar();
                    Console.WriteLine(consequencia.Descricao);
                    escolhas = consequencia.Escolhas;
                }

                Console.Write($"\r");
                for (int i = 0; i < escolhas.Count; i++)
                {
                    string Titulo = escolhas[i].Acao.Titulo;
                    string Descricao = escolhas[i].Acao.Descricao;
                    if (selecao == i) Console.BackgroundColor = ConsoleColor.DarkGreen;
                    else Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write($"  [{Titulo}]  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            } while (true);

            return consequencia;
        }
    }
}
