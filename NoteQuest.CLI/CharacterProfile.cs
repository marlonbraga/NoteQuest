using NoteQuest.Domain.Core.Interfaces;
using Alba.CsConsoleFormat;
using static System.ConsoleColor;
using System.Linq;
using NoteQuest.CLI.Interfaces;
using NoteQuest.CLI.IoC;
using NoteQuest.Application.Interfaces;
using NoteQuest.Domain.Core.Entities;
using System;
using System.Threading;

namespace NoteQuest.CLI
{
    public static class CharacterProfile
    {
        static public IContainer Container;

        public static void ExibirFicha(IPersonagem personagem)
        {
            var headerThickness = new LineThickness(LineWidth.Single, LineWidth.None);

            var doc = new Document(
                new Span("\n\nPERSONAGEM: ") { Color = Yellow }, personagem.Nome, "\n\n",
                new Span("  ■ Classe: ") { Color = Yellow }, personagem.Classes[0].Nome,
                new Span(" ■ Raça: ") { Color = Yellow }, personagem.Raca.Nome, "\n",
                new Span("  ■ PV: ") { Color = Yellow }, $"{personagem.Pv.Pv}/{personagem.Pv.PvMaximo}",
                new Span(" ■ Moedas: ") { Color = Yellow }, $"{personagem.Inventario.Moedas}", "\n",
                new Span("  ■ Tochas: ") { Color = Yellow }, personagem.Inventario.Tochas,
                new Span(" ■ Provisões: ") { Color = Yellow }, personagem.Inventario.Provisoes, "\n",
                new Span("\n■ Itens Equipados ■") { Color = Yellow },
                new Grid
                {
                    Color = Gray,
                    Columns = { GridLength.Auto, GridLength.Auto },
                    Children = {
                        new Cell("Nome") { Stroke = headerThickness },
                        new Cell("Descrição") { Stroke = headerThickness },
                        personagem.Inventario.Equipamentos.Listar().Select(item => new[] {
                            new Cell(item.Nome) { Align = Align.Right },
                            new Cell(item.Descricao) { Align = Align.Left },
                        })
                    }
                },
                new Span($"\n■ Mochila (") { Color = Yellow }, personagem.Inventario.Mochila.Count,
                new Span($"/10) ■") { Color = Yellow },
                new Grid
                {
                    Color = Gray,
                    Columns = { GridLength.Auto, GridLength.Auto },
                    Children = {
                        new Cell("Item") { Stroke = headerThickness },
                        new Cell("Descrição") { Stroke = headerThickness },
                        personagem.Inventario.Mochila.Select(item => new[] {
                            new Cell(item.Nome) { Align = Align.Right },
                            new Cell(item.Descricao) { Align = Align.Left },
                        }),
                        new Cell(" ") { Align = Align.Right },
                        new Cell(" ") { Align = Align.Left }
                    }
                }
            );

            var doc1 = new Document(
                new Grid
                {
                    Color = Gray,
                    Columns = { GridLength.Auto},
                    Children = {
                        new Cell(doc) { Stroke = new LineThickness(LineWidth.Heavy, LineWidth.Heavy) }
                    }
                }
            );

            ConsoleRenderer.RenderDocument(doc1);
        }

        public static IPersonagem CriarPersonagem()
        {
            Container = new Container();
            IPersonagemBuilder personagemBuilder = Container.PersonagemBuilder;
            IPersonagemService personagemService = Container.PersonagemService;
            IPersonagem personagem = personagemService.CriarPersonagem();

            var doc1 = new Document(
                new Span(" ■ Pressione [ENTER] ou [ESPAÇO] para Definir Raça ■") { Color = White, Background = ConsoleColor.Black }, "\n",
                new Span("[Definir Raça]") { Color = White, Background = ConsoleColor.DarkGreen }/*, "\n"*/
            );
            ConsoleRenderer.RenderDocument(doc1);

            Console.ReadKey();
            int raca = D6.Rolagem(2);
            personagemService.DefinirRaca(personagem, raca);
            FakeLoading();
            var doc2 = new Document(
                new Span($"  { personagem.Raca.Nome.ToUpper() }                   ") { Color = Yellow }, "\n",
                new Span($"    ■ Vantagem: ") { Color = Yellow }, personagem.Raca.Vantagem, "\n",
                new Span($"    ■ PV: ") { Color = Yellow }, personagem.Raca.Pv,"\n"
            );
            ConsoleRenderer.RenderDocument(doc2);

            var doc3 = new Document(
                new Span(" ■ Pressione [ENTER] ou [ESPAÇO] para Definir Classe ■") { Color = White, Background = ConsoleColor.Black }, "\n",
                new Span("[Definir Classe]") { Color = White, Background = ConsoleColor.DarkGreen }/*, "\n"*/
            );
            ConsoleRenderer.RenderDocument(doc3);

            Console.ReadKey();
            int classe = D6.Rolagem(2);
            personagemService.DefinirClasse(personagem, classe);
            FakeLoading();
            var doc4 = new Document(
                new Span($"  { personagem.Classes[0].Nome.ToUpper() }                   ") { Color = Yellow }, "\n",
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
