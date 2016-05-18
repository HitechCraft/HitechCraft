namespace WebApplication.Domain.Json
{
    public enum JsonStatus
    {
        NO,
        YES
    }

    public class JsonStatusData
    {
        public JsonStatus Status { get; set; }

        public string Message { get; set; }
    }
}