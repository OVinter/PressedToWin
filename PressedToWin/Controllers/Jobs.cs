using HttpRequestsLibrary;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PressedToWin.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PressedToWin.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/jobs")]
    [ApiController]
    public class Jobs : ControllerBase
    {
        [HttpGet("GetJobs")]
/*        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
*/        public async Task<IActionResult> GetAttachmentsList()
        {
            var token = Request.GetAuthToken();
            var httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"{token}");

                var response = await httpClient.GetStringAsync("https://prismapreparego.tst.cppinter.net/jst/api/v1.0/jobs/cloudbox?pageSize=1&filters=%5B%5D&dateFormat=en-gb");

                var jstJobs = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                var jobs = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(response);
                return Ok(jobs);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var a = new List<string>() { "test" };
            return Ok(a);
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendPhoneMessage([FromBody] PhoneMessage phoneMessage)
        {
            var token = Request.GetAuthToken();
            string accountSid = "ACf31c38901ce0744d9c3526fc0b504e53";
            string authToken = "37003ac7b9c6b64162a3cb801d099c4d";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: phoneMessage.message,
                from: new Twilio.Types.PhoneNumber("+13203789578"),
                to: new Twilio.Types.PhoneNumber($":{phoneMessage.phoneNumber}")//+40743357281
            );

            Console.WriteLine(message.Sid);

            return Ok();
        }
    }
}
