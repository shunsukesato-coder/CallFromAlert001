using System.Collections.Generic;

public class AlertData
{
    public string schemaId { get; set; }
    public AlertDataDetails data { get; set; }
}

public class AlertDataDetails
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
    // 必要に応じて他のプロパティを追加
}
