using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CostMSWebAPI.Models;

[Table("funds")]
public class Funds
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("date")]
    public DateTime Date { get; set; }
    [Column("source")]
    public string? Source { get; set; }
    [Column("value")]
    public decimal Value { get; set; }
    [Column("currency")]
    public string? Currency { get; set; }
    [Column("type")]
    public FundsType Type { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }
    public User User { get; set; } = null!;
}
