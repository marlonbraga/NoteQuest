using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class SairDeMasmorra : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public SairDeMasmorra()
        {
            GatilhoDeAcao = GatilhoDeAcao.SairDeMasmorra;
            Efeito = delegate { return Executar(); };
            Titulo = "Sair de Masmorra";
            Descricao = "Voltar para a cidade";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            Console.WriteLine("[[Opção em desenvolvimento]]");
            return null;
        }
    }
}
