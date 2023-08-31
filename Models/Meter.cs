using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CostMSWebAPI.Models;

[Table("meter")]
public class Meter
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("name")]
    public required string Name { get; set; }
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
