using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.ItensContext.ObjectValue;

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
            //TODO: E se for 2 maõs?
            // Tocha não aparece nos equipamentos;
        }

        public bool RemoverItem(IItem item)
        {
            return Mochila.Remove(item);
        }

        public bool AdicionaItem(IItem item)
        {
            if (item.GetType() == typeof(Cabidela))
            {
                Cabidela cabidela = (Cabidela)item;
                Moedas += (ushort) cabidela.Qtd;
                return true;
            }
            if (Mochila.Count < CapacidadeMochila)
            {
                Mochila.Add(item);
                return true;
            }
            
            return false;
        }
        
        public bool Equipar(IEquipamento equipamento)
        {
            if (equipamento is IAmuleto amuleto)
                Equipamentos.Amuletos.Add(amuleto);
            else if (equipamento is IBraceletes braceletes && Equipamentos.Braceletes is null)
                Equipamentos.Braceletes = braceletes;
            else if (equipamento is IBotas botas && Equipamentos.Botas is null)
                Equipamentos.Botas = botas;
            else if (equipamento is IElmo elmo && Equipamentos.Elmo is null)
                Equipamentos.Elmo = elmo;
            else if (equipamento is IOmbreiras ombreiras && Equipamentos.Ombreiras is null)
                Equipamentos.Ombreiras = ombreiras;
            else if (equipamento is IPeitoral peitoral && Equipamentos.Peitoral is null)
                Equipamentos.Peitoral = peitoral;
            else
                return false;

            RemoverItem(equipamento);

            return true;
        }

        public bool Desequipar(IEquipamento equipamento)
        {
            if (Mochila.Count == 10)
                return false;

            if (equipamento is IAmuleto amuleto && Equipamentos.Amuletos.Contains(amuleto))
                Equipamentos.Amuletos.Remove(amuleto);
            else if (equipamento is IBraceletes braceletes && Equipamentos.Braceletes == braceletes)
                Equipamentos.Braceletes = null;
            else if (equipamento is IBotas botas && Equipamentos.Botas == botas)
                Equipamentos.Botas = null;
            else if (equipamento is IElmo elmo && Equipamentos.Elmo == elmo)
                Equipamentos.Elmo = null;
            else if (equipamento is IOmbreiras ombreiras && Equipamentos.Ombreiras == ombreiras)
                Equipamentos.Ombreiras = null;
            else if (equipamento is IPeitoral peitoral && Equipamentos.Peitoral == peitoral)
                Equipamentos.Peitoral = null;
            else
                return false;

            AdicionaItem(equipamento);

            return true;
        }

        public bool UsarItem(IItem item)
        {
            return false;
        }
    }
}
