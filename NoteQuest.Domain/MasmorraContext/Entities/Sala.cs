using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Sala : BaseSegmento, ISegmento
    {
        public string DescricaoConteudo { get; set; }
        public string DescricaoMonstros { get; set; }
        public List<Monstro> Monstros { get; set; }
        public IConteudo Conteudo { get; set; }

        public Sala(IPortaComum portaDeEntrada, string descricao, int qtdPortas) : base(portaDeEntrada, descricao, qtdPortas)
        {
            Descricao = descricao;
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
            if(monstros.Count > 0)
            {
                Monstros = monstros;
                DescricaoMonstros = $"Nesse cômodo, encontra-se {monstros.Count} {monstros[0].Nome}(s) distraído(s) (PV:{monstros[0].PV}; Dano:{monstros[0].Dano}).";
                DetalhesDescricao += DescricaoMonstros;
            }
            return this;
        }
        public Sala AdicionaConteudo(IConteudo conteudo)
        {
            Conteudo = conteudo;
            DescricaoConteudo = $"Contém {conteudo.Descricao}";
            DetalhesDescricao += DescricaoConteudo;
            return this;
        }
    }
}