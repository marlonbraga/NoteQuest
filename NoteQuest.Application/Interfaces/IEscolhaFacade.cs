using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;

namespace NoteQuest.Application.Interface
{
    public interface IEscolhaFacade
    {
        IPortaEntrada PortaEntrada { get; set; }
        IClasseBasicaRepository MasmorraRepository { get; set; }
        IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        IVerificarPortaService VerificarPortaService { get; set; }
        IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        IAbrirFechaduraService AbrirFechaduraService { get; set; }
        IQuebrarPortaService QuebrarPortaService { get; set; }
        ISairDeMasmorraService SairDeMasmorraService { get; set; }
        ConsequenciaView EntrarEmMasmorra(IMasmorra masmorra);
        ConsequenciaView SelecionaEscolha(int escolha, int? indice);
    }
}