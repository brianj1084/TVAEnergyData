using Newtonsoft.Json;

namespace TVAEnergyData.EIAClient.Models
{
    public class Request
    {
        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("series_id")]
        public string SeriesId { get; set; }
    }
}