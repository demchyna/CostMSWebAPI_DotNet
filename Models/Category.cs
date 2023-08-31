using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CostMSWebAPI.Models;

[Table("category")]
public class Category
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("user_id")]
    public long UserId { get; set;  }
    public User User { get; set; } = null!; 
    [JsonIgnore]
    public IEnumerable<Meter> Meters { get; set; } = new List<Meter>();
    [JsonIgnore]
    public IEnumerable<Tariff> Tariffs { get; set; } = new List<Tariff>(); 
}
