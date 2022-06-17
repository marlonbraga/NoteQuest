using NoteQuest.Domain.Core.DTO;
using System;

namespace NoteQuest.Domain.Core.Interfaces
{
    //public enum GatilhoDeAcao : int
    //{
    //    //Masmorra
    //    AbrirPorta = 0,
    //    ArromarPorta = 1,
    //    AbrirFechadura = 2,
    //    AbrirUmBau = 3,
    //    AcharPassagemSecreta = 4,
    //    DesarmarArmadilha = 5,
    //    EntrarPelaPorta = 6,
    //    MoverEmSilencio = 7,
    //    EntrarEmMasmorra = 8,
    //    SairDeMasmorra = 9,
    //    VerificarPorta = 10,
    //    CairEmArmadilha = 11,
    //    // Combate
    //    Atacar = 0,
    //    SofrerGolpe = 0,
    //    Matar = 0,
    //    Morrer = 0,
    //    VencerLuta = 0,
    //    VencerChefe = 0,
    //    UsarMagia = 0,
    //    SofrerMagia = 0,
    //    //Item
    //    UsarItem = 0,
    //    EsgotarItem = 0,
    //    GastarTocha = 0,
    //    GastarProvisao = 0,
    //    AdicionarItem = 0,
    //    DescartarItem = 0,

    //}

    public interface IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public ConsequenciaDTO Executar();
    }
}
