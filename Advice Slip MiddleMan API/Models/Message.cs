using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Advice_Slip_MiddleMan_API
{   
    public class Message : ISerializeable
    {
        [JsonProperty("message")]
        public MessageBody message { get; set; }
    }

    public class MessageBody
    {
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("text")]
        public string text { get; set; }
    }

    
}
