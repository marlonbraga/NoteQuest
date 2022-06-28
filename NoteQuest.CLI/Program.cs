using NoteQuest.CLI.Interfaces;
using NoteQuest.CLI.IoC;
using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Text;

namespace NoteQuest.CLI
{
    public class Program
    {
        static public IContainer Container;
        static public IPersonagem Personagem;
        static public Partida Partida;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Container = new Container();
            Console.OutputEncoding = Encoding.UTF8;
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
            Personagem = CharacterProfile.CriarPersonagem();
            CharacterProfile.ExibirFicha(Personagem);
            Partida = new(Container.EscolhaFacade, Container.Masmorra, Personagem);
            Partida.CriarNovoJogo();
        }
    }
}
