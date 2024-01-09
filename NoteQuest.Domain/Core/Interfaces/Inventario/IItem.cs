﻿using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
