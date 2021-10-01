using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [JsonProperty("@odata.context")]
        public string _Context { get; set; }

        [JsonProperty("@odata.nextLink")]
        public string _NextLink { get; set; }
    }
}
