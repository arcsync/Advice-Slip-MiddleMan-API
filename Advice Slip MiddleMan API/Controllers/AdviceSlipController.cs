using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Advice_Slip_MiddleMan_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AdviceSlipController : ControllerBase
    {
        private readonly ILogger<AdviceSlipController> _logger;

        public AdviceSlipController(ILogger<AdviceSlipController> logger)
        {
            _logger = logger;
        }

        // GET: api
        [HttpGet]
        public IActionResult Get()
        {
            string response;
            response = APIClient.GetAdviceSlip();
            ISerializeable serializeable;
            try
            {
                serializeable = JsonConvert.DeserializeObject<AdviceSlip>(response);
                if ((serializeable as AdviceSlip).id == 0)
                {
                    //Deserialize to Message if AdviceSlip is invalid
                    serializeable = JsonConvert.DeserializeObject<Message>(response);
                }
            }
            catch (JsonSerializationException)
            {
                throw new JsonSerializationException($"Unable to deserialize {response}");
            }
            
            //LEGACY
            //string serialized = JsonConvert.SerializeObject(serializeable);
            //return Ok(serialized);
            //Depracated because JsonResult gives a more elegant output
            JsonResult result = new JsonResult(serializeable);            
            return Ok(result);
        }

        // GET api/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string response;
            response = APIClient.GetAdviceSlip(id);
            ISerializeable serializeable;
            try
            {
                serializeable = JsonConvert.DeserializeObject<AdviceSlip>(response);
                if ((serializeable as AdviceSlip).id == 0)
                {
                    serializeable = JsonConvert.DeserializeObject<Message>(response);
                }
                
                
            }
            catch (JsonSerializationException)
            {
                throw new JsonSerializationException($"Unable to deserialize {response}"); 
            }
                 
            JsonResult result = new JsonResult(serializeable);
            return Ok(result);
        }

        //GET api/search/abc
        [HttpGet("search/{searchQuery}")]
        public  IActionResult Get(string searchQuery)
        {
            string response;
            response = APIClient.GetAdviceSlip(searchQuery);
            ISerializeable serializeable;
            try
            {
                serializeable = JsonConvert.DeserializeObject<Search>(response);
                if ((serializeable as Search).total_results == null)
                {
                    serializeable = JsonConvert.DeserializeObject<Message>(response);
                }
            }
            catch (JsonSerializationException)
            {
                throw new JsonSerializationException($"Unable to deserialize {response}");
            }
            
            JsonResult result = new JsonResult(serializeable);
            return Ok(result); ;
        }
    }
}
