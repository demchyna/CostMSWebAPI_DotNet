using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CostMSWebAPI.Models;

[Table("user")]
public class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("first_name")]
    public string? FirstName { get; set; }
    [Column("last_name")]
    public string? LastName { get; set; }
    [Column("username")]
    public string? Username { get; set; }
    [Column("password")]
    public string? Password { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("create_date")]
    public DateTime CreateDate { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [JsonIgnore]
    public List<Role> Authorities { get; set; } = new();
    [JsonIgnore]
    public IEnumerable<Category>? Categories { get; set; }
    [JsonIgnore]
    public IEnumerable<Funds>? Funds { get; set; }
}
