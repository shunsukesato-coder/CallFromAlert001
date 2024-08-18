using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace CallFromAlert01.Functions
{
    public static class AlertFunction
    {
        [FunctionName("ReceiveAlertFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "alert")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            using var reader = new StreamReader(req.Body);
            var requestBody = await reader.ReadToEndAsync();

            log.LogInformation("Received request body: {RequestBody}", requestBody);

            // Request body を JSON として処理
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // 必要に応じて、data に対する操作をここで実行

            return new OkObjectResult("Alert received successfully");
        }
    }
}
