using System.Text.Json.Serialization;

namespace AirCoil_API.Dto
{
    public class PredictResult
    {
        [JsonPropertyName("predictions")]
        public ICollection<int> Predictions { get; set; }
    }
}
