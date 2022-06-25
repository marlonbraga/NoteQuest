using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.ItensContext.Entities
{
    public class Inventario : IInventario
    {
        ushort MaxTochas = 10;
        ushort MaxProvisoes = 20;
        ushort CapacidadeMochila = 10;

        public ushort Tochas { get; private set; }
        public ushort Provisoes { get; private set; }
        public ushort Moedas { get; private set; }
        public IList<IItem> Mochila { get; private set; }
        public IItensEquipados Equipamentos { get; private set; }

        public Inventario()
        {
            Moedas = 0;
            Tochas = 10;
            Provisoes = 20;
            Mochila = new List<IItem>(CapacidadeMochila);
            Equipamentos = new ItensEquipados();
        }

        public ushort AdicionarTochas(ushort qtd)
        {
            Tochas += qtd;
            Tochas = Math.Min(Tochas, MaxTochas);

            return Tochas;
        }
        public ushort GastarTochas(ushort qtd)
        {
            Tochas -= qtd;
            Tochas = Math.Max(Tochas, (ushort)0);

            return Tochas;
        }

        public ushort AdicionarProvisoes(ushort qtd)
        {
            Provisoes += qtd;
            Provisoes = Math.Min(Provisoes, MaxProvisoes);

            return Provisoes;
        }

        public void EquiparItemDeMao(IItemDeMao equipamento)
        {
            RemoverItem((IItem)equipamento);
            if (Equipamentos.MaoDireita is not null)
                AdicionaItem((IItem)equipamento);
            Equipamentos.MaoDireita = equipamento;
        }

        public bool RemoverItem(IItem item)
        {
            return Mochila.Remove(item);
        }
        public bool AdicionaItem(IItem item)
        {
            if (Mochila.Count < CapacidadeMochila)
            {
                Mochila.Add(item);
                return true;
            } else {
                return false;
            }
        }
    }
}
