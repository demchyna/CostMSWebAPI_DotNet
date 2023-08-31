using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CostMSWebAPI.Models;

[Table("unit")]
public class Unit
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [JsonIgnore] 
    public IEnumerable<Meter> Meters { get; set; } = new List<Meter>();
    [JsonIgnore]
    public IEnumerable<Tariff> Tariffs { get; set; } = new List<Tariff>();
}
