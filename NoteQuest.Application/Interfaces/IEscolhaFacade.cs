using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;

namespace NoteQuest.Application.Interface
{
    public interface IEscolhaFacade
    {
        public IPortaEntrada PortaEntrada { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }
        public IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        public IVerificarPortaService VerificarPortaService { get; set; }
        public IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        public IAbrirFechaduraService AbrirFechaduraService { get; set; }
        public IQuebrarPortaService QuebrarPortaService { get; set; }
        public ISairDeMasmorraService SairDeMasmorraService { get; set; }
        public ConsequenciaDTO EntrarEmMasmorra(IMasmorra masmorra);
    }
}