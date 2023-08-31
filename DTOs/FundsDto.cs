namespace CostMSWebAPI.DTOs;

public class FundsDto
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string? Source { get; set; }
    public decimal Value { get; set; }
    public string? Currency { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public long UserId { get; set; }
}
