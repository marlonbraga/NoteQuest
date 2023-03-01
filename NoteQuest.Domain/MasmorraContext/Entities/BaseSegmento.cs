using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public enum SegmentoTipo
    {
        sala, corredor, escadaria
    }

    public abstract class BaseSegmento : ISegmento
    {
        public int Nivel { get; }
        public int IdSegmento { get; set; }
        public static int ContagemDeSalas { get; set; }
        public string Descricao { get; set; }
        public string DetalhesDescricao { get; set; }
        public Tuple<int, int> Dimensoes { get; set; }
        public IDictionary<Direcao, Tuple<int, IPorta>> Passagens { get; set; }
        public IList<IEscolha> Escolhas { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }

        public BaseSegmento(ISegmentoBuilder segmentoFactory)
        {
            SegmentoFactory = segmentoFactory;
        }

        public void Build(IPorta portaDeEntrada, string descricao, int qtdPortas)
        {
            IdSegmento = ContagemDeSalas++;
            IPorta porta = null;
            if (portaDeEntrada is IPortaComum comum)
            {
                porta = comum.InvertePorta();
                porta.SegmentoAtual = this;
            }
            Passagens = new Dictionary<Direcao, Tuple<int, IPorta>>();
            Direcao direcao = InverterDirecao(portaDeEntrada.Direcao);
            Passagens.Add(direcao, new(1, porta));
            Dimensoes = GeraDimensoes(descricao);
            Descricao = descricao;
            DetalhesDescricao = string.Empty;
            Escolhas = GerarEscolhasBasicas();
            GerarPortas(qtdPortas);
        }

        public void Build(string descricao, int qtdPortas)
        {
            ContagemDeSalas = 0;
            IdSegmento = ContagemDeSalas++;
            Dimensoes = GeraDimensoes(descricao);
            Descricao = descricao;
            Escolhas = GerarEscolhasBasicas();
            GerarPortas(qtdPortas);
        }

        private Direcao InverterDirecao(Direcao direcao)
        {
            return direcao switch
            {
                Direcao.frente => Direcao.tras,
                Direcao.direita => Direcao.esquerda,
                Direcao.tras => Direcao.frente,
                Direcao.esquerda => Direcao.direita,
                _ => Direcao.frente
            };
        }

        private Tuple<int, int> GeraDimensoes(string descricao)
        {
            Tuple<int, int> dimensoes = new(0, 0);
            Random random = new();
            if (descricao.ToLower().Contains("pequen"))
                dimensoes = new(random.Next(3,5), random.Next(3, 5));
            else if (descricao.ToLower().Contains("median"))
                dimensoes = new(random.Next(6, 10), random.Next(6, 10));
            else if (descricao.ToLower().Contains("salão") || descricao.ToLower().Contains("cumprid"))
                dimensoes = new(random.Next(6, 10), random.Next(8, 10)*2);
            else if (descricao.ToLower().Contains("corredor"))
                dimensoes = new(2, 5);
            else if (descricao.ToLower().Contains("escadaria"))
                dimensoes = new(2, 5);
            //TODO: Adicionar rotação para segmentos não quadrados
            return dimensoes;
        }

        public void DesarmarArmadilhas(int valorD6)
        {

        }

        private IList<IEscolha> GerarEscolhasBasicas()
        {
            IAcao acaoDesarmarArmadilhas = new DesarmarArmadilhasService();
            Escolha desarmarArmadilhas = new(acaoDesarmarArmadilhas, null);
            IAcao acaoAcharPassagemSecreta = new AcharPassagemSecretaService();
            Escolha acharPassagemSecreta = new(acaoAcharPassagemSecreta, null);
            List<IEscolha> escolhas = new() { desarmarArmadilhas, acharPassagemSecreta };
            return escolhas;
        }

        private void GerarPortas(int qtdPortas)
        {
            for (int i = 1; i <= qtdPortas; i++)
            {
                Direcao direcao = RecuperaDirecaoAleatoria();
                int posicao = RecuperaPosicaoDePassagem(direcao);

                IPortaComum porta = SegmentoFactory.CriarPortaComum(this, direcao);
                porta.Build(this, direcao);
                Passagens.Add(direcao, new(posicao, porta));
            }
        }

        private Direcao RecuperaDirecaoAleatoria()
        {
            Random random = new ();
            int i;
            Direcao direcao;
            do
            {
                i = random.Next(0, 4);
                direcao = (Direcao)i;
            } while (Passagens.ContainsKey(direcao));

            return direcao;
        }

        private int RecuperaPosicaoDePassagem(Direcao direcao)
        {
            Random random = new ();
            int posicao = 0;

            if (direcao == Direcao.frente || direcao == Direcao.tras)
                posicao = random.Next(1, Math.Max(1,Dimensoes.Item1-2));
            if (direcao == Direcao.esquerda || direcao == Direcao.direita)
                posicao = random.Next(1, Math.Max(1, Dimensoes.Item2-2));

            return posicao;
        }

        public IList<IEscolha> RecuperaEscolhasDePortas()
        {
            List<IEscolha> escolhas = new();
            foreach (var passagem in Passagens)
            {
                IPorta porta = passagem.Value.Item2;
                escolhas.AddRange(porta.Escolhas);
            }
            return escolhas;
        }

        public IList<IEscolha> RecuperaTodasAsEscolhas()
        {
            //TODO: Adicionar demais escolhas em segmento
            //List<IEscolha> escolhas = GerarEscolhasBasicas();
            List<IEscolha> escolhas = new();
            escolhas.AddRange(RecuperaEscolhasDePortas());
            return escolhas;
        }
    }
}
