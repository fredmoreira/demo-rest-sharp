using System;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;
using Xunit.Abstractions;


namespace PhoneBookTest
{
    public class TestApiPhoneBook
    {
        private readonly ITestOutputHelper output;
        private readonly string Url ="http://localhost:5000";

        public TestApiPhoneBook(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void RestApiTestingGet()
        {
            RestClient client = new RestClient(Url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);          
            var statusCode = response.StatusCode;
            statusCode.Equals(HttpStatusCode.OK);
            response.Content.Equals("! :) :) API PHONE-BOOK UP :) :) !");
        }
        [Fact]
        public void RestApiTestingPost()
        {
            RestClient client = new RestClient(Url+"/contacts");
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { name = "TestRestSharp", mobilephone = "351986116979", homephone = "35133222233" });

            IRestResponse response = client.Execute(request);

            var statusCode = response.StatusCode;
            var responseContent = response.Content;
            //Assert http status
            statusCode.Equals(HttpStatusCode.Created);

            dynamic json = JValue.Parse(response.Content);
            string name = json.name;
            string mobilephone = json.mobilephone;
            string homephone = json.homephone;

            //Assert Body
            name.Equals("TestRestSharp");
            mobilephone.Equals("351986116979");
            homephone.Equals("35133222233");
        }
    }
}
