using System.Text.Json.Serialization;

namespace Condominios.Models.Services.Classes
{
    public class AlertaEstado
    {
        [JsonPropertyName("Leyenda")]
        public string Leyenda { get; set; } = string.Empty;

        [JsonPropertyName("Estado")]
        public bool Estado { get; set; } 
    }
}
