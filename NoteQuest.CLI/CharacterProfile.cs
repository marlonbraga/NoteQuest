using NoteQuest.Domain.Core.Interfaces;
using Alba.CsConsoleFormat;
using static System.ConsoleColor;
using System.Linq;

namespace NoteQuest.CLI
{
    public static class CharacterProfile
    {
        public static void ExibirFicha(IPersonagem personagem)
        {
            var headerThickness = new LineThickness(LineWidth.Double, LineWidth.Single);

            var doc = new Document(
                new Span("PERSONAGEM: ") { Color = Yellow }, personagem.Nome, "\n",
                new Span("  Classe: ") { Color = Yellow }, personagem.Classes[0].Nome,
                new Span(" | Raça: ") { Color = Yellow }, personagem.Raca.Nome, "\n",
                new Span("  PV: ") { Color = Yellow }, $"{personagem.Pv.Pv}/{personagem.Pv.PvMaximo}",
                new Span(" | Moedas: ") { Color = Yellow }, $"{personagem.Inventario.Moedas}", "\n",
                new Span("  Tochas: ") { Color = Yellow }, personagem.Inventario.Tochas,
                new Span(" | Provisões: ") { Color = Yellow }, personagem.Inventario.Provisoes, "\n",
                new Span("\nItens Equipados: ") { Color = Yellow }, "\n",
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
                new Span($"\nMochila (") { Color = Yellow }, personagem.Inventario.Mochila.Count,
                new Span($"/10:") { Color = Yellow }, "\n",
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
                        })

                    }
                }
            );

            ConsoleRenderer.RenderDocument(doc);
        }
    }
}
