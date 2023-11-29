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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
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
            /*

            Salão mediano com três portas.
            Contém grande mesa com algumas cadeiras. Pode conter passagem secreta.
            No chão, encontra-se o corpo de um antigo aventureiro com seu inventário.

            ███████▬▬███████   [Esc]   Inventário 
            ██            ██   [Space] Vasculhar
            ██         !  ██   [0]     Magias
            ▌?    ᴕᴕ      X▌   [1][↑]  Porta da frente 
            ██     ᴕᴕ     ██   [2][←]  Porta da esquerda 
            ██            ██   [3][→]  Porta da direita  (Trancada)
            ███████  ███████   [4][↓]  Porta de trás 

             [1][→]  Porta(trancada)
             [ destrancar ]  [ quebrar ]  [▲] |  (Descrição sobre a escolha selecionada)

             */
            IContainer Container = new Container();
            //EscolhaFacade EscolhaFacade = new EscolhaFacade(Container);
            Personagem = CharacterProfile.CriarPersonagem();
            IMasmorra masmorra = Masmorra.GerarMasmorra(Container.MasmorraRepository);

            ConsequenciaDTO consequencia = masmorra.EntrarEmMasmorra();
            EscreverCabecalho(consequencia, masmorra);
            Console.WriteLine(consequencia.Descricao);
            List<IEscolha> escolhas = consequencia.Escolhas;

            IDictionary<int, string[]> escolhasMenu = new Dictionary<int, string[]>();
            for (int i = 0; i < escolhas.Count; i++)
            {
                string Titulo = escolhas[i].Acao.Titulo;
                string Descricao = escolhas[i].Acao.Descricao;
                escolhasMenu[i] = new[] { Titulo, "", $"({Descricao})"};
                //Console.WriteLine($"{i} → {Titulo} ({Descricao})");
            }
            int numeroDeEscolha = 0;


            do
            {

                numeroDeEscolha = Menu.MenuVertical(escolhasMenu);
                /*
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
                }
                */

                try
                {
                    IAcao acao = Personagem.ChainOfResponsabilityEfeito(escolhas[numeroDeEscolha].Acao);
                    consequencia = acao.Efeito();
                }
                catch
                {
                    Console.WriteLine(" [Opção inválida!]");
                    continue;
                }
                
                EscreverCabecalho(consequencia, masmorra);
                Console.WriteLine(consequencia.Descricao);
                escolhas = consequencia.Escolhas;
                escolhasMenu.Clear();
                for (int i = 0; i < escolhas.Count; i++)
                {
                    string Titulo = escolhas[i].Acao.Titulo;
                    string Descricao = escolhas[i].Acao.Descricao;
                    escolhasMenu[i] = new[] { Titulo, "", $"({Descricao})" };
                    //Console.WriteLine($"{i} → {Titulo}");
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
                Console.Write($" [{porta.Posicao}|{porta.Andar}] ");
            }
            Console.WriteLine();

        }


    }
}


/*
 ---

╔══════════════════════════════════════════════════╗
║  PERSONAGEM: Iglu                                ║
║  ■ Raça: Humano                                  ║
║  ■ Classe: Chaveiro, Vingador                    ║
║    Não gasta tochas para abrir fechaduras        ║
║  ■ Moedas:    0                                  ║
║  ■ Tochas:    ■■■■■■□□□□ 6/10                    ║
║  ■ Provisões: ■■■■■■■■■■■■■■■■■■■□ 19/20         ║
║  ■ PV:        ●●●●●●●●●●●●●●●●●○○○○○ 17/22       ║
║  ■ Inventário:                                   ║
║        ▪ Mochila:  ■■■■■■□□□□ 6/10               ║
║        ▪ Armadura:                               ║
║           ▪ Elmo:       ●●●●●● 6/6               ║
║           ▪ Peitoral:   ●●●●●●○○○○ 6/10          ║
║           ▪ Braceletes: ●● 2/2                   ║
║        ▪ Livro de Magias:                        ║
║           ▪ Cura            ■□□ 1/3              ║
║           ▪ Luz:            ■■■ 3/3              ║
║           ▪ Teletransporte: ■□ 1/2               ║
║           ▪ Raio de Gelo:   ■■■ 3/3              ║
║           ▪ Relâmpago:      ■ 1/1                ║
╚══════════════════════════════════════════════════╝

 Pergaminho de cura
 [◄] [ usar]  [ equipar ]  [ descartar ]  [▲] [►]

[▲] 
[0] X   
[1] (MÃO)    Adaga da destruição (1d6+1)
[2] (MÃO)    Tocha       ■■■■■■□□□□ 6/10
[3] (CABEÇA) Elmo        ●●●●●● 6/6    
[4] (PEITO)  Peitoral    ●●●●●●○○○○ 6/10    
[5] (BRAÇOS) Braceletes  ●● 2/2  
[▼]

 ----

COMBATE INICIADO!

╔═════▬▬══════╗   [Esc]   Inventário 
║             ║   [0][Space] Lutar (Adaga [1d6] -1)
║             ║   [1]  Cura (■□□) (Recurera 5 PV)
▌?    ᴥᴥ      X   [2]  Luz  (■□□)
║      ᴥᴥ     ║   [3]  Teletransporte (■■)
║             ║   [4]  Raio de Gelo (□□)
╚════════  ═══╝   [5]  Relâmpago (□)

Iglu ♥●●●●●●●●●●●●●●●●●ᴓ○○○○ 17/22
[▲]
[0] X
[1] Goblin Gordo  ●●●
[2] Goblin Caolho ●●●
[3] Goblin Feio   ●●○
[2] Goblin Caolho ●●●
[▼]
 */