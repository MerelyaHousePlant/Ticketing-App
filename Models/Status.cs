using System.Text.Json.Serialization;

namespace Ticketing_App.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        To_Do = 1,
        In_Progress = 2,
        Done = 3
    }
}