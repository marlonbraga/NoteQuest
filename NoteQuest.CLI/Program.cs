using NoteQuest.Application;
using NoteQuest.CLI.IoC;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Linq;
using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.ItensContext.Interfaces;
using Spectre.Console;
using NoteQuest.Domain.Core;

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
            ---
               0 1 2 3 4 5 6 7
            0 ██████▬▬████████ 0
            1 ██            ██ 1
            2 ██         !  ██ 2
            3 ▌?    ᴕᴕ      X▌ 3
            4 ██     ᴕᴕ     ██ 4
            5 ██            ██ 5
            6 ██████████  ████ 6
               0 1 2 3 4 5 6 7
            Itens de Title:
            - ██ Parede simples
            - ▬▬ Porta aberta frente-tras
            - XX Porta trancada frente-tras
            - ?? Porta inverificada frente-tras
            - ▌ Porta aberta esquerda
            - ▌X Porta trancada esquerda
            - ▌? Porta inverificada esquerda
            -  ▐ Porta aberta direita
            - X▐ Porta trancada direita
            - ?▐ Porta inverificada direita
            -    Porta quebrada
            - ᴥ  Monstro vivo
            -  ᴕ Monstro morto
            - □  Passagem secreta
            -  ! Opção de vasculhar
            - 

            */
            IContainer Container = new Container();
            //EscolhaFacade EscolhaFacade = new EscolhaFacade(Container);
            Personagem = CharacterProfile.CriarPersonagem();
            IMasmorra masmorra = Masmorra.GerarMasmorra(Container.MasmorraRepository);
            _ = masmorra.GerarNome(/*index ?? D6.Rolagem(1,true)*/0, D6.Rolagem(1, true), D6.Rolagem(1, true));
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n");
            //Masmorra.Build(D6.Rolagem(), D6.Rolagem(), D6.Rolagem());

            AnsiConsole.MarkupLine($"   [yellow]{masmorra.Nome.ToUpper()}[/]\n");

            ConsequenciaDTO consequencia = masmorra.EntrarEmMasmorra();
            Console.WriteLine(consequencia.Descricao);
            EscreverSala(consequencia, masmorra);
            List<IEscolha> escolhas = consequencia.Escolhas;

            IDictionary<int, string[]> escolhasMenu = new Dictionary<int, string[]>();
            for (int i = 0; i < escolhas.Count; i++)
            {
                string Titulo = escolhas[i].Acao.Titulo;
                string Descricao = escolhas[i].Acao.Descricao;
                escolhasMenu[i] = new[] { Titulo, "", $"({Descricao})"};
            }
            int numeroDeEscolha = 0;

            do
            {
                numeroDeEscolha = Menu.MenuVertical(escolhasMenu);
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
                
                Console.WriteLine(consequencia.Descricao);

                EscreverSala(consequencia, masmorra);
                Console.WriteLine();
                Mapa.DesenharSala(consequencia.Segmento);
                escolhas = consequencia.Escolhas;
                escolhasMenu.Clear();
                for (int i = 0; i < escolhas.Count; i++)
                {
                    string Titulo = escolhas[i].Acao.Titulo;
                    string Descricao = escolhas[i].Acao.Descricao;
                    escolhasMenu[i] = new[] { Titulo, "", $"({Descricao})" };
                }
            } while (true);
        }

        static void EscreverSala(ConsequenciaDTO consequencia, IMasmorra masmorra)
        {
            Console.WriteLine();
            AdicionaConteudo(consequencia.Segmento.Descricao, "cyan");
            if (consequencia.Segmento.GetType() != typeof(Sala))
                return;
            AdicionaConteudo(((Sala)consequencia.Segmento).DescricaoConteudo);
            AdicionaMonstros(((Sala)consequencia.Segmento).Monstros);
        }
        
        private static string[] conteudosInterativos =
        {
            "uma porta", "duas portas", "três portas", "Baú", "moedas", "Passagem Secreta", "Pergaminhos", "Itens Mágicos"
        };

        private static void AdicionaConteudo(string conteudo, string cor = "bold blue")
        {
            if (conteudo == null)
                return;
            for (int i = 0; i < conteudosInterativos.Length; i++)
            {
                conteudo = conteudo.Replace(conteudosInterativos[i], $"[{cor}]{conteudosInterativos[i]}[/]");
            }
            AnsiConsole.MarkupLine(conteudo);
        }

        private static void AdicionaMonstros(List<Monstro> monstros)
        {
            string result = "";
            if (monstros == null)
                return;
            if (monstros.Count > 0)
            {
                result = $"Nesse cômodo, encontra-se [bold red]{monstros.Count} {monstros[0].Nome}[/] distraído(s) (PV:{monstros[0].PV}; Dano:{monstros[0].Dano}).";
            }
            if (monstros.Count == 1)
            {
                result = result.Replace("(s)","");
            }
            if (monstros.Count > 1)
            {
                result = result.Replace("(s)","s");
            }

            AnsiConsole.MarkupLine(result);
        }
        
        private static string AdicionaConteudo(IConteudo conteudo)
        {
            string result = "";
            result += $"Contém {conteudo.Descricao}";
            return $"Contém {conteudo.Descricao}";
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