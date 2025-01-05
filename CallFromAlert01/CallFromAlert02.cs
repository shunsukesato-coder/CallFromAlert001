using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CallFromAlert01
{
    public static class AlertFunction
    {
        [FunctionName("ReceiveAlertFunction02")]
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
                // AlertData オブジェクト全体を JSON にシリアライズしてログに出力
                string dataJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                log.LogInformation("Deserialized AlertData:
{ AlertDataJson}
                ", dataJson);
            }
            else
            {
                log.LogWarning("Received request body could not be parsed.");
            }

            return new OkObjectResult("Alert received successfully");
        }
    }

    // AlertData クラスの定義を含めてください（既存のクラス構造をここに追加）
    public class AlertData
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Essentials essentials { get; set; }
        public AlertContext alertContext { get; set; }
    }

    public class Essentials
    {
        public string severity { get; set; }
        public string signalType { get; set; }
        public string monitoringService { get; set; }
        public string firedDateTime { get; set; }
        public string description { get; set; }
        public string alertId { get; set; }
    }

    public class AlertContext
    {
        public string AlertCategory { get; set; }
    }
}
