using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using NoteQuest.Domain.Core.ObjectValue;
using System.Collections.Generic;
using System;

namespace NoteQuest.Domain.Core.Entities.Classes.Basica
{
    public class Chaveiro : IClasse
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public IArma ArmaInicial { get; set; }
        public int Dano { get; set; }
        public int QtdMagias { get; set; }
        public Type TipoAcao { get; set; }
        public IAbrirFechaduraService Acao { get; }

        public void Build()
        {
            //TODO: GetType não pega Interface!
            //NoteQuest.Domain.Core.Entities.Classes.Basica.Chaveiro.Acao.get returned null.

            TipoAcao = Acao.GetType();
            Pv = 2;
            Nome = "Chaveiro";
            Vantagem = "Não gasta tochas ao Abrir Fechaduras.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Adaga",-1);
            QtdMagias = 0;
        }

        public IAcao AtualizarAcao(IAcao acao)
        {
            //TODO: Inicializar TipoAcao
            if (acao.GetType() == TipoAcao)
            {
                acao.Execucao = Efeito;
                return acao;
            }
            return acao;
        }
        
        public ConsequenciaDTO Efeito()
        {
            IAcaoPorta acaoPorta = (IAcaoPorta)Acao;
            acaoPorta.Porta.SegmentoAlvo = acaoPorta.Porta.SegmentoAlvo ?? acaoPorta.SegmentoFactory.GeraSegmento(acaoPorta.Porta, D6.Rolagem());
            ISegmento novoSegmento = acaoPorta.Porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Você destranca a fechadura com successo e consegue espiar um novo segmento da masmorra.";
            texto += $"\n  Porém o processo foi demorado. A iluminação cessou te colocando outra vez na escuridão.";
            texto += $"\n  #{novoSegmento.IdSegmento}";
            texto += $"\n  {novoSegmento.Descricao}";
            texto += novoSegmento.DetalhesDescricao;
            ConsequenciaDTO consequencia = new()
            {
                Descricao = texto,
                Segmento = novoSegmento,
                //TODO: Verifica se é uma sala recem criada e passa a Escolha de gerar Conteudo e Monstros
                Escolhas = novoSegmento.RecuperaTodasAsEscolhas()
            };

            return consequencia;
        }
    }
}