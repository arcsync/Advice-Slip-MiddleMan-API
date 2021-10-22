using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Advice_Slip_MiddleMan_API
{
    public class Search : ISerializeable
    {
        [JsonProperty("total_results")]
        public string total_results { get; set; }
        [JsonProperty("query")]
        public string query { get; set; }
        [JsonProperty("slips")]
        public Slip[] slips { get; set; }
        //Note that Slip is not AdviceSlip
        
    }
}
