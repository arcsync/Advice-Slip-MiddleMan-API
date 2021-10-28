using Advice_Slip_MiddleMan_API;
using NUnit.Framework;
using System;

namespace Advice_Slip_MiddleMan_API_Tests

{
    public class APIClientTests
    {
        [Test]
        public void GetAdviceSlip_NoArgs_ReturnsJSON_Test()
        {
            //ARRANGE
            string testJSON;

            //ACT
            testJSON = APIClient.GetAdviceSlip();

            //ASSERT
            if (testJSON.StartsWith("{\"slip\":") && testJSON.EndsWith("}}"))
            {
                Assert.Pass();
            }
            else Assert.Fail();

        }

        [Test]
        public void GetAdviceSlip_WithID_Test_ReturnsProperJSON_Test()
        {
            //ARRANGE
            string testJSON;

            //ACT
            int lastslip = 224;               //amout of slips
            int zeroCorrected = lastslip - 1; //ID can not be 0
            Random RNG = new Random();
            int randomInt = RNG.Next()%zeroCorrected;
            testJSON = APIClient.GetAdviceSlip(randomInt);

            //ASSERT
            if (testJSON.StartsWith("{\"slip\":") && testJSON.EndsWith("}}"))
            {
                Assert.Pass();
            }
            else Assert.Fail();
        }


        [Test]
        public void getJSONFromForeignAPITest()
        {
            //ARRANGE
            string testJSON;
            //ACT
            testJSON = APIClient.GetAdviceSlip();
            //ASSERT

            Assert.Pass();
        }

        [Test]
        public void Test()
        {
            //ARRANGE

            //ACT

            //ASSERT

            Assert.Pass();
        }
    }
}
