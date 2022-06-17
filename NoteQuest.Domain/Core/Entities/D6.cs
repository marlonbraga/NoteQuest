using System;

namespace NoteQuest.Domain.Core.Entities
{
    public static class D6
    {
        public static int Valor { get; set; }

        private static readonly Random random = new();

        public static int Rolagem(int qtdDados = 1)
        {
            Valor = 0;
            for (int i = 0; i < qtdDados; i++)
            {
                Valor += random.Next(1, 6 + 1);
            }
            return Valor;
        }
    }
}
