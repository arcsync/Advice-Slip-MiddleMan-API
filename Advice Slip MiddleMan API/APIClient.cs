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

        public static string GetAdviceSlip()
        {
            string connectionString = AS_API_ADDRESS;
            string adviceSlip = getJSONFromForeignAPI<string>(APIAddress: connectionString);
            string fixedJSON = fixJSON(adviceSlip);
            return fixedJSON;
        }


        public static string GetAdviceSlip(int Id) 
        {
            string connectionString = $"{AS_API_ADDRESS}/{Id}";
            string adviceSlip = getJSONFromForeignAPI<string>(APIAddress: connectionString);
            string fixedJSON = fixJSON(adviceSlip);
            return fixedJSON;
        }

        public static string GetAdviceSlip(string searchQuery)
        {
            string connectionString = $"{AS_API_ADDRESS}/search/{searchQuery}";
            string adviceSlip = getJSONFromForeignAPI<string>(APIAddress: connectionString);
            return adviceSlip;
        }

        private static string getJSONFromForeignAPI<AdviceSlip>(string APIAddress)
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
                return rawJSON;
            }
        }

        

        private static string fixJSON(string rawJSON)
        {
            /* ADVICE SLIP JSON API returns some 
             * responses missing a trailing "}"
             * this is meant to be a quick hotfix for that
             * (eg. https://api.adviceslip.com/advice/1).
             */
            if (!rawJSON.EndsWith("}}"))
            {
                rawJSON = rawJSON + "}";
            }
            
            return rawJSON;
        }

        


        /* LEGACY
         * private static string unpackJSON(string rawJSON)
        {
             *ADVICE SLIP JSON API returns JSONS
             *encapsulated in a {rootobject} that
             *is not comfortable for JSON.NET to handle
             *this unpacks these JSONS by keeping only
             *the innermost brakets.
             *
            int start = rawJSON.LastIndexOf("{");
            int length = rawJSON.IndexOf("}") - start;
            return rawJSON.Substring(start, length);
        }
        */
        /*
         * LEGACY
        public static async Task<string> PullAdviceStripFromForeignAPI(string APIAddress)
        {
            using (HttpResponseMessage responseMessage = await httpClient.GetAsync(APIAddress))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string reply = await responseMessage.Content.ReadAsStringAsync();
                    return reply;
                }
                else
                {
                    throw new Exception("Advice Slip Request Failed");

                }
            }
        }
        */
    }
}
