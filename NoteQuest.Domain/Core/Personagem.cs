﻿using System.Collections.Generic;

namespace NoteQuest.Domain.Core
{
    public class Personagem
    {
        int PontosDeVida { get; set; }
        int PontosDeVidaMaximo { get; set; }
        string Nome { get; set; }
        string Raça { get; set; }
        List<string> Classe { get; set; }
        string Localidade { get; set; }
        string Segmento { get; set; }
    }
}
