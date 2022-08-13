using System.Text.Json.Serialization;

namespace Ticketing_App.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}