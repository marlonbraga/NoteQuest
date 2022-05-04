using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core
{
    public class D6 : ID6
    {
        public int Valor { get; set; }

        private readonly Random random = new();

        public int Rolagem(int qtdDados = 1)
        {
            Valor = 0;
            for (int i = 0; i < qtdDados; i++)
            {
                Valor += random.Next(1, 6+1);
            }
            return Valor;
        }
    }
}
