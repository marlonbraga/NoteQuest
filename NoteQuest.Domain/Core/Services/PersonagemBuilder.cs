using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Dados;

namespace NoteQuest.Domain.Core.Services
{
    public class PersonagemBuilder
    {
        IRacaRepository RacaRepository { get; set; }
        IClasseRepository ClasseRepository { get; set; }

        public PersonagemBuilder(IRacaRepository racaBasicaRepository, IClasseRepository classeBasicaRepository)
        {
            RacaRepository = racaBasicaRepository;
            ClasseRepository = classeBasicaRepository;
        }

        public IRaca CriarRaca(int indice) => RacaRepository.PegarRacaBasica(indice);

        public IClasse CriarClasse(int indice) => ClasseRepository.PegarClasseBasica(indice);
    }
}
