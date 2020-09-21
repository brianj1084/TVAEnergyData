using TVAEnergyData.EIAClient.Converters;

namespace TVAEnergyData.EIAClient.Models
{
    using Newtonsoft.Json;

    public class EIAResponse
    {
        [JsonProperty("request")]
        public Request Request { get; set; }

        [JsonProperty("series")]
        public Series[] Series { get; set; }

        public static EIAResponse FromJson(string json) => JsonConvert.DeserializeObject<EIAResponse>(json, Converter.Settings);

        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }
}
