namespace CostMSWebAPI.DTOs;

public class MeterDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long CategoryId { get; set; }
    public long UnitId { get; set; }
}