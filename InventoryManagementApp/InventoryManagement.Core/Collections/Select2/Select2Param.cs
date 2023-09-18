using Newtonsoft.Json;

namespace InventoryManagement.Core.Collections.Select2
{
    public class Select2Param
    {
        [JsonProperty("term")]
        public string Term { get; set; }

        [JsonProperty("q")]
        public string Q { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }
    }
}