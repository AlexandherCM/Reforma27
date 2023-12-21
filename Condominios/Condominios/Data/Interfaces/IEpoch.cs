namespace Condominios.Data.Interfaces
{
    public interface IEpoch
    {
        public long CrearEpoch(DateTime fecha);
        public DateTime ObtenerFecha(long epoch);
        public string ObtenerMesYAnio(DateTime fecha);
    }
}
