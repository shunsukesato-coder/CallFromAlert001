using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly ILogger<AlertController> _logger;

        public AlertController(ILogger<AlertController> logger) => _logger = logger;

        [HttpPost]
        public async Task<IActionResult> ReceiveAlert()
        {
            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            _logger.LogInformation("Received request body: {RequestBody}", requestBody);

            // Request body を JSON として処理
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // 必要に応じて、data に対する操作をここで実行

            return Ok("Alert received successfully");
        }
    }
}
