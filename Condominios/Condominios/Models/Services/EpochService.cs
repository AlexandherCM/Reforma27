using Condominios.Data.Interfaces;
using System.Globalization;

namespace Condominios.Models.Services
{
    public class EpochService : IEpoch
    {
        public long CrearEpoch(DateTime fecha)
        {
            DateTime fechaInicial = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long epoch = (long)Math.Round((fecha.ToUniversalTime() - fechaInicial).TotalSeconds, 0);

            return epoch;
        }

        public DateTime ObtenerFecha(long epoch)
        {
            DateTime fechaInicial = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            fechaInicial = fechaInicial.AddSeconds(epoch).ToLocalTime();

            return fechaInicial;
        }

        public string ObtenerMesYAnio(DateTime fecha)
        {
            string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(fecha.Month);
            nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);

            // Formatear la cadena "Mes del Año"
            string resultado = $"{nombreMes} del {fecha.Year}";
            return resultado;
        }
    }
}
