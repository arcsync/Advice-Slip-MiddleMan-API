using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Advice_Slip_MiddleMan_API
{
    public static class APIClient
    {
        private const string AS_API_ADDRESS = "https://api.adviceslip.com/advice";

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
            if (rawJSON.EndsWith("}}") == false)
            {
                rawJSON = rawJSON + "}";
            }
            
            return rawJSON;
        }        
    }
}
