using Alba.CsConsoleFormat;
using NoteQuest.Application.Interface;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;
using System.Collections.Generic;
using NoteQuest.Application;
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
            //Console.ReadKey();

            //Console.WriteLine("→↓↔←↑▲►▼◄█▓▒░ ▌▐■");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n");
            Masmorra.Build(D6.Rolagem(), D6.Rolagem(), D6.Rolagem());
            Console.WriteLine($"  {Masmorra.Nome.ToUpper()}\n");

            ConsequenciaView consequencia = EscolhaFacade.EntrarEmMasmorra(Masmorra);
            Console.WriteLine(consequencia.Descricao);
            IDictionary<int, EscolhaView> escolhas = consequencia.Escolhas;
            Console.Write($"\n\n\n");
            Console.WriteLine(DesenharSegmento.Desenha(consequencia.Segmento));
            ushort selecao = 0;
            for (int i = 1; i <= escolhas.Count; i++)
            {
                string Titulo = escolhas[i].Titulo;
                string Descricao = escolhas[i].Descricao;
                if (selecao == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                else Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($" [{i}] → {Titulo} ");
            }
            Rodada(escolhas);
        }

        public void Rodada(IDictionary<int, EscolhaView> escolhas)
        {
            do
            {
                IDictionary<int, EscolhaView> escolhasFiltradas = EscolheTipoDeAcao(escolhas);
                escolhas = EscolheAcao(escolhasFiltradas);
            } while (true);
        }

        public IDictionary<int, EscolhaView> EscolheTipoDeAcao(IDictionary<int, EscolhaView> escolhas)
        {
            AcaoTipo acaoTipo;
            IDictionary<int, EscolhaView> result;
            do
            {
                acaoTipo = EscolherTipoAcao(Console.ReadKey().Key);
                result = ObtemEscolhasPorDirecao(acaoTipo, escolhas);
            } while (result.Count == 0);
            return result;
        }

        public IDictionary<int, EscolhaView> EscolheAcao(IDictionary<int, EscolhaView> escolhas)
        {
            ConsequenciaView consequencia = new();
            Console.WriteLine("");
            ushort selecao = 0;
            int numeroDeEscolha = 0;
            do
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Tab:
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        Console.WriteLine(""); 
                        CharacterProfile.ExibirFicha(Personagem);
                        continue;
                    case ConsoleKey.LeftArrow:
                        selecao = (ushort)Math.Max(0, selecao - 1);
                        numeroDeEscolha = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        selecao = (ushort)Math.Min(escolhas.Count - 1, selecao + 1);
                        numeroDeEscolha = -1;
                        break;
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
                if (numeroDeEscolha >= 0)
                {
                    consequencia = EscolhaFacade.SelecionaEscolha(numeroDeEscolha, null);
                    Console.WriteLine(consequencia.Descricao);
                    //if(escolhas[numeroDeEscolha].Acao is IEntrarPelaPortaService)
                    //    Console.WriteLine(DesenharSegmento.Desenha(consequencia.Segmento));
                    escolhas = consequencia.Escolhas;
                }
                break;
                
            } while (true);

            Console.Write($"\r");
            for (int i = 0; i < escolhas.Count; i++)
            {
                string Titulo = escolhas[i].Titulo;
                string Descricao = escolhas[i].Descricao;
                if (selecao == i) Console.BackgroundColor = ConsoleColor.DarkGreen;
                else Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($"  [{Titulo}]  ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            return consequencia.Escolhas;
        }

        private AcaoTipo EscolherTipoAcao(ConsoleKey consoleKey)
        {
            AcaoTipo result;
            while (true)
            {
                switch (consoleKey)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                        CharacterProfile.ExibirFicha(Personagem);
                        continue;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        return AcaoTipo.Segmento;
                    case ConsoleKey.LeftArrow:
                        return AcaoTipo.PortaEsquerda;
                    case ConsoleKey.UpArrow:
                        return AcaoTipo.PortaFrente;
                    case ConsoleKey.RightArrow:
                        return AcaoTipo.PortaDireita;
                    case ConsoleKey.DownArrow:
                        return AcaoTipo.PortaTras;
                    default:
                        continue;
                }
            }
            return result;
        }

        private IDictionary<int, EscolhaView> ObtemEscolhasPorDirecao(AcaoTipo acaoTipo, IDictionary<int, EscolhaView> escolhas)
        {
            IDictionary<int, EscolhaView> result = new Dictionary<int, EscolhaView>();
            foreach (var escolha in escolhas)
            {
                if(escolha.Value.AcaoTipo == acaoTipo)
                    result.Add(escolha);
            }
            return result;
        }
    }
}
