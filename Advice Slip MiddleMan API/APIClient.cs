using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Net;

namespace Advice_Slip_MiddleMan_API
{
    public class APIClient
    {

        private const string AS_API_ADDRESS = "https://api.adviceslip.com/advice";

        public static HttpClient httpClient{ get; set; }
        public static void CreateHttpClient()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static AdviceSlip GetAdviceSlip()
        {
            string connectionString = AS_API_ADDRESS;
            AdviceSlip adviceSlip = getAdviceSlipJSON<AdviceSlip>(APIAddress: connectionString);
            return adviceSlip;
        }


        public static AdviceSlip GetAdviceSlip(int Id) 
        {
            string connectionString = $"{AS_API_ADDRESS}/{Id}";
            AdviceSlip adviceSlip = getAdviceSlipJSON<AdviceSlip>(APIAddress: connectionString);
            return adviceSlip;
        }

        private static AdviceSlip getAdviceSlipJSON<AdviceSlip>(string APIAddress) where AdviceSlip: new()
        {
            using (WebClient webClient = new WebClient())
            {
                string rawJSON;
                try
                {
                    rawJSON = webClient.DownloadString(APIAddress);
                }
                catch (Exception)
                {

                    throw new Exception("Failed getting advice strip");
                }
                string fixedJSON = fixJSON(rawJSON);
                AdviceSlip packed = JsonConvert.DeserializeObject<AdviceSlip>(fixedJSON);
                //Slip unpacked = packed.slip;
                return packed;
            }
        }

        private static string fixJSON(string rawJSON)
        {
            if (rawJSON.EndsWith("}}"))
            {
                rawJSON = rawJSON.Replace("}}", "}");
            }
            rawJSON = rawJSON.Replace("{\"slip\": ", "");
            return rawJSON;
        }

        public static async Task<string> PullAdviceStripFromForeignAPI(string APIAddress)
        {
            using (HttpResponseMessage responseMessage = await httpClient.GetAsync(APIAddress))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string advice = await responseMessage.Content.ReadAsStringAsync();
                    return advice;
                }
                else
                {
                    throw new Exception("Advice Slip Request Failed");

                }
            }
        }
    }
}
