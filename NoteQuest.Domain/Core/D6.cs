using System;

namespace NoteQuest.Domain.Core
{
    public static class D6
    {
        public static int Valor { get; set; }

        private static readonly Random random = new();

        public static int Rolagem(int qtdDados = 1, bool deslocamento = false)
        {
            Valor = 0;
            for (int i = 0; i < qtdDados; i++)
            {
                Valor += random.Next(1, 7);
            }
            if(deslocamento)
                return Valor - qtdDados;
            return Valor;
        }
    }
}
