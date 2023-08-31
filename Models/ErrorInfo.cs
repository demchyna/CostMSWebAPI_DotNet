namespace CostMSWebAPI.Models;

public class ErrorInfo
{
    public int Status { get; set; }
    public required string Url { get; set; }
    public required string Message { get; set; }
}
