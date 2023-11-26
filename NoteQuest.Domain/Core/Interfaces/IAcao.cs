using NoteQuest.Domain.Core.DTO;
using System;

namespace NoteQuest.Domain.Core.Interfaces
{
    public enum GatilhoDeAcao : int
    {
        #region Masmorra
        AbrirPorta = 0,
        ArromarPorta = 1,
        AbrirFechadura = 2,
        AbrirUmBau = 3,
        AcharPassagemSecreta = 4,
        DesarmarArmadilha = 5,
        EntrarPelaPorta = 6,
        MoverEmSilencio = 7,
        EntrarEmMasmorra = 8,
        SairDeMasmorra = 9,
        VerificarPorta = 10,
        CairEmArmadilha = 11,
        #endregion 

        #region Combate
        Atacar = 12,
        SofrerGolpe = 13,
        Matar = 14,
        Morrer = 15,
        VencerLuta = 16,
        VencerChefe = 17,
        UsarMagia = 18,
        SofrerMagia = 19,
        #endregion

        #region Item
        UsarItem = 20,
        EsgotarItem = 21,
        GastarTocha = 22,
        GastarProvisao = 23,
        AdicionarItem = 24,
        DescartarItem = 25,
        #endregion
    }

    public enum AcaoTipo
    {
        PortaFrente,
        PortaDireita,
        PortaTras,
        PortaEsquerda,
        Segmento,
        Batalha,
        Nulo = 99
    }

    public interface IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public ConsequenciaDTO Executar(int? indice = null);
    }
}
