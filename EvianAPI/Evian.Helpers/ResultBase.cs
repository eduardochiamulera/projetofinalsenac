using Newtonsoft.Json;
using System.Collections.Generic;

namespace Evian.Helpers
{
    public class ResultBase<T>
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }

        public bool HasNext
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_NextLink);
            }
        }

        [JsonProperty("context")]
        public string _Context { get; set; }

        [JsonProperty("nextLink")]
        public string _NextLink { get; set; }
    }
}
