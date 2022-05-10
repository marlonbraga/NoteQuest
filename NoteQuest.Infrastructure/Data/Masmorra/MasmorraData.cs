using NoteQuest.Domain.CombateContext.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

//namespace NoteQuest.Domain.MasmorraContext.ObjectValue
//{
//    public class MasmorraBuilder
//    {
//        public string Descrição { get; set; }
//        public TabelaSegmentos TabelaSegmentos { get; set; }
//        public TabelaPassagemSecreta[] TabelaPassagemSecreta { get; set; }
//        public TabelaArmadilha[] TabelaArmadilha { get; set; }
//        public TabelaConteúdo[] TabelaConteúdo { get; set; }
//        public TabelaMonstro[] TabelaMonstro { get; set; }
//        public TabelaRecompensa TabelaRecompensa { get; set; }
//        public TabelaChefeDaMasmorra[] TabelaChefeDaMasmorra { get; set; }
//        public TabelaArmadura[] TabelaArmadura { get; set; }
//        public TabelaArma[] TabelaArma { get; set; }

//        public MasmorraBuilder(int indice = 1)
//        {

//        }
//    }



//}

namespace NoteQuest.Infrastructure.Data.Masmorra
{
    public partial class MasmorraData : IMasmorraData
    {
        [JsonProperty("Descrição")]
        public string Descricao { get; set; }

        [JsonProperty("SegmentoInicial")]
        public Domain.MasmorraContext.Entities.SegmentoInicial SegmentoInicial { get; set; }

        [JsonProperty("TabelaSegmentos")]
        public ITabelaSegmentos TabelaSegmentos { get; set; }

        [JsonProperty("TabelaPassagemSecreta")]
        public ITabelaArmadilhaElement[] TabelaPassagemSecreta { get; set; }

        [JsonProperty("TabelaArmadilha")]
        public ITabelaArmadilhaElement[] TabelaArmadilha { get; set; }

        [JsonProperty("TabelaConteúdo")]
        public ITabelaConteudo[] TabelaConteudo { get; set; }

        [JsonProperty("TabelaMonstro")]
        public ITabelaMonstro[] TabelaMonstro { get; set; }

        [JsonProperty("TabelaRecompensa")]
        public ITabelaRecompensa TabelaRecompensa { get; set; }

        [JsonProperty("TabelaChefeDaMasmorra")]
        public ITabelaChefeDaMasmorra[] TabelaChefeDaMasmorra { get; set; }

        [JsonProperty("TabelaArmadura")]
        public ITabelaArmadura[] TabelaArmadura { get; set; }

        [JsonProperty("TabelaArma")]
        public ITabelaArma[] TabelaArma { get; set; }
    }

    public partial class SegmentoInicial : ISegmentoInicial
    {
        [JsonProperty("segmento")]
        public SegmentoTipo Segmento { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("portas")]
        public int Portas { get; set; }
    }

    public partial class TabelaArma : ITabelaArma
    {
        [JsonProperty("indice")]
        public int Indice { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("dano")]
        public string Dano { get; set; }

        [JsonProperty("caracteristicas", NullValueHandling = NullValueHandling.Ignore)]
        public string Caracteristicas { get; set; }
    }

    public partial class TabelaArmadilhaElement : ITabelaArmadilhaElement
    {
        [JsonProperty("indice")]
        public int Indice { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }
    }

    public partial class TabelaArmadura : ITabelaArmadura
    {
        [JsonProperty("indice")]
        public int Indice { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("pvs")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Pvs { get; set; }
    }

    public partial class TabelaChefeDaMasmorra : ITabelaChefeDaMasmorra
    {
        [JsonProperty("indice")]
        public int Indice { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("qtd")]
        public int Qtd { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("dano")]
        public int Dano { get; set; }

        [JsonProperty("pvs")]
        public int Pvs { get; set; }
    }

    public partial class TabelaConteudo : ITabelaConteudo
    {
        [JsonProperty("indice")]
        public int Indice { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("PassagemSecreta", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PassagemSecreta { get; set; }

        [JsonProperty("moedas", NullValueHandling = NullValueHandling.Ignore)]
        public string Moedas { get; set; }

        [JsonProperty("pergaminho", NullValueHandling = NullValueHandling.Ignore)]
        public string Pergaminho { get; set; }

        [JsonProperty("baú", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Baú { get; set; }

        [JsonProperty("ItensMagico", NullValueHandling = NullValueHandling.Ignore)]
        public string ItensMagico { get; set; }
    }

    public partial class TabelaMonstro : ITabelaMonstro
    {
        [JsonProperty("indice")]
        public int Indice { get; set; }

        [JsonProperty("qtd", NullValueHandling = NullValueHandling.Ignore)]
        public string Qtd { get; set; }

        [JsonProperty("nome", NullValueHandling = NullValueHandling.Ignore)]
        public string Nome { get; set; }

        [JsonProperty("dano", NullValueHandling = NullValueHandling.Ignore)]
        public int Dano { get; set; }

        [JsonProperty("pvs", NullValueHandling = NullValueHandling.Ignore)]
        public int Pvs { get; set; }

        [JsonProperty("caracteristicas")]
        public string Caracteristicas { get; set; }
    }
    public partial class TabelaRecompensa : ITabelaRecompensa
    {
        public ITabelaItemTesouro[] TabelaTesouro { get; set; }
        public ITabelaItemMaravilha[] TabelaMaravilha { get; set; }
        public ITabelaItemMagico[] TabelaItemMágico { get; set; }
    }

    public partial class TabelaItemTesouro : ITabelaItemTesouro
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }
    public partial class TabelaItemMaravilha : ITabelaItemMaravilha
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }
    public partial class TabelaItemMagico : ITabelaItemMagico
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public string Efeito { get; set; }
    }

    public partial class TabelaSegmentos : ITabelaSegmentos
    {
        [JsonProperty("TabelaAPartirDeEscadaria")]
        public ITabelaAPartirDe[] TabelaAPartirDeEscadaria { get; set; }

        [JsonProperty("TabelaAPartirDeCorredor")]
        public ITabelaAPartirDe[] TabelaAPartirDeCorredor { get; set; }

        [JsonProperty("TabelaAPartirDeSala")]
        public ITabelaAPartirDe[] TabelaAPartirDeSala { get; set; }
    }

    public partial class TabelaAPartirDe : ITabelaAPartirDe
    {
        public int Indice { get; set; }
        public SegmentoTipo Segmento { get; set; }
        public string Descricao { get; set; }
        public int Portas { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SegmentoConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type int");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (int)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class SegmentoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SegmentoTipo) || t == typeof(SegmentoTipo?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "corredor":
                    return SegmentoTipo.Corredor;
                case "escadaria":
                    return SegmentoTipo.Escadaria;
                case "sala":
                    return SegmentoTipo.Sala;
            }
            throw new Exception("Cannot unmarshal type Segmento");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SegmentoTipo)untypedValue;
            switch (value)
            {
                case SegmentoTipo.Corredor:
                    serializer.Serialize(writer, "corredor");
                    return;
                case SegmentoTipo.Escadaria:
                    serializer.Serialize(writer, "escadaria");
                    return;
                case SegmentoTipo.Sala:
                    serializer.Serialize(writer, "sala");
                    return;
            }
            throw new Exception("Cannot marshal type Segmento");
        }

        public static readonly SegmentoConverter Singleton = new SegmentoConverter();
    }
}
