using Newtonsoft.Json;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.IO;

namespace NoteQuest.Infrastructure.Data.Masmorra
{
    public class MasmorraRepository : IClasseBasicaRepository
    {
        public IMasmorraData DadosDeMasmorra { get; set; }
        public IMasmorraNomes MasmorraNomenclatura { get; set; }

        public IMasmorraData PegarDadosMasmorra(string nomeMasmorra = "Palacio")
        {
            string json = LerArquivoTexto(@$"MasmorrasBasicas\{nomeMasmorra}.json");
            DadosDeMasmorra = ConverterDados(@$"{json}");
            return DadosDeMasmorra;
        }

        private string LerArquivoTexto(string nomeArquivo)
        {
            string CurrentDirectory = Directory.GetCurrentDirectory() + $@"\..\..\..\..\docs\configFiles\";
            string json = File.ReadAllText(CurrentDirectory + nomeArquivo);
            return json;
        }

        private MasmorraDataDTO ConverterDados(string json)
        {
            try
            {
                MasmorraDataDTO resultado = JsonConvert.DeserializeObject<MasmorraDataDTO>(json);
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        public IMasmorraNomes PegarNomesMasmorra()
        {
            string json = LerArquivoTexto("NomeDeMasmorra.json");
            MasmorraNomenclatura = ConverterNomes(json);
            return MasmorraNomenclatura;
        }
        private MasmorraNomesDTO ConverterNomes(string json)
        {
            try
            {
                MasmorraNomesDTO resultado = JsonConvert.DeserializeObject<MasmorraNomesDTO>(json);
                return resultado;
            }
            catch
            {
                throw;
            }
        }

    }
}
