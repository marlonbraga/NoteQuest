using Alba.CsConsoleFormat;
using NoteQuest.Application.Interface;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using NoteQuest.Application;
using static System.ConsoleColor;
using Ninject.Selection;
using NoteQuest.Domain.Core.ObjectValue;

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

            ExibeMacroOpcoes(escolhas);
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

        public IDictionary<int, EscolhaView> EscolheAcao(IDictionary<int, EscolhaView> escolhas)
        {
            ConsequenciaView consequencia = new();

            ExibeOpcoes(escolhas);
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
                    Console.WriteLine("");
                }
                break;
                
            } while (true);

            ExibeMacroOpcoes(escolhas);
            
            return consequencia.Escolhas;
        }

        public IDictionary<int, EscolhaView> EscolheTipoDeAcao(IDictionary<int, EscolhaView> escolhas)
        {
            AcaoTipo acaoTipo;
            IDictionary<int, EscolhaView> result;
            do
            {
                acaoTipo = EscolherTipoAcao(Console.ReadKey().Key);
                ExibeMacroOpcoes(escolhas, acaoTipo);
                result = ObtemEscolhasPorDirecao(acaoTipo, escolhas);
            } while (result.Count == 0);
            return result;
        }

        //Como esse método sabe que a escolha for de uma porta válida?
        private AcaoTipo EscolherTipoAcao(ConsoleKey consoleKey)
        {
            while(true)
            {
                switch (consoleKey)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Clear:
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        CharacterProfile.ExibirFicha(Personagem);
                        consoleKey = Console.ReadKey().Key;
                        continue;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        return AcaoTipo.Segmento;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        return AcaoTipo.PortaEsquerda;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return AcaoTipo.PortaFrente;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        return AcaoTipo.PortaDireita;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        return AcaoTipo.PortaTras;
                    default:
                        Console.WriteLine(consoleKey);
                        consoleKey = Console.ReadKey().Key;
                        continue;

                }
            }
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

        private void ExibeOpcoes(IDictionary<int, EscolhaView> escolhas)
        {
            Console.WriteLine("");
            int selecao = 0;
            foreach (var escolha in escolhas)
            {
                string titulo = escolha.Value.Titulo;
                string descricao = escolha.Value.Descricao;
                Console.BackgroundColor = selecao == escolha.Key ? ConsoleColor.DarkGray : ConsoleColor.Black;
                Console.Write($" [{escolha.Key}] → {titulo} ");
            }
        }

        private void ExibeMacroOpcoes(IDictionary<int, EscolhaView> escolhas, AcaoTipo acaoTipo = AcaoTipo.Nulo)
        {
            Console.Write($"\r");
            foreach (var escolha in escolhas)
            {
                string titulo = escolha.Value.Titulo;
                string descricao = escolha.Value.Descricao;
                Console.BackgroundColor = acaoTipo == escolha.Value.AcaoTipo ? ConsoleColor.DarkGray : ConsoleColor.Black;
                Console.Write($" [{ExibeTipoAcao(escolha.Value.AcaoTipo)}] Porta ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private string ExibeTipoAcao(AcaoTipo acaoTipo)
        {
            switch (acaoTipo)
            {
                case AcaoTipo.PortaFrente:
                    return "↑";
                case AcaoTipo.PortaDireita:
                    return "→";
                case AcaoTipo.PortaTras:
                    return "↓";
                case AcaoTipo.PortaEsquerda:
                    return "←";
                case AcaoTipo.Segmento:
                    return "■";
                case AcaoTipo.Batalha:
                    return "x";
                default:
                    return "-";

            }
        }
    }
}
