using Newtonsoft.Json;

namespace e.mix.Models
{
    public class BuscaCEPResult
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("cidade")]
        public string Cidade { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }

        [JsonProperty("unidade")]
        public string Unidade { get; set; }

        [JsonProperty("ibge")]
        public int IBGECode { get; set; }

        [JsonProperty("gia")]
        public int? GIACode { get; set; }
    }
}
