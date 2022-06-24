using NoteQuest.Application.Interface;
using NoteQuest.Application.Interfaces;
using NoteQuest.CLI.Interfaces;
using NoteQuest.CLI.IoC;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NoteQuest.CLI
{
    public class Program
    {
        static public IContainer Container;
        static public IPersonagem Personagem;
        static void Main(string[] args)
        {
            Container = new Container();
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
            Personagem = CriarPersonagem();
            CriarNovoJogo();
        }

        static void CriarNovoJogo()
        {
            //Console.WriteLine("→↓↔←↑▲►▼◄█▓▒░ ▌▐");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            //Console.Write("░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓████████████████████████████████████▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░");
            //DesenharEntrada();
            IEscolhaFacade EscolhaFacade = Container.EscolhaFacade;
            IMasmorra Masmorra = Container.Masmorra;
            Masmorra.Build(D6.Rolagem(), D6.Rolagem(), D6.Rolagem());
            Console.WriteLine($"  {Masmorra.Nome.ToUpper()}\n");

            ConsequenciaDTO consequencia = EscolhaFacade.EntrarEmMasmorra(Masmorra);
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
                        break;
                }
                consequencia = escolhas[numeroDeEscolha].Acao.Executar();
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

        static IPersonagem CriarPersonagem()
        {
            IPersonagemBuilder personagemBuilder = Container.PersonagemBuilder;
            IPersonagemService personagemService = Container.PersonagemService;
            IPersonagem personagem = personagemService.CriarPersonagem();

            Console.Write($"Rolar Raça\n ■ ■:");
            Console.ReadKey();
            int raca = D6.Rolagem(2);
            personagemService.DefinirRaca(personagem, raca);
            FakeLoading();
            Console.WriteLine($"\n  {personagem.Raca.Nome.ToUpper()}");
            //Console.WriteLine($"    Descrição:{personagem.Raca.Descricao}");
            Console.WriteLine($"    Vantagem: {personagem.Raca.Vantagem}");
            Console.WriteLine($"    PV: {personagem.Raca.Pv}");

            Console.Write($"\nRolar Classe\n ■ ■:");
            Console.ReadKey();
            int classe = D6.Rolagem(2);
            personagemService.DefinirClasse(personagem, classe);
            FakeLoading();
            Console.WriteLine($"\n  {personagem.Classes[0].Nome.ToUpper()}");
            Console.WriteLine($"    Vantagem: {personagem.Classes[0].Vantagem}");
            Console.WriteLine($"    PV Adicional: {personagem.Classes[0].Pv}");
            Console.WriteLine($"    Arma Inicial: {personagem.Classes[0].ArmaInicial}");

            Console.WriteLine($"\nQual é o nome do personagem?\n: ");
            string nomePersonagem = Console.ReadLine();
            personagem.Nome = nomePersonagem;
            FakeLoading();
            Console.WriteLine($"\nPERSONAGEM {personagem.Nome} ");
            Console.WriteLine($"  {personagem.Classes[0].Nome}-{personagem.Raca.Nome} ");
            Console.WriteLine($"  PV Total: {personagem.Pv.PvMaximo}\n");
            return personagem;
        }

        static void FakeLoading()
        {
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
        }

        static void DesenharEntrada()
        {
            string desenho = @"
&&&%%%%&#&@@@&%&%%%%(%#/,,(##&%(#%#%&####(%#*(*,%((/(//*.(/#/&#%&&##(#%((###%&/%##%/((%&%%%%&%&&&@%#
%%/*//##/*&&&&&%%%(##.((%#*/(&##(%%(/######%#/((%#/*%#(..##%#((%/&*%##/(%#%%&&%&#%%#%#&%&%#%%&&%&&%%
@%%%%&###&%%%#/(%%**/(%#%/(/(%#%//&#(%/#%(/(**  #,((*,*,.*,,*,,,.,#((##/%#%&%%&%%%%%%%&%&&%@%&&%&&&&
%&&&#&&&&&%&%%/#((%%%%%%%%#%(/**%%##****##**(,%,,...*.,,,,.*,.,,.//#*,.#/%&%%&&%&&&&&%&&&&%%&&&&&&%%
#/*%#%#&%#&(/%(&%&#&&&%%%%%%((###,%.**,,(,*(/*///**/*/**/** ,,.,,.,,,.*/*(#%&%&&&#&##%%%&%&&%&&&%%%%
&&%%&&&%@%%%(&#(###&&&%%((%/(((/,(*%/((#(((#%%###%%######(#((*,*.**,,,,(*//#&%%%%%&%%%%%&%&@&&%(%%##
&%&%&%(#####&%&(%%%##&((/(#.**%%%&%%%%%%%%(#%%%&%%%%%%%%%%%%%%%##(/,,**,//(#&&%&&%%(%#/(##/#%#((#(,*
%%&@&#%&%#&&&&&&&&#%&&%%%%/%&&&&&&@@&%%&%(//////(//*#(#/&%&%&%%&%%%((****,/**%%&%%%(#(//(%(((##(&%(%
/#&#,,,/(#%%%%#%###&&&#(*#&%&((((((, ..  .,,,,,,**,,,,  .  ,(/**//#&#%((,**,,,,%#%%((%%%#%%%%%/#%#&/
%%&%((/&&%&%&&%%&#&##%#&&&&&#(..,,,,,,,,..,,,,..,,*,,,,,,,,,,*,*,  ///#((/,**/*/#(&%%%####%((///(##,
(#(%%%%%*#(##&%%(%/*#(&@@@&%***/*...............................*/,/*%####(*((#/(%###%&#%%#%%%((%#%#
*//(#####(///%%%&%((/@@@&@&&/,/#/,(                           ..*#,//&&%%#(/(*//(*#*/*(*/(&%%#%#%%%%
(//((%(((/(#(%#((#(#@@@@@@&&#@,//.*                        , .../#*((&&&&%(/((*/#/#%#%&%#%%###%*///.
#,###((#(/,,/#((#%(@@@@@@@@&&#,#(,.,                      ,  ..,(#*##@&@&%##/(**#//%&&&%&&&&%((##%%%
#((/#%*#%%%##(*%%%(&@@@@@@@@&(*/(*.,,  ,       ,  .    , .   *./%%*/#&&&&&##(/, //(#&/%&/*((%%%%%&%%
(//**(*/(,((#%#/(##&@@@@@@@@((*#(/..                      . ..,(#%*#(@&@@@%%%//#(/#(/(%%%%%%&&%&%/#%
*#,((#(//(/#((###%%&@&@&&@@%/**(#/,.                      . .,//##*/(@&&&%&%##%###(#%#%&&#(%&&&%%&&&
(#/(/#////(/*%(/%(%@@&@@@@@%***##/*. .                      .,/((%//#%@&&&#(#/%(#*#%%%%(#%%%%&&%&%&%
(*,*(/((((/(%#(%%#%%@&&@@@@%///%#(,, .                    .,.*((#%//(&&&&&%&%#//((((#%%&&%&&&&&%&&##
(/(%%%%/(###%#&%%#%%@&&&&&@@//((#/,,..                   ..,.,(###*/(&@&&&%/,(##(#/%%(%&&&&&&&&&&@&&
/(*#%#####&#(#%%%,%#&@&&&&&&(((#(/,,. .                   ...**((((//%&&&%##,,####/#%%&&&&##&&%%#%%#
,//(((/#(//%((%*.*%%(&%&&&&%(#%#(/**...                   ...*//*////%%%%##,**,/*(,/%%#((#&%&##%##%*
//**/(/(((((((%((/((/#%(%%&(((&#/*/*...                  ..,,.*/((*//#####/*#*/###(#(((#((((#%%%(//%
//(#/*/#///*((#/*((/(((((**((/#/*(/*.,.        ..      ..,,,*/*//*,//(((/*,(*(/(*//*(#/((#%######*((
**/**/*,,*(.//(/*//*/,(((//(((##(((//****/(((####(#(#((#(######(/*/#(/((#(((//*.,(,,#,(,,(#/(##/#(**
**/**(***,(/***,(.(//*/#(//((##(%#%#(##%###%%(#(##%(####(#%%(%%###(#((/#((#*#*/(/,///.,*#*(((*.((#(/
*(#(//*,,,*(#%/((/###(((####%#%%%(#(/%((###(#(/###%#/#(#(((/%##(##(/#(/((/(*#//*.(/*#(/,/*,//#,..,//
";
            Console.WriteLine(desenho);
        }
    }
}
