using System;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.ItensContext.ObjectValue.Tesouros;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities.Armadilhas;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.ItensContext.Factories
{
    public class ItemFactory : IItemFactory
    {
        private readonly TabelaRecompensa _tabelaRecompensa;
        private readonly TabelaItemTesouro[] _tabelaTesouro;
        private readonly TabelaItemMaravilha[] _tabelaMaravilha;
        private readonly TabelaItemMagico[] _tabelaItemMagico;
        private readonly TabelaArmadura[] _tabelaArmadura;
        private readonly TabelaArma[] _tabelaArma;

        public ItemFactory(IMasmorraRepository masmorraRepository)
        {
            for (int i = 1; i <= 18; i++)
            {
                //TODO: SET mesmorra
                _tabelaRecompensa = masmorraRepository.PegarDadosMasmorra().TabelaRecompensa;
                _tabelaArmadura = masmorraRepository.PegarDadosMasmorra().TabelaArmadura;
                _tabelaArma = masmorraRepository.PegarDadosMasmorra().TabelaArma;

                #region Tesouro
                _tabelaTesouro = new TabelaItemTesouro[6];
                for (int j = 0; j < 6; j++)
                {
                    _tabelaTesouro[j] = _tabelaRecompensa.TabelaTesouro[j];
                }
                #endregion

                #region Maravilha
                _tabelaMaravilha = new TabelaItemMaravilha[6];
                for (int j = 0; j < 6; j++)
                {
                    _tabelaMaravilha[j] = _tabelaRecompensa.TabelaMaravilha[j];
                }
                #endregion

                #region ItemMagico
                _tabelaItemMagico = new TabelaItemMagico[6];
                for (int j = 0; j < 6; j++)
                {
                    _tabelaItemMagico[j] = _tabelaRecompensa.TabelaItemMágico[j];
                }
                #endregion
            }
        }

        public IItem GeraTesouro(IMasmorra masmorra, int? indice = null, int? indice2 = null)
        {
            indice ??= D6.Rolagem(1, true);
            var tabelaTesouro = _tabelaTesouro[(int)indice];

            IItem item;
            if (tabelaTesouro.Efeito == "maravilha")
                item = GeraMaravilha(indice2);
            else if (tabelaTesouro.Efeito == "equipamento")
                item = GeraItemMagico(indice2);
            else
                item = GeraItemSimples(tabelaTesouro);

            return item;
        }

        public IItem GeraMaravilha(int? indice = null)
        {
            indice ??= D6.Rolagem(1, true);
            IItem item = (IItem)_tabelaMaravilha[(int)indice];

            return item;
        }

        public IItem GeraItemMagico(int? indice = null, int? indice2 = null)
        {
            indice ??= D6.Rolagem(1, true);
            IEncantamento item = (IEncantamento)_tabelaItemMagico[(int)indice];
            if (item.Descricao.Contains("[Arma]"))
                return GeraArma(item, indice2);
            if (item.Descricao.Contains("[Armadura]"))
                return GeraArmadura(item, indice2);
            
            throw new NotImplementedException();
        }

        public IEquipamento GeraArmadura(IEncantamento encantamento, int? indice = null)
        {
            indice ??= D6.Rolagem(1, true);
            var item = _tabelaArmadura[(int)indice];
            IArmadura armadura;
            armadura = indice switch
            {
                1 => new Amuleto(item.Nome, (int)item.Pvs),
                2 => new Braceletes(item.Nome, (int)item.Pvs),
                3 => new Botas(item.Nome, (int)item.Pvs),
                4 => new Ombreiras(item.Nome, (int)item.Pvs),
                5 => new Elmo(item.Nome, (int)item.Pvs),
                6 => new Peitoral(item.Nome, (int)item.Pvs),
                _ => throw new ArgumentOutOfRangeException("indice", "Índice não encontrado na tabela de armaduras"),
            };
            armadura.DefinirEncantamento(encantamento);

            return armadura;
        }

        public IArma GeraArma(IEncantamento encantamento, int? indice = null)
        {
            indice ??= D6.Rolagem(1, true);
            var item = _tabelaArma[(int)indice];
            IArma arma = new Arma();
            bool empunhaduraDupla = item.Caracteristicas == "Duas Mãos";
            string danoRaw = item.Dano.Replace("1d", "").Replace("1D", "");
            _ = short.TryParse(danoRaw, out short dano);
            arma.Build(item.Nome, dano, empunhaduraDupla);
            arma.DefinirEncantamento(encantamento);

            return arma;
        }

        private IItem GeraItemSimples(TabelaItemTesouro tabelaItemTesouro)
        {
            string nome = tabelaItemTesouro.Nome;
            string descricao = tabelaItemTesouro.Descricao;
            string efeito = tabelaItemTesouro.Efeito;
            if (efeito.Contains("valor"))
            {
                string efeitoRaw = GetIndex(efeito);
                int valor = 0;
                _ = int.TryParse(efeitoRaw, out valor);
                return new ItemDeValor(nome, descricao, valor);
            }
            if (efeito.Contains("pv"))
            {
                string efeitoRaw = GetIndex(efeito);
                int pv = 0;
                _ = int.TryParse(efeitoRaw, out pv);
                return new PocaoDeCura(nome, descricao, pv);
            }
            if (efeito.Contains("pergaminho"))
            {
                //TODO
                return null;
            }
            if (efeito.Contains("mana"))
            {
                return new PocaoDeMana(nome, descricao);
            }

            throw new NotImplementedException();
        }

        private string GetIndex(string baseText, string startMark = "[", string finalMark = "]")
        {
            int start = baseText.IndexOf(startMark) + startMark.Length;
            if (start < 0) return baseText;
            int end = baseText.IndexOf(finalMark);
            if (end < 0) return baseText;

            string result = baseText.Substring(start, end - start);
            return result;
        }
    }
}
