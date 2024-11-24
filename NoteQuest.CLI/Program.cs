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
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using NoteQuest.Domain.Core.Racas;
using NoteQuest.Domain.ItensContext.Entities;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using System.Threading;

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
            MasmorraAbstractFactory masmorraAbstractFactory = new MasmorraAbstractFactory(Container.MasmorraRepository, Container.SegmentoFactory, Container.ArmadilhaFactory, Container.ItemFactory);
            IMasmorra masmorra = masmorraAbstractFactory.GerarMasmorra(1);
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n");

            AnsiConsole.MarkupLine($"   [underline yellow]{masmorra.Nome.ToUpper()}[/]\n");

            IEnumerable<ActionResult> result = masmorra.EntrarEmMasmorra();
            DungeonConsequence dungeonConsequence = (DungeonConsequence)result.Single();
            Console.WriteLine(dungeonConsequence.Descricao);

            ActionResult scenario = dungeonConsequence;

            int numeroDePorta = 0;

            Turn(dungeonConsequence, masmorra);
        }

        static void Turn(DungeonConsequence initialConsequence, IMasmorra masmorra)
        {
            ActionResult result = initialConsequence;
            do
            {
                switch (result)
                {
                    case DungeonConsequence dungeonConsequence:
                        result = DungeonTurn(dungeonConsequence, masmorra);
                        break;
                    case CombatConsequence combatConsequence:
                        result = CombateTurn(combatConsequence, masmorra);
                        break;
                }
            } while (true);
        }

        static ActionResult DungeonTurn(DungeonConsequence dungeonConsequence, IMasmorra masmorra)
        {
            IEvent acao = null;
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
                    acao = OpcaoPorta(portaIndex: 0, sala);
                    if (acao is null) return dungeonConsequence;
                    break;
                case TipoMenu.Porta2:
                    acao = OpcaoPorta(portaIndex: 1, sala);
                    if (acao is null) return dungeonConsequence;
                    break;
                case TipoMenu.Porta3:
                    acao = OpcaoPorta(portaIndex: 2, sala);
                    if (acao is null) return dungeonConsequence;
                    break;
                case TipoMenu.Porta4:
                    acao = OpcaoPorta(portaIndex: 3, sala);
                    if (acao is null) return dungeonConsequence;
                    break;
                case TipoMenu.Sala:
                    acao = Menu.MenuSala(sala);
                    if (acao is null) return dungeonConsequence;
                    if (acao is VasculharRepositorio)
                    {
                        VasculharRepositorio(acao, dungeonConsequence, sala);
                    }
                    else if (acao is AbrirUmBau)
                    {
                        OpcaoAbrirBau(acao, dungeonConsequence, sala);
                    }
                    else
                    {
                        ExecutaAcao(acao, dungeonConsequence);
                    }
                    return dungeonConsequence;
                case TipoMenu.Inventário:
                    Console.Write("╔");
                    int linhas = dungeonConsequence.Segment.Escolhas?.Count + 2 ?? 2;
                    AnsiConsole.Markup(CharacterProfile.ExibirFicha(linhas: linhas));
                    Inventario(Personagem);
                    return dungeonConsequence;
                default:
                    return dungeonConsequence;
            }

            return ExecutaAcao(acao, dungeonConsequence);
        }

        private static IEvent OpcaoPorta(int portaIndex, BaseSegmento sala)
        {
            IPorta porta = sala.Portas.Where(p => p.Posicao == (Posicao)(portaIndex)).SingleOrDefault();
            if (porta?.Escolhas?.Count == 1)
            {
                return porta.Escolhas.Single().Acao;
            }
            return Menu.MenuPorta(porta);
        }

        private static void OpcaoAbrirBau(IEvent acao, DungeonConsequence dungeonConsequence, BaseSegmento sala)
        {
            _ = ExecutaAcao(acao, dungeonConsequence);
            IRepositorio repositorio = sala.Conteudo.Repositorio.FirstOrDefault(x => x.GetType() == typeof(Bau));
            IItem item = null;
            if (repositorio?.Conteudo.Count == 1)
                item = repositorio.Conteudo.Single().Value;
            do
            {
                item ??= Menu.MenuRepositorio(repositorio, Personagem);
                if (item is null) break;
                if (Personagem.Inventario.AdicionaItem(item))
                {
                    repositorio.PegarItem(item);
                    if (repositorio.Conteudo.Count == 0)
                    {
                        repositorio = null;
                        var bau = sala.Conteudo.Repositorio.FirstOrDefault(x => x.GetType() == typeof(Bau));
                        sala.Conteudo.Repositorio.Remove(bau);
                    }
                }
                item = null;
            } while (repositorio?.Conteudo.Count > 0);
        }

        private static void VasculharRepositorio(IEvent acao, DungeonConsequence dungeonConsequence, BaseSegmento sala)
        {
            _ = ExecutaAcao(acao, dungeonConsequence);
            IRepositorio repositorio = sala.Conteudo.Repositorio.FirstOrDefault(x => x.GetType() == typeof(RepositorioDeItens));
            IItem item = null;
            if (repositorio?.Conteudo.Count == 1)
                item = repositorio.Conteudo.Single().Value;
            do
            {
                item ??= Menu.MenuRepositorio(repositorio, Personagem);
                if (item is null) break;
                if (Personagem.Inventario.AdicionaItem(item))
                {
                    repositorio.PegarItem(item);
                    if (repositorio.Conteudo.Count == 0)
                    {
                        repositorio = null;
                        var repositorioDeItens = sala.Conteudo.Repositorio.FirstOrDefault(x => x.GetType() == typeof(RepositorioDeItens));
                        sala.Conteudo.Repositorio.Remove(repositorioDeItens);
                    }
                }
                item = null;
            } while (repositorio?.Conteudo.Count > 0);
        }

        static ActionResult CombateTurn(CombatConsequence combatConsequence, IMasmorra masmorra)
        {
            return combatConsequence;
            //IEvent acao = null;
            //do
            //{
            //    AnsiConsole.MarkupLine("\n------------------------------------------------\n");
            //    EscreverSala(combatConsequence, masmorra);
            //    Console.WriteLine();
            //    TipoMenu tipoMenu = Menu.MenuCombate(combatConsequence.Segment);
            //    Sala sala = combatConsequence.Segment;
            //    IEvent acao = combatConsequence.Escolhas[tipoMenu];
            //    switch (tipoMenu)
            //    {
            //        case TipoMenu.Inventário:
            //            Console.Write("╔");
            //            int linhas = combatConsequence.Segment.Escolhas?.Count + 2 ?? 2;
            //            AnsiConsole.Markup(CharacterProfile.ExibirFicha(linhas: linhas));
            //            Inventario(Personagem);
            //            continue;
            //        case TipoMenu.Ataque:
            //            Monstro monstro = Menu.MenuMonstro(sala.Monstros);
            //            if (monstro is not null)
            //            {
            //                acao.Alvo = monstro;
            //                ExecutaAcao(acao, combatConsequence, out result, out combatConsequence);
            //            }
            //            continue;
            //        default:
            //            continue;
            //    }
            //} while (true);//TODO: Fazer condição de saída (Vitória ou Derrota)
        }

        static ActionResult ExecutaAcao(IEvent acao, ActionResult defaultConsequence)
        {
            ActionResult consequence = defaultConsequence;
            IEnumerable<ActionResult> result = Personagem.ChainOfResponsabilityEfeito(acao).Efeito();
            foreach (var action in result)
            {
                AnsiConsole.MarkupLine(action.Descricao);
                consequence = action;
            }
            return consequence;
        }

        static void Repositorio(IRepositorio repositorio)
        {

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
                        IItem item = Menu.MenuMochila(personagem.Inventario);
                        if (item is null)
                            continue;
                        //if(item is IItemEfeitoAtivo)
                        //    personagem.ChainOfResponsabilityEfeito(item);
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
            //Console.WriteLine($"ANDAR = {consequencia?.Segment.Andar} ??? = {consequencia?.Segment.Masmorra.QtdPortasInexploradas}");
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