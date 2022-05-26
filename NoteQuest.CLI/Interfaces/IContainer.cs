﻿using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using NoteQuest.Application.Interface;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.CLI.Interfaces
{
    public interface IContainer
    {
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
        public IEntrarEmMasmorraService EntrarEmMasmorraService { get; set; }
        public IVerificarPortaService VerificarPortaService { get; set; }
        public IEntrarPelaPortaService EntrarPelaPortaService { get; set; }
        public IAbrirFechaduraService AbrirFechaduraService { get; set; }
        public IQuebrarPortaService QuebrarPortaService { get; set; }
        public ISairDeMasmorraService SairDeMasmorraService { get; set; }
        public IEscolhaFacade EscolhaFacade { get; set; }
    }
}