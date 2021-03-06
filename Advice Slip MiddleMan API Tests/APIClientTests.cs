using Advice_Slip_MiddleMan_API;
using NUnit.Framework;
using System;

namespace Advice_Slip_MiddleMan_API_Tests

{
    public class APIClientTests
    {
        //Tests could be further improved with customised regex at a later date
        //The getJSONFromForeignAPI and fixJSON methods will not be tested as these are both private and a part of below tests.

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
            int lastslip = 224;               //amout of slips in the foreign API
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
        public void GetAdviceSlip_With_BAD_ID_Test_ReturnsProperJSON_Test()
        {
            //ARRANGE
            string testJSON;

            //ACT
            int badSlipID = 0;             
            testJSON = APIClient.GetAdviceSlip(badSlipID);

            //ASSERT
            if (testJSON == "{\"message\": {\"type\": \"error\", \"text\": \"Advice slip not found.\"}}")
            {
                Assert.Pass();
            }
            else Assert.Fail();
        }


        [Test]
        public void GetAdviceSlip_WithSearch_Test_ReturnsProperJSON_Test()
        {
            //ARRANGE
            string testJSON;
            string testQuery = "andandand";
            //ACT
            testJSON = APIClient.GetAdviceSlip(testQuery);
            //ASSERT
            if (testJSON == "{\"message\": {\"type\": \"notice\", \"text\": \"No advice slips found matching that search term.\"}}")
            {
                Assert.Pass();
            }
            else Assert.Fail();
        }

        
    }
}
