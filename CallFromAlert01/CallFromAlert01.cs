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

            // Request body を AlertData オブジェクトとしてデシリアライズ
            AlertData data = JsonConvert.DeserializeObject<AlertData>(requestBody);

            if (data != null)
            {
                // JSON データからフィールドを抽出してログに出力
                log.LogInformation("Alert Details:\n" +
                                   $"重大度: {data.data?.essentials?.severity}\n" +
                                   $"Signal Type: {data.data?.essentials?.signalType}\n" +
                                   $"Monitoring Service: {data.data?.essentials?.monitoringService}\n" +
                                   $"発生日時: {data.data?.essentials?.firedDateTime}\n" +
                                   $"説明: {data.data?.essentials?.description}\n" +
                                   $"アラート ID: {data.data?.essentials?.alertId}\n" +
                                   $"アラートカテゴリー: {data.data?.alertContext?.AlertCategory}");
            }
            else
            {
                log.LogWarning("Received request body could not be parsed.");
            }

            return new OkObjectResult("Alert received successfully");
        }
    }
}
