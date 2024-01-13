using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Sala : BaseSegmento, ISegmento
    {
        public string DescricaoConteudo { get; set; }
        public string DescricaoMonstros { get; set; }
        public List<Monstro> Monstros { get; set; }

        public Sala(IPortaComum portaDeEntrada, string descricao, int qtdPortas) : base(portaDeEntrada, descricao, qtdPortas)
        {
            Descricao = descricao;
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
            Escolhas = GerarEscolhasBasicas(conteudo);
            
            return this;
        }

        private IDictionary<OpcaoSala, IEscolha> GerarEscolhasBasicas(IConteudo conteudo)
        {
            IDictionary<OpcaoSala, IEscolha> escolhasDeProcuraEmSala = new Dictionary<OpcaoSala, IEscolha>();

            if (conteudo.QtdItens > 0)
            {
                IEvent acaoVasculharRepositorio = new VasculharRepositorio(this);
                Escolha vasculharRepositorio = new(acaoVasculharRepositorio);
                escolhasDeProcuraEmSala.Add(new KeyValuePair<OpcaoSala, IEscolha>(OpcaoSala.saquear, vasculharRepositorio));
            }

            for (int i = 0; i < conteudo.Bau; i++)
            {
                IEvent acaoAbrirUmBau = new AbrirUmBau(this);
                Escolha abrirUmBau = new(acaoAbrirUmBau);
                escolhasDeProcuraEmSala.Add(new KeyValuePair<OpcaoSala, IEscolha>(OpcaoSala.bau, abrirUmBau));
            }

            if (conteudo.PassagemSecreta is true)
            {
                IEvent acaoAcharPassagemSecreta = new AcharPassagemSecreta(this);
                Escolha acharPassagemSecreta = new(acaoAcharPassagemSecreta);
                escolhasDeProcuraEmSala.Add(new KeyValuePair<OpcaoSala, IEscolha>(OpcaoSala.procurar, acharPassagemSecreta));
            }

            IEvent acaoDesarmarArmadilhas = new DesarmarArmadilhas(this);
            Escolha desarmarArmadilhas = new(acaoDesarmarArmadilhas);
            escolhasDeProcuraEmSala.Add(new KeyValuePair<OpcaoSala, IEscolha>(OpcaoSala.desarmar, desarmarArmadilhas));

            return escolhasDeProcuraEmSala;
        }

        public void AdicionarCadaver(IPersonagem personagem)
        {
            IEvent acaoVasculharRepositorio = null;//new VasculharRepositorio(this);
            Escolha vasculharRepositorio = new(acaoVasculharRepositorio);
            Escolhas.Add(new KeyValuePair<OpcaoSala, IEscolha>(OpcaoSala.cadaver, vasculharRepositorio));
        }
    }
}