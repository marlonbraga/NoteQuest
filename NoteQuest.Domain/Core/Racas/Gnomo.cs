﻿using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core.Racas
{
    public class Gnomo : IRaca
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public int QtdMagias { get; set; }
        public string Vantagem { get; set; }
        public Type TipoAcao { get; set; }
        public IAcao Acao { get; set; }

        public IAcao AplicaEfeito(IAcao acao)
        {
            return acao;
        }

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 14;
            QtdMagias = 0;
            Nome = "Gnomo";
            Vantagem = "Começa o jogo com 3 usos de Magias Básicas aleatórias.";
        }

        public ConsequenciaDTO Efeito()
        {
            throw new NotImplementedException();
        }
    }
}
