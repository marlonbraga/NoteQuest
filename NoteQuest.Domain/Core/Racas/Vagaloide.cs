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
    public class Vagaloide : IRaca
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
            Pv = 16;
            QtdMagias = 0;
            Nome = "Vagalóide";
            Vantagem = "Começa o jogo com 3 usos da Magia Básica: Luz.";
        }

        public ConsequenciaDTO Efeito()
        {
            throw new NotImplementedException();
        }
    }
}