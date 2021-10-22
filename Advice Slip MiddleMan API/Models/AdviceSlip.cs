using System;
using Newtonsoft.Json;

namespace Advice_Slip_MiddleMan_API
{

    public class AdviceSlip : ISerializeable
    {
        [JsonProperty("slip")]
        public Slip slip { get; set; }
    }

    public class Slip
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("advice")]
        public string advice { get; set; }
    }

}
