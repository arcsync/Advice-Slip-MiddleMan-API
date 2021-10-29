using NUnit.Framework;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Advice_Slip_MiddleMan_API.Controllers;
using Moq;
using Advice_Slip_MiddleMan_API;

namespace Advice_Slip_MiddleMan_API_Tests
{
    public class AdviceSlipControllerTests
    {   
        //TODO could be more specific
        [Test]
        public void Get_Random_Slip_Test() 
        {
            //ARRANGE
            var logger = Mock.Of<ILogger<AdviceSlipController>>();
            IActionResult testResult;
            AdviceSlipController testController = new AdviceSlipController(logger);
            //ACT
            testResult = testController.Get();

            //ASSERT
            Assert.IsNotNull(testResult);
        }

        [Test]
        public void Get_Slip_by_ID_Test()
        {
            //ARRANGE
            var logger = Mock.Of<ILogger<AdviceSlipController>>();
            IActionResult testResult;
            AdviceSlipController testController = new AdviceSlipController(logger);
            //ACT
            int testSlipID = 1;
            testResult = testController.Get(testSlipID);

            //ASSERT
            Assert.IsNotNull(testResult);
        }
        [Test]
        public void Get_Search_Results_Test()
        {
            //ARRANGE
            var logger = Mock.Of<ILogger<AdviceSlipController>>();
            IActionResult testResult;
            AdviceSlipController testController = new AdviceSlipController(logger);
            //ACT
            string testQuery = "and";
            testResult = testController.Get(testQuery);

            //ASSERT
            Assert.IsNotNull(testResult);
        }
    }
}