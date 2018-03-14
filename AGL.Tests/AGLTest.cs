using AGL.Service.Service;
using NUnit.Framework;
using System.Net.Http;

namespace AGL.Tests
{

    [TestFixture]
    public class AGLTest
    {
        private DisplayService _displayService;
        private string url = "http://agl-developer-test.azurewebsites.net/people.json";
        [SetUp]
        public void Initialize()
        {
            _displayService = new DisplayService(); 
        }       

        [TestCase]
        public void GetAPIData_Connection_Failure()
        {
            var failurl = "http://agl-developer-test.aewebsites.net/people.json";
            
            Assert.ThrowsAsync<HttpRequestException>(() =>  _displayService.GetAPIData(failurl));
        }

        [TestCase]
        public void GetAPIData_Connection_Success()
        {
            Assert.DoesNotThrowAsync(() => _displayService.GetAPIData(url));
        }

        [TestCase]
        public void GetCatsGroupedByOwnerGender_Gender_Count_Success()
        {
            var catList = _displayService.GetCatsGroupedByOwnerGender(url);
            Assert.AreEqual(2, catList.Count);
        }

        [TestCase]
        public void GetCatsGroupedByOwnerGender_Gender_Count_Failure()
        {
            var catList = _displayService.GetCatsGroupedByOwnerGender(url);
            Assert.AreNotEqual(3, catList.Count);
        }

        [TestCase]
        public void GetCatsGroupedByOwnerGender_Not_Null_Success()
        {
            var catList = _displayService.GetCatsGroupedByOwnerGender(url);
            Assert.IsNotNull(catList);
        }

        [TestCase]
        public void GetCatsGroupedByOwnerGender_Validate_Result_Success()
        {
            var catList = _displayService.GetCatsGroupedByOwnerGender(url);
            foreach (var item in catList)
            {
                foreach (var cat in item.CatNames)
                {
                    Assert.IsTrue(_displayService.VerifyOwnersGender(url, cat, item.Gender));
                }

            }
        }

        [TearDown]
        public void EndTests()
        {
            _displayService = null;
        }
    }
}
