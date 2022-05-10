using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
	public class Corredor : BaseSegmento
	{
        public Corredor(IPortaComum portaDeEntrada, string descricao) : base(portaDeEntrada, descricao)
		{
			Descricao = descricao;
		}
	}
}