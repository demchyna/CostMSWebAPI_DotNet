namespace CostMSWebAPI.DTOs;

public class TariffDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Currency { get; set; }
    public decimal Rate { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public long CategoryId { get; set; }
    public long UnitId { get; set; }
    public string? UnitName { get; set; }
}