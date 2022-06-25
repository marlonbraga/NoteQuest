using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Services
{
    public class PersonagemBuilder : IPersonagemBuilder
    {
        public IRacaRepository RacaRepository { get; set; }
        public IClasseRepository ClasseRepository { get; set; }

        public PersonagemBuilder(IRacaRepository racaBasicaRepository, IClasseRepository classeBasicaRepository)
        {
            RacaRepository = racaBasicaRepository;
            ClasseRepository = classeBasicaRepository;
        }

        public IPersonagem BuildPersonagem() => new Personagem();

        public IPersonagem BuildPersonagem(string nome, int indiceRaca, int indiceClasse)
        {
            IPersonagem personagem = new Personagem();
            IRaca raca = CriarRaca(indiceRaca);
            IClasse classe = CriarClasse(indiceClasse);
            personagem.Build(nome, raca, classe);

            return personagem;
        }

        public IPersonagem DecorateNome(IPersonagem personagem, string nome)
        {
            personagem.Nome = nome;

            return personagem;
        }

        public IPersonagem DecorateRaca(IPersonagem personagem, int indiceRaca)
        {
            personagem.Raca = CriarRaca(indiceRaca);
            personagem.Pv.AlterarMaximo(personagem.Raca.Pv);
            return personagem;
        }

        public IPersonagem DecorateClasse(IPersonagem personagem, int indiceClasse)
        {
            personagem.Classes ??= new List<IClasse>();
            personagem.Classes.Add(CriarClasse(indiceClasse));
            personagem.Pv.AlterarMaximo(personagem.Classes[0].Pv);
            personagem.Pv.RecuperarTudo();
            personagem.Inventario.Equipamentos.MaoDireita = personagem.Classes[0].ArmaInicial;

            return personagem;
        }

        private IRaca CriarRaca(int indice)
        {
            IRaca raca = RacaRepository.PegarRacaBasica(indice);
            raca.Build();

            return raca;
        }
        private IClasse CriarClasse(int indice)
        {
            IClasse classe = ClasseRepository.PegarClasseBasica(indice);
            classe.Build();

            return classe;
        }
    }
}
