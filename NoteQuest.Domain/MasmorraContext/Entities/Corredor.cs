using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
	public class Corredor : Segmento
	{
        public Corredor(IPorta portaDeEntrada, string descricao) : base(portaDeEntrada, descricao)
		{
			Descricao = descricao;
		}
	}
}