namespace CostMSWebAPI.DTOs;

public class IndicatorDto
{
    public long Id { get; set; }
    public long Current { get; set; }
    public DateTime Date { get; set; }
    public decimal Payment { get; set; }
    public string? Description { get; set; }
    public long Previous { get; set; }
    public long? PreviousId { get; set; }
    public long MeterId { get; set; }
    public long TariffId { get; set; }
    public decimal TariffRate { get; set; }
    public string? TariffCurrency { get; set; }
    public string? UnitName { get; set; }
    public decimal Price { get; set; }
}