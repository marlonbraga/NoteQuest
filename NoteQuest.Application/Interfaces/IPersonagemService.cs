﻿using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Application.Interfaces
{
    public interface IPersonagemService
    {
        public IPersonagem CriarPersonagem();
        public IPersonagem CriarPersonagem(string nome, int indiceRaca, int indiceClasse);
        public IPersonagem NomearPersonagem(IPersonagem personagem, string nome);
        public IPersonagem DefinirRaca(IPersonagem personagem, int indiceRaca);
        public IPersonagem DefinirClasse(IPersonagem personagem, int indiceClasse);
    }
}