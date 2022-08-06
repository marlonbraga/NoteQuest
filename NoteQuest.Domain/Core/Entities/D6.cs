using System;

namespace NoteQuest.Domain.Core.Entities
{
    public static class D6
    {
        public static ushort Valor { get; set; }

        private static readonly Random random = new();

        public static ushort Rolagem(int qtdDados = 1)
        {
            Valor = 0;
            for (ushort i = 0; i < qtdDados; i++)
            {
                Valor += (ushort)random.Next(1, 6 + 1);
            }
            return Valor;
        }
    }
}
