﻿using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities.Armadilhas;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Factories
{
    public class ArmadilhaFactory : IArmadilhaFactory
    {
        private IDictionary<TipoMasmorra, IArmadilha[]> Armadilhas;

        public ArmadilhaFactory(IMasmorraRepository masmorraRepository)
        {
            Armadilhas = new Dictionary<TipoMasmorra, IArmadilha[]>();
            TabelaArmadilhaElement[] tabelaDeArmadilhas;

            for (int i = 1; i <= 18; i++)
            {
                TipoMasmorra tipo = (TipoMasmorra)i;
                Armadilhas[tipo] = new IArmadilha[6];
                tabelaDeArmadilhas = masmorraRepository.PegarDadosMasmorra(/*tipo.ToString()*/).TabelaArmadilha;

                for (int j = 0; j < 6; j++)
                {
                    string descricao = tabelaDeArmadilhas[j].Descricao;
                    string efeito = tabelaDeArmadilhas[j]?.Efeito;
                    if(efeito is null)
                        continue;
                    if (efeito.Contains("lâmina"))
                    {
                        Armadilhas[tipo][j] = null;
                        continue;
                    }
                    if (efeito.Contains("dano"))
                    {
                        string danoRaw = GetIndex(efeito);
                        int dano = 0;
                        _ = int.TryParse(danoRaw, out dano);
                        IArmadilha armadilha = new SofrerDano(descricao, dano);
                        Armadilhas[tipo][j] = armadilha;

                        continue;
                    }
                    if (efeito.Contains("tocha"))
                    {
                        Armadilhas[tipo][j] = null;
                        continue;
                    }
                    if (efeito.Contains("luta"))
                    {
                        Armadilhas[tipo][j] = null;
                        continue;
                    }
                    if (efeito.Contains("morte"))
                    {
                        Armadilhas[tipo][j] = null;
                        continue;
                    }
                    if (efeito.Contains("decapitação"))
                    {
                        Armadilhas[tipo][j] = null;
                        continue;
                    }
                    if (efeito.Contains("amputação"))
                    {
                        Armadilhas[tipo][j] = null;
                        continue;
                    }
                    Armadilhas[tipo][j] = null;
                }
            }
        }

        private string GetIndex(string baseText, string startMark = "[", string finalMark = "]")
        {
            int start = baseText.IndexOf(startMark) + startMark.Length;
            if (start < 0) return baseText;
            int end = baseText.IndexOf(finalMark);
            if (end < 0) return baseText;

            string result = baseText.Substring(start, end - start);
            return result;
        }

        public IArmadilha GeraArmadilha(IMasmorra masmorra, int? indice = null)
        {
            indice ??= D6.Rolagem(1, true);
            return Armadilhas[masmorra.TipoMasmorra][(int)indice];
        }
    }
}
