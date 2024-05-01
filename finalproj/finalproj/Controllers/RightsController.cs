using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using finalproj.BL;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks; // Required for async methods

namespace finalproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightsController : ControllerBase
    {
        // GET: api/<RightsController>
        [HttpGet]
        //public ActionResult<IEnumerable<Rights>> Get()
        //{
        //    Rights rights = new Rights();
        //    return rights.Read();
        //}


        [HttpPost("QueryChatGPT")]
        public async Task<ActionResult<string>> QueryChatGPT(User user, string prompt)
        {
            var apiKey = "sk-proj-e9pm7g1TiGVogJcpdl4NT3BlbkFJUiBRvtn7TWkZO8czTBVT";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var content = new StringContent("{\"model\": \"gpt-3.5-turbo\"," +
                " \"messages\": [{\"role\": \"system\", " +
                "\"content\": \"You are an expert assistant for IFRS standards and compliance.\"}, " +
                "{\"role\": \"user\", \"content\": \"How do I calculate lease liabilities under IFRS 16?\"}]}",
                Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            string result = await response.Content.ReadAsStringAsync();

            return result;
        }


        //[HttpPost("QueryEntitlements")]
        //public async Task<ActionResult<string>> QueryEntitlements(User user, string prompt)
        //{
        //    var apiKey = "your_api_key_here";
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        //    var content = new StringContent(JsonConvert.SerializeObject(new
        //    {
        //        model = "gpt-3.5-turbo",
        //        messages = new[]
        //        {
        //    new { role = "system", content = "You are an expert assistant for understanding user entitlements." },
        //    new { role = "user", content = prompt }
        //}
        //    }), Encoding.UTF8, "application/json");

        //    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
        //    string result = await response.Content.ReadAsStringAsync();

        //    return result;
        //}
    }
}