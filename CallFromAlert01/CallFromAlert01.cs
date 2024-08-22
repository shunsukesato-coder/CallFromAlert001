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

            // JSON データからフィールドを抽出してログに出力
            var severity = data?.data?.essentials?.severity;
            var signalType = data?.data?.essentials?.signalType;
            var monitoringService = data?.data?.essentials?.monitoringService;
            var firedDateTime = data?.data?.essentials?.firedDateTime;
            var description = data?.data?.essentials?.description;
            var alertId = data?.data?.essentials?.alertId;
            var alertCategory = data?.data?.alertContext?.AlertCategory;

            log.LogInformation("Alert Details:\n" +
                               $"重大度: {severity}\n" +
                               $"Signal Type: {signalType}\n" +
                               $"Monitoring Service: {monitoringService}\n" +
                               $"発生日時: {firedDateTime}\n" +
                               $"説明: {description}\n" +
                               $"アラート ID: {alertId}\n" +
                               $"アラートカテゴリー: {alertCategory}");

            return new OkObjectResult("Alert received successfully");
        }
    }
}
