namespace Condominios.Data.Interfaces
{
    public interface IEpoch
    {
        public long CrearEpoch(DateTime fecha);
        public DateTime ObtenerFecha(double epoch);
        public string ObtenerMesYAnio(DateTime fecha);
    }
}
