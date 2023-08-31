using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CostMSWebAPI.Models;

[Table("role")]
public class Role
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [JsonIgnore]
    public IEnumerable<User> Users { get; set; } = new List<User>();
}
