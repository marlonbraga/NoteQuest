using NoteQuest.Domain.Core.Interfaces;
using Alba.CsConsoleFormat;
using static System.ConsoleColor;
using System.Linq;
//using NoteQuest.CLI.Interfaces;
using NoteQuest.CLI.IoC;
//using NoteQuest.Application.Interfaces;
//using NoteQuest.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using NoteQuest.Domain.Core;
using System.Reflection.Metadata;
using Document = Alba.CsConsoleFormat.Document;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Application;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using Spectre.Console;

namespace NoteQuest.CLI
{
    public static class CharacterProfile
    {
        static public IContainer Container;
        static public IPersonagem Personagem;

        public static string ExibirFicha(IPersonagem? personagem = null, int linhas = 0)
        {
            Personagem = personagem ?? Personagem;
            IItensEquipados equipamentos = Personagem.Inventario.Equipamentos;

            string ficha = $@"
╔═ [underline][yellow]Personagem:[/] {Personagem.Nome} [/]
║
║ [yellow]■ Raça:[/]      {Personagem.Raca.Nome}
║ [yellow]■ Classe:[/]    {GetClassesNome(Personagem.Classes)}
║                    [gray]{GetClassesDescricao(Personagem.Classes)}[/]
║ ▪ Moedas:    [#daa520]{Personagem.Inventario.Moedas}[/]
║ ▪ Tochas:    [yellow]{GetTochaBar(Personagem)} {Personagem.Inventario.Tochas}/10[/]
║ ▪ Provisões: [#4f7942]{GetProvisaoBar(Personagem)} {Personagem.Inventario.Provisoes}/20[/]
║ ▪ PV:        [red]{GetPvBar(Personagem)} {Personagem.Pv.Pv}/{Personagem.Pv.PvMaximo}[/]
║ [yellow]■ Inventário[/]
║    [yellow]▪ Mochila:[/] {GetMochilaBar(Personagem)} {Personagem.Inventario.Mochila.Count}/10
║    [yellow]▪ Equipamentos:[/]
║       [gray]Mão 1[/]  {equipamentos.MaoDireita?.Nome} {equipamentos.MaoDireita?.Descricao}
║       [gray]Mão 2[/]  [yellow]Tocha[/]{equipamentos.MaoEsquerda?.Nome} {equipamentos.MaoEsquerda?.Descricao}
║       [gray]Peito[/]  {equipamentos.Peitoral?.Nome} {GetPv(equipamentos.Peitoral?.Pv)}
║       [gray]Cabeça[/] {equipamentos.Elmo?.Nome} {GetPv(equipamentos.Elmo?.Pv)}
║       [gray]Pernas[/] {equipamentos.Botas?.Nome} {GetPv(equipamentos.Botas?.Pv)}
║       [gray]Braços[/] {equipamentos.Braceletes?.Nome} {GetPv(equipamentos.Braceletes?.Pv)}
║    [yellow]▪ Livro de Magias:[/]
║       [gray]- sem magias -[/]
║       Cura   [#ad5cad]■□□ 1/3[/]
╚═";

            string espaçamento = "";
            for (int i = 0; i <= linhas; i++)
            {
                espaçamento += "\n";
            }
            return (ficha + espaçamento);
        }

        public static string GetProvisaoBar(IPersonagem personagem)
        {
            ushort maxLimit = 20;
            ushort qtdProvisoes = personagem.Inventario.Provisoes;
            return GetBar('■', '□', maxLimit, qtdProvisoes);
        }

        public static string GetMochilaBar(IPersonagem personagem)
        {
            int maxLimit = 10;
            int qtdItens = personagem.Inventario.Mochila.Count();
            return GetBar('■', '□', maxLimit, qtdItens);
        }

        public static string GetTochaBar(IPersonagem personagem)
        {
            ushort maxLimit = 10;
            ushort qtdTochas = personagem.Inventario.Tochas;
            return GetBar('■', '□', maxLimit, qtdTochas);
        }

        public static string GetPvBar(IPersonagem personagem)
        {
            int maxLimit = personagem.Pv.PvMaximo;
            int qtdPontosDeVida = personagem.Pv.Pv;
            return GetBar('●', '○', maxLimit, qtdPontosDeVida);
        }

        public static string GetPv(IPontosDeVida? pontosDeVida)
        {
            if (pontosDeVida is null) return String.Empty;
            int maxLimit = pontosDeVida.PvMaximo;
            int qtdPontosDeVida = pontosDeVida.Pv;
            return GetBar('●', '○', maxLimit, qtdPontosDeVida);
        }

        public static string GetBar(char full, char empty, int max, int actual)
        {
            string result = string.Empty;
            int i = 0;
            for (; i < actual; i++)
            {
                result += full;
            }
            for (; i < max; i++)
            {
                result += empty;
            }

            return result;
        }

        public static string GetClassesNome(List<IClasse> classes)
        {
            return string.Join(", ", classes.Select(x=>x.Nome));
        }

        public static string GetClassesDescricao(List<IClasse> classes)
        {
            return string.Join("\n", classes.Select(x => x.Descricao));
        }

        public static IPersonagem CriarPersonagem()
        {
            Container = new Container();
            //IPersonagemBuilder personagemBuilder = Container.PersonagemBuilder;
            IPersonagemService personagemService = Container.PersonagemService;
            IPersonagem personagem = personagemService.CriarPersonagem();

            var doc1 = new Document(
                new Span(" ■ Pressione [ENTER] ou [ESPAÇO] para Definir Raça ■") { Color = White, Background = ConsoleColor.Black }, "\n",
                new Span("[Definir Raça]") { Color = White, Background = ConsoleColor.DarkGreen }/*, "\n"*/
            );
            ConsoleRenderer.RenderDocument(doc1);

            //Console.ReadKey();
            int raca = D6.Rolagem(2);
            personagemService.DefinirRaca(personagem, raca);
            //FakeLoading();
            var doc2 = new Document(
                new Span($"  {personagem.Raca.Nome.ToUpper()}                   ") { Color = Yellow }, "\n",
                new Span($"    ■ Vantagem: ") { Color = Yellow }, personagem.Raca.Vantagem, "\n",
                new Span($"    ■ PV: ") { Color = Yellow }, personagem.Raca.Pv, "\n"
            );
            ConsoleRenderer.RenderDocument(doc2);

            var doc3 = new Document(
                new Span(" ■ Pressione [ENTER] ou [ESPAÇO] para Definir Classe ■") { Color = White, Background = ConsoleColor.Black }, "\n",
                new Span("[Definir Classe]") { Color = White, Background = ConsoleColor.DarkGreen }/*, "\n"*/
            );
            ConsoleRenderer.RenderDocument(doc3);

            //Console.ReadKey();
            int classe = D6.Rolagem(2);
            personagemService.DefinirClasse(personagem, classe);
            //FakeLoading();
            var doc4 = new Document(
                new Span($"  {personagem.Classes[0].Nome.ToUpper()}                   ") { Color = Yellow }, "\n",
                new Span($"    ■ Vantagem: ") { Color = Yellow }, personagem.Classes[0].Vantagem, "\n",
                new Span($"    ■ PV Adicional: ") { Color = Yellow }, personagem.Classes[0].Pv, "\n",
                new Span($"    ■ Arma Inicial: ") { Color = Yellow }, $"{personagem.Classes[0].ArmaInicial.Nome} ({personagem.Classes[0].ArmaInicial.Descricao})", "\n"
            );
            ConsoleRenderer.RenderDocument(doc4);
            Console.Write($"\n■ Qual é o nome do seu personagem?\n>: ");
            string nomePersonagem = Console.ReadLine();
            personagem.Nome = nomePersonagem;
            return personagem;
        }

        static void FakeLoading()
        {
            ushort pontos = 20;
            ushort tempo = 1500;
            for (int i = 0; i < pontos; i++)
            {
                Console.Write(".");
                Thread.Sleep(tempo / pontos);
            }
            Console.WriteLine($"\r{Espaco(pontos)}\r");
        }
        static string Espaco(ushort qtd)
        {
            string espaco = "";
            for (int i = 0; i < qtd; i++)
            {
                espaco += " ";
            }
            return espaco;
        }
    }
}
