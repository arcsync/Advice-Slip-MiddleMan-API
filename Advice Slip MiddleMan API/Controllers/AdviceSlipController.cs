using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            string randomAdviceSlip;
            randomAdviceSlip = APIClient.GetAdviceSlip().advice;
            return Ok(randomAdviceSlip);
        }

        // GET api/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string specificAdviceSlip;
            specificAdviceSlip = APIClient.GetAdviceSlip(id).advice;
            return Ok(specificAdviceSlip);
        }
    }
}
