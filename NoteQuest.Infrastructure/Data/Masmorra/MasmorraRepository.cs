using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.ObjectValue;
using System.IO;
using Newtonsoft.Json;
using System;

namespace NoteQuest.Infrastructure.Data.Masmorra
{
    public class MasmorraRepository : IMasmorraRepository
    {
        public MasmorraData DadosDeMasmorra { get; set; }

        public IMasmorraData PegarDadosMasmorra(string nomeMasmorra = "Palacio")
        {
            string json = LerArquivoTexto(@$"{nomeMasmorra}.json");
            DadosDeMasmorra = ConverterDados(json);
            return DadosDeMasmorra;
        }

        private string LerArquivoTexto(string nomeArquivo)
        {
            string CurrentDirectory = Directory.GetCurrentDirectory() + $@"\..\..\..\..\docs\configFiles\";
            string json = File.ReadAllText(CurrentDirectory + nomeArquivo);
            return json;
        }

        private MasmorraData ConverterDados(string json)
        {
            try
            {
                MasmorraData resultado = JsonConvert.DeserializeObject<MasmorraData>(json);
                return resultado;
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch
            {
                throw;
            }
        }
    }
}
