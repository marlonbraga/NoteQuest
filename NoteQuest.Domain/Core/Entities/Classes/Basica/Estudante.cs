﻿using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.ObjectValue;
using System;

namespace NoteQuest.Domain.Core.Entities.Classes.Basica
{
    public class Estudante : IClasse
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
        public IAcao Acao { get; set; }

        public IAcao AtualizarAcao(IAcao acao) => acao;

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 0;
            Nome = "Estudante";
            Vantagem = "Começa o jogo com +3 Magia Básica aleatória.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Adaga", -1);
            QtdMagias = 3;
        }
    }
}