namespace QTC.Dashboard.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string[] Headers = new string[] { "Application Name", "Layer", "Module", "Alert", "AlertTeam", "Severity", "ServerName", "ErrorCode", "Error Message", "Error Date", "User", "View" };
    }
}