using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.ObjectValue;
using System.Collections;
using System.Collections.Generic;

namespace NoteQuest.Domain.ItensContext.Entities
{
    public class ItensEquipados : IItensEquipados
    {
        public IItemDeMao MaoDireita { get; set; }
        public IItemDeMao MaoEsquerda { get; set; }
        public IPeitoral Peitoral { get; set; }
        public IElmo Elmo { get; set; }
        public IBotas Botas { get; set; }
        public IBraceletes Braceletes { get; set; }
        public IOmbreiras Ombreiras { get; set; }
        public IList<IAmuleto> Amuletos { get; set; }

        public ItensEquipados()
        {
            Amuletos = new List<IAmuleto>();
        }

        public IList<IEquipamento> Listar()
        {
            List<IEquipamento> lista = new();
            if (MaoDireita is not null) lista.Add((IEquipamento)MaoDireita);
            if (MaoEsquerda is not null) lista.Add((IEquipamento)MaoEsquerda);
            if (Peitoral is not null) lista.Add((IEquipamento)Peitoral);
            if (Elmo is not null) lista.Add((IEquipamento)Elmo);
            if (Botas is not null) lista.Add((IEquipamento)Botas);
            if (Braceletes is not null) lista.Add((IEquipamento)Braceletes);
            if (Ombreiras is not null) lista.Add((IEquipamento)Ombreiras);
            foreach (var amuleto in Amuletos)
            {
                lista.Add((IEquipamento)amuleto);
            }

            return lista;
        }

        public bool Descartar(IEquipamento equipamento)
        {
            bool equipamentoDescartado = false;

            if (equipamento == MaoDireita) { MaoDireita = null; equipamentoDescartado = true; }
            if (equipamento == MaoEsquerda) {MaoEsquerda = null; equipamentoDescartado = true; }
            if (equipamento == Peitoral) {Peitoral = null; equipamentoDescartado = true; }
            if (equipamento == Elmo) {Elmo = null; equipamentoDescartado = true; }
            if (equipamento == Botas) {Botas = null; equipamentoDescartado = true; }
            if (equipamento == Braceletes) {Braceletes = null; equipamentoDescartado = true; }
            if (equipamento == Ombreiras) {Ombreiras = null; equipamentoDescartado = true; }
            foreach (var amuleto in Amuletos)
            {
                Amuletos.Remove(amuleto);
                equipamentoDescartado = true;
            }

            return equipamentoDescartado;
        }
    }
}
