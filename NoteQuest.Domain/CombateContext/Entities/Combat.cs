using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoteQuest.Domain.CombateContext.Entities
{
    public class Combat
    {
        public List<Monstro> Monsters { get; set; }
        public List<Personagem> Characters { get; set; }
        public BaseSegmento Local { get; set; }
        public Combat(List<Personagem> characters, List<Monstro> monsters)
        {
            Characters = characters;
            Monsters = monsters;
        }
        public Combat() { }

        public CombatConsequence ChoiceAtaque(Personagem character, Monstro monster)
        {
            //character;
            //monster;
            List<string> descriptions = new();

            string weaponName = character.Inventario.Equipamentos.MaoDireita.Nome;
            
            //TODO: deve passar pela chain of responsability
            string atackDescription = $"{character.Nome} golpeou {monster.Nome} com {weaponName}";
            descriptions.Add(atackDescription);

            //TODO: deve passar pela chain of responsability
            int dano;
            int vidaTotalMonstro;
            int vidaRestanteMonstro;
            string danoDescription = $"{monster.Nome} sofreu {dano} de dano";
            descriptions.Add(danoDescription);

            //TODO: Verificar se todos os personagens já jogaram



            //TODO: Verificar se todos os monstros já jogaram

            return new CombatConsequence() { Combat = this, Descricao = atackDescription };
        }

    }
}
