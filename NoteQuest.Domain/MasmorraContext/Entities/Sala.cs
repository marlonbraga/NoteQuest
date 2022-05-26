using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Sala : BaseSegmento, ISegmento
    {
        public string DescricaoConteudo { get; set; }
        public string DescricaoMonstros { get; set; }
        public List<Monstro> Monstros { get; set; }
        public List<IConteudo> Conteudo { get; set; }

        public Sala(ISegmentoBuilder segmentoFactory) : base(segmentoFactory)
        {
        }
        public void MoverEmSilencio(int valorD6)
        {
        }
        public void AcharPassagemSecreta(int valorD6)
        {
        }
        public void AbrirBau(int valorD6_1, int valorD6_2)
        {
        }
        public Sala AdicionaMonstros(List<Monstro> monstros)
        {
            if (monstros.Count > 0)
            {
                Monstros = monstros;
                DescricaoMonstros = $"\n  Há {monstros.Count} {monstros[0].Nome} na sala! (PV:{monstros[0].PV}; Dano:{monstros[0].Dano})";
                DetalhesDescricao += DescricaoMonstros;
            }
            return this;
        }
        public Sala AdicionaConteudo(List<IConteudo> conteudo)
        {
            Conteudo = conteudo;
            DescricaoConteudo = $"";
            return this;
        }
    }
}