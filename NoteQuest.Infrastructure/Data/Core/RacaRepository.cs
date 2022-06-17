using NoteQuest.Domain.Core.Entities.Raca.Basica;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Infrastructure.Data.Core
{
    public class RacaRepository : IRacaRepository
    {
        public Dictionary<int, IRaca> RacasBasicas { get; set; }

        public RacaRepository()
        {
            RacasBasicas = new Dictionary<int, IRaca>();
            RacasBasicas.Add(2, new HomemGosma());
            RacasBasicas.Add(3, new Vagaloide());
            RacasBasicas.Add(4, new Fada());
            RacasBasicas.Add(5, new Gnomo());
            RacasBasicas.Add(6, new Elfo());
            RacasBasicas.Add(7, new Humano());
            RacasBasicas.Add(8, new Anao());
            RacasBasicas.Add(9, new Pequenino());
            RacasBasicas.Add(10, new PovoGato());
            RacasBasicas.Add(11, new Rinoceroide());
            RacasBasicas.Add(12, new MeioDragao());
        }

        public IRaca PegarRacaBasica(int indice)
        {
            return RacasBasicas.GetValueOrDefault(indice);
        }
    }
}
