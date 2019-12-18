using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SJPayone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetUserController : PayOneBaseController
    {

        public GetUserController(IConfiguration config) : base(config)
        { }

        [HttpGet]
        public string Get()
        {
            var getUserObj = new
            {
                request = "getuser",
                userid = "123456789", // unique userid
                type = "userdata", // retrieves user data as structured JSON
            };

            return SendRequest(payone_settings, getUserObj);
        }
    }
}