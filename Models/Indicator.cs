using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostMSWebAPI.Models;

[Table("indicator")]
public class Indicator
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("current")]
    public long Current { get; set; }
    [Column("date")]
    public DateTime Date { get; set; }
    [Column("payment")]
    public decimal Payment { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("previous_id")] 
    public long? PreviousId { get; set; }
    public Indicator Previous { get; set; } = null!;
    [Column("meter_id")]
    public long MeterId { get; set;  }
    public Meter Meter { get; set; } = null!;
    [Column("tariff_id")]
    public long TariffId { get; set; }
    public Tariff Tariff { get; set; } = null!;
}
