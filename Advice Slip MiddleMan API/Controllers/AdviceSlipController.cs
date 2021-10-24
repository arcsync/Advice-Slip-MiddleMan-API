using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

//TODO: Reduce boilerpate

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

        const string SerializationExceptionMessage = "There was a problem getting data from a provider, please try again later.";

        // GET: api
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("getting json from foreign API (no args)");
            string response;
            response = APIClient.GetAdviceSlip();
            ISerializeable serializeable;
            try
            {
                _logger.LogDebug("attepting deserialization as AdviceSlip");
                serializeable = JsonConvert.DeserializeObject<AdviceSlip>(response);
                if ((serializeable as AdviceSlip).slip == null)
                {
                    _logger.LogWarning("Advice slip seems invalid, attepting deserialization as Message");
                    //Deserialize to Message if AdviceSlip is invalid
                    serializeable = JsonConvert.DeserializeObject<Message>(response);
                }
            }
            catch (JsonSerializationException e)
            {
                _logger.LogError($"{e} \n Unable to deserialize {response}");
                serializeable = new Message { message = new MessageBody {
                    type = "Internal Error", text = SerializationExceptionMessage } };
            }
            
            //LEGACY
            //string serialized = JsonConvert.SerializeObject(serializeable);
            //return Ok(serialized);
            //Depracated because JsonResult gives a more elegant output
            JsonResult result = new JsonResult(serializeable);
            result = populateResultFields(result);
            return Ok(result);
        }

        // GET api/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"getting json from foreign API with ID: {id}");
            string response;
            response = APIClient.GetAdviceSlip(id);
            ISerializeable serializeable;
            try
            {
                _logger.LogDebug("attepting deserialization as AdviceSlip");
                serializeable = JsonConvert.DeserializeObject<AdviceSlip>(response);
                if ((serializeable as AdviceSlip).slip == null)
                {
                    _logger.LogWarning("Advice slip seems invalid, attepting deserialization as Message");
                    serializeable = JsonConvert.DeserializeObject<Message>(response);
                }
                
                
            }
            catch (JsonSerializationException e)
            {
                _logger.LogError($"{e} \n Unable to deserialize {response}");
                serializeable = new Message
                {
                    message = new MessageBody
                    {
                        type = "Internal Error",
                        text = SerializationExceptionMessage
                    }
                };
            }
                 
            JsonResult result = new JsonResult(serializeable);
            result = populateResultFields(result);
            return Ok(result);
        }

        //GET api/search/abc
        [HttpGet("search/{searchQuery}")]
        public  IActionResult Get(string searchQuery)
        {
            _logger.LogInformation($"getting json from foreign API with query: {searchQuery}");
            string response;
            response = APIClient.GetAdviceSlip(searchQuery);
            ISerializeable serializeable;
            try
            {
                _logger.LogDebug("attepting deserialization as Search");
                serializeable = JsonConvert.DeserializeObject<Search>(response);
                if ((serializeable as Search).total_results == null)
                {
                    _logger.LogWarning("Search result seems invalid, attepting deserialization as Message");
                    serializeable = JsonConvert.DeserializeObject<Message>(response);
                }
            }
            catch (JsonSerializationException e)
            {
                _logger.LogError($"{e} \n Unable to deserialize {response}");
                serializeable = new Message
                {
                    message = new MessageBody
                    {
                        type = "Internal Error",
                        text = SerializationExceptionMessage
                    }
                };
            }
            
            JsonResult result = new JsonResult(serializeable);
            result = populateResultFields(result);
            return Ok(result);
        }

        private JsonResult populateResultFields(JsonResult result)
        {
            result.ContentType = "application/json";
            result.StatusCode = 200;
            return result;
        }
    }
}
