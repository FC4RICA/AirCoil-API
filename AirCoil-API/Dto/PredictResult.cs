using System.Text.Json.Serialization;

namespace AirCoil_API.Dto
{
    public class PredictResult
    {
        [JsonPropertyName("predictions")]
        public IList<int> Predictions { get; set; } = new List<int>();
    }
}
