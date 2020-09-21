using Newtonsoft.Json;

namespace TVAEnergyData.EIAClient.Models
{
    public class Series
    {
        [JsonProperty("series_id")]
        public string SeriesId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("f")]
        public string F { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("data")]
        public Datum[][] Data { get; set; }
    }
}