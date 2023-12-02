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
using Alba.CsConsoleFormat;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using NoteQuest.Domain.Core.ObjectValue;
using Spectre.Console.Rendering;

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
            AnsiConsole.MarkupLine(@"[darkgreen]
                                                                                                                  
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
           
[/]");
            
            CriarNovoJogo();
        }

        static void CriarNovoJogo()
        {
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
            
            //CRIA PERSONAGEM
            Personagem = CharacterProfile.CriarPersonagem();
            AnsiConsole.Markup(CharacterProfile.ExibirFicha(Personagem));

            //CRIA MASMORRA
            IMasmorra masmorra = Masmorra.GerarMasmorra(Container.MasmorraRepository);
            _ = masmorra.GerarNome(/*index ?? D6.Rolagem(1,true)*/0, D6.Rolagem(1, true), D6.Rolagem(1, true));
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n");
            //Masmorra.Build(D6.Rolagem(), D6.Rolagem(), D6.Rolagem());

            AnsiConsole.MarkupLine($"   [underline yellow]{masmorra.Nome.ToUpper()}[/]\n");

            ConsequenciaDTO consequencia = masmorra.EntrarEmMasmorra();
            Console.WriteLine(consequencia.Descricao);
            List<IEscolha> escolhas = consequencia.Escolhas;
            
            int numeroDePorta = 0;

            //Nova Partida
            IAcao acao = null;
            do
            {
                Console.WriteLine("-------------------------------------------------");
                EscreverSala(consequencia, masmorra);
                TipoMenu tipoMenu = Menu.MenuSegmento(consequencia.Segmento);
                BaseSegmento sala = consequencia.Segmento;
                int portaIndex = 0;
                IPorta porta;
                switch (tipoMenu)
                {
                    case TipoMenu.Porta1:
                        portaIndex = 0;
                        porta = sala.Portas.Where(p => p.Posicao == (Posicao)(portaIndex)).SingleOrDefault();
                        if (porta?.Escolhas?.Count == 1)
                        {
                            acao = porta.Escolhas.Single().Acao;
                            if (acao is null) continue;
                            break;
                        }
                        acao = Menu.MenuPorta(porta);
                        if (acao is null) continue;
                        break;
                    case TipoMenu.Porta2:
                        portaIndex = 1;
                        porta = sala.Portas.Where(p => p.Posicao == (Posicao)(portaIndex)).SingleOrDefault();
                        if (porta?.Escolhas?.Count == 1)
                        {
                            acao = porta.Escolhas.Single().Acao;
                            if (acao is null) continue;
                            break;
                        }
                        acao = Menu.MenuPorta(porta);
                        if (acao is null) continue;
                        break;
                    case TipoMenu.Porta3:
                        portaIndex = 2;
                        porta = sala.Portas.Where(p => p.Posicao == (Posicao)(portaIndex)).SingleOrDefault();
                        if (porta?.Escolhas?.Count == 1)
                        {
                            acao = porta.Escolhas.Single().Acao;
                            if (acao is null) continue;
                            break;
                        }
                        acao = Menu.MenuPorta(porta);
                        if (acao is null) continue;
                        break;
                    case TipoMenu.Porta4:
                        portaIndex = 3;
                        porta = sala.Portas.Where(p => p.Posicao == (Posicao)(portaIndex)).SingleOrDefault();
                        if (porta?.Escolhas?.Count == 1)
                        {
                            acao = porta.Escolhas.Single().Acao;
                            if (acao is null) continue;
                            break;
                        }
                        acao = Menu.MenuPorta(porta);
                        if (acao is null) continue;
                        break;
                    case TipoMenu.Sala:
                        continue;
                    case TipoMenu.Inventário:
                        Console.Write("╔");
                        AnsiConsole.Markup(CharacterProfile.ExibirFicha(linhas: (escolhas.Count + 2)));
                        break;
                    case TipoMenu.Equipamentos:
                        continue;
                    case TipoMenu.Mochila:
                        continue;
                    case TipoMenu.Magias:
                        continue;
                    default:
                        continue;
                }

                //Executa Ação
                consequencia = acao.Efeito();
                AnsiConsole.MarkupLine(consequencia.Descricao);
            } while (true);
        }

        static void EscreverSala(ConsequenciaDTO consequencia, IMasmorra masmorra)
        {
            AdicionaConteudo(consequencia.Segmento.Descricao, "cyan");
            if (consequencia.Segmento.GetType() != typeof(Sala))
                return;
            AdicionaConteudo(((Sala)consequencia.Segmento).DescricaoConteudo);
            AdicionaMonstros(((Sala)consequencia.Segmento).Monstros);
            Console.WriteLine();
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