using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.ItensContext.Entities;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.ItensContext.ObjectValue;
using NoteQuest.Domain.ItensContext.ObjectValue.Tesouros;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Conteudo : IConteudo
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public bool? PassagemSecreta { get; set; }
        public bool Armadilhas { get; set; }
        public int? Bau { get; set; }
        public int? QtdItens { get; set; }
        public IPortaComum Porta { get; set; }
        public IList<IRepositorio> Repositorio { get; set; }
        public IMasmorra Masmorra { get; set; }

        public Conteudo(TabelaConteudo tabelaConteudo, IMasmorra masmorra)
        {
            Indice = tabelaConteudo.Indice;
            Descricao = tabelaConteudo.Descricao;
            PassagemSecreta = tabelaConteudo.PassagemSecreta;
            Armadilhas = true;
            Bau = (int?)tabelaConteudo.Baú;
            Porta = null;
            Masmorra = masmorra;

            Repositorio = new List<IRepositorio>();
            for (int indiceBau = 0; indiceBau < tabelaConteudo.Baú; indiceBau++)
            {
                Repositorio.Add(new Bau());
            }

            RepositorioDeItens repositorioDeItens = new()
            {
                Conteudo = new Dictionary<int, IItem>()
            };

            int key = 1;
            int qtdMoedas = ConverteQtd(tabelaConteudo.Moedas);
            QtdItens = qtdMoedas > 1 ? 1 : 0;
            if (qtdMoedas > 0)
            {
                IItem cabidela = new Cabidela(qtd: qtdMoedas);
                repositorioDeItens.Conteudo.Add(new KeyValuePair<int, IItem>(key, cabidela));
                key++;
            }

            int qtdItens = ConverteQtd(tabelaConteudo.ItensMagico);
            QtdItens += qtdItens;
            for (; key < QtdItens; key++)
            {
                IItem item = Masmorra.GeraItem();
                repositorioDeItens.Conteudo.Add(new KeyValuePair<int, IItem>(key, item));
            }

            int qtdPergaminho = ConverteQtd(tabelaConteudo.Pergaminho);
            QtdItens += qtdPergaminho;
            for (; key <= QtdItens; key++)
            {
                IItem pergaminho = Masmorra.ItemFactory.GerarPergaminho();
                repositorioDeItens.Conteudo.Add(new KeyValuePair<int, IItem>(key, pergaminho));
            }

            Repositorio.Add(repositorioDeItens);
        }

        private static int ConverteQtd(string qtd)
        {
            switch (qtd)
            {
                case "1d6":
                case "1D6":
                    return D6.Rolagem(1);
                case "2d6":
                case "2D6":
                    return D6.Rolagem(2);
                case "3d6":
                case "3D6":
                    return D6.Rolagem(3);
                case null:
                    return 0;
                default:
                    return int.Parse(qtd);
            }
        }
    }
}
