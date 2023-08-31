using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CostMSWebAPI.Models;

[Table("tariff")]
public class Tariff
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("currency")]
    public required string Currency { get; set; }
    [Column("rate")]
    public decimal Rate { get; set; }
    [Column("begin_date")]
    public DateTime BeginDate { get; set; }

    [Column("end_date")] 
    public DateTime? EndDate { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("category_id")]
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    [Column("unit_id")]
    public long UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
    [JsonIgnore]
    public IEnumerable<Indicator> Indicators { get; set; } = new List<Indicator>();
}
