using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Models.ViewModels.CtrolVarianteEquipo
{
    public class VarianteEquipo
    {
        public bool Estado { get; set; }
        public int ID { get; set; }

        public int MarcaID { get; set; }

        public int MotorID { get; set; }

        public int PeriodoID { get; set; }

        public int TipoEquipoID { get; set; }

        public string ?CapacidadValor { get; set; }

        public int CapacidadSelect { get; set; }

        public string Capacidad(string Unidad)
            => $"{CapacidadValor} {(Unidad == null ? "" : Unidad)}".Trim();
    }
}
