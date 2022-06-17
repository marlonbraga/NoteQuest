using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Infrastructure.Data.Core
{
    public class MagiaRepository : IMagiaRepository
    {
        public Dictionary<int, IMagia> MagiasBasicas { get; set; }

        public MagiaRepository()
        {
            //MagiasBasicas.Add(1, new Cura());
            //MagiasBasicas.Add(2, new Luz());
            //MagiasBasicas.Add(3, new Teletransporte());
            //MagiasBasicas.Add(4, new RaioDeGelo());
            //MagiasBasicas.Add(5, new Relampago());
            //MagiasBasicas.Add(6, new BolaDeFogo());
        }

        public IMagia PegarMagiaBasica(int indice)
        {
            return MagiasBasicas.GetValueOrDefault(indice);
        }
    }
}
