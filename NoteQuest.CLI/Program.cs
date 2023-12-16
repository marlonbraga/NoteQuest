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
using NoteQuest.Domain.MasmorraContext.Services.Factories;

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
            IContainer Container = new Container();
            //EscolhaFacade EscolhaFacade = new EscolhaFacade(Container);
            
            //CRIA PERSONAGEM
            Personagem = CharacterProfile.CriarPersonagem();
            AnsiConsole.Markup(CharacterProfile.ExibirFicha(Personagem));

            //CRIA MASMORRA
            MasmorraAbstractFactory masmorraAbstractFactory = new MasmorraAbstractFactory(Container.MasmorraRepository, Container.SegmentoFactory, Container.ArmadilhaFactory);
            IMasmorra masmorra = masmorraAbstractFactory.GerarMasmorra(1);
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n");

            AnsiConsole.MarkupLine($"   [underline yellow]{masmorra.Nome.ToUpper()}[/]\n");

            IEnumerable<ActionResult> result = masmorra.EntrarEmMasmorra();
            DungeonConsequence dungeonConsequence = (DungeonConsequence)result.Single();
            Console.WriteLine(dungeonConsequence.Descricao);

            ActionResult scenario = dungeonConsequence;

            int numeroDePorta = 0;

            //Nova Partida
            IEvent acao = null;
            do
            {
                AnsiConsole.MarkupLine("\n------------------------------------------------\n");
                EscreverSala(dungeonConsequence, masmorra);
                Console.WriteLine();
                TipoMenu tipoMenu = Menu.MenuSegmento(dungeonConsequence.Segment);
                BaseSegmento sala = dungeonConsequence.Segment;
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
                    case TipoMenu.Inventário: Console.Write("╔");
                        AnsiConsole.Markup(CharacterProfile.ExibirFicha(linhas: (dungeonConsequence.Segment.Escolhas.Count + 2)));
                        Inventario(Personagem);
                        continue;
                    default:
                        continue;
                }

                //Executa Ação
                result = Personagem.ChainOfResponsabilityEfeito(acao).Efeito();
                foreach (var action in result)
                {
                    AnsiConsole.MarkupLine(action.Descricao);
                    if(action.GetType() == typeof(DungeonConsequence))
                        dungeonConsequence = (DungeonConsequence)action;
                }

            } while (true);
        }

        static void Inventario(IPersonagem personagem)
        {
            TipoMenu tipoMenu;
            do
            {
                tipoMenu = Menu.MenuInventario(personagem);
                switch (tipoMenu)
                {
                    case TipoMenu.Equipamentos:
                        /*tipoMenu = */Menu.MenuEquipamentos(personagem.Inventario);
                        continue;
                    case TipoMenu.Mochila:
                        /*tipoMenu = */Menu.MenuMochila(personagem.Inventario);
                        continue;
                    case TipoMenu.Magias:
                        /*tipoMenu = */Menu.MenuMagias(personagem.Inventario);
                        continue;
                    default:
                        tipoMenu = TipoMenu.None;
                        continue;
                }


            } while (tipoMenu != TipoMenu.None);
        }

        static void EscreverSala(DungeonConsequence consequencia, IMasmorra masmorra)
        {
            AdicionaConteudo(consequencia?.Segment.Descricao, "cyan");
            if (consequencia?.Segment.GetType() != typeof(Sala))
                return;
            AdicionaConteudo(((Sala)consequencia.Segment).DescricaoConteudo);
            AdicionaMonstros(((Sala)consequencia.Segment).Monstros);
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
            return result;
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