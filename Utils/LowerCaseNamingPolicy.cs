using System.Text.Json;

namespace CostMSWebAPI.Utils;

public class LowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.ToLower();
}