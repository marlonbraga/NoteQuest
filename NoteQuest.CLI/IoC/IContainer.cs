using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteQuest.Application;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem.Data;

namespace NoteQuest.CLI.IoC
{
    public interface IContainer
    {
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorra Masmorra { get; set; }

        public ISegmentoFactory SegmentoFactory { get; set; }
        public IArmadilhaFactory ArmadilhaFactory { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        //public IClasseBasicaRepository MasmorraRepository { get; set; }
        //public ISegmentoBuilder SegmentoFactory { get; set; }
        //public IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        //public IVerificarPortaService VerificarPortaService { get; set; }
        //public IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        //public IAbrirFechaduraService AbrirFechaduraService { get; set; }
        //public IQuebrarPortaService QuebrarPortaService { get; set; }
        //public ISairDeMasmorraService SairDeMasmorraService { get; set; }
        //public IEscolhaFacade EscolhaFacade { get; set; }
        public IRacaRepository RacaRepository { get; set; }
        public IClasseRepository ClasseRepository { get; set; }
        public IPersonagemBuilder PersonagemBuilder { get; set; }
        public IPersonagemService PersonagemService { get; set; }
    }
}
