using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.Core.DTO
{
	public class CombatConsequence : ActionResult, ISalaSegmentConsequence
	{
		public CombatConsequence(string descricao, BaseSegmento segment) : base(descricao)
		{
			Segment = segment;
		}

		public BaseSegmento Segment { get; set; }
		public Combat Combat { get; set; }
	}
}

/*
{
	"Consequences":
	[
		{
			"description":"",
			"executor":"",
			"target":"",
			"damage":0
		}
	],
	"Choices":
	[
		{"Atack":"∞"},
		{"Fireball":"■□□"},
		{"Ice Bean":"■■□"}
	]
}
 
 


[
	{Magia: Bola de fogo (character)}
	{Dano (monster, dano, vida, vidaMax)}
	{Morte (monster)}
	{Dano (monster, dano, vida, vidaMax)}
	{Morte (monster)}
	{Dano (monster, dano, vida, vidaMax)}
	{Morte (monster)}
	{Dano (monster, dano, vida, vidaMax)}
	{Morte (monster)}
	
	{CongelamentoMonstro (monster)}
	
	{AtaqueMonstro (monster, character)}
	{Dano (character, dano, vida, vidaMax)}
]
 */