using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
	public class Sala : ISegmento
    {
        public int IdSegmento { get; set; }
        public List<IPorta> Portas { get; set; }
		public string Descricao { get; set; }
		public string DescricaoConteudo { get; set; }
		public string DescricaoMonstros { get; set; }

		public string Entrar()
        {
			throw new System.NotImplementedException();
		}

		public void MoverEmSilencio(int valorD6)
        {
		}

		public void AcharPassagemSecreta(int valorD6)
		{
		}

		public void DesarmarArmadilhas(int valorD6)
        {
		}

		public void AbrirBau(int valorD6_1, int valorD6_2)
        {
		}
	}
}