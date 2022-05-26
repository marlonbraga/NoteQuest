using NoteQuest.Domain.MasmorraContext.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraNomes
    {
        public Tipodemasmorra[] TipoDeMasmorra { get; set; }
        public Segundaparte[] SegundaParte { get; set; }
        public Terceiraparte[] TerceiraParte { get; set; }
    }
}
