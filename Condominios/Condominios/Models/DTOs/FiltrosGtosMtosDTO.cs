using Condominios.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Condominios.Models.DTOs
{
    public class FiltrosGtosMtosDTO : FiltrosDTO
    {
        public int ProveedorID { get; set; }
        public long FechaEpoch1 { get; set; }
        public long FechaEpoch2 { get; set; } 

        [DisplayFormat(DataFormatString = "Fecha 1", ApplyFormatInEditMode = true)]
        public DateTime? Fecha1 { get; set; }

        [DisplayFormat(DataFormatString = "Fecha 2", ApplyFormatInEditMode = true)]
        public DateTime? Fecha2 { get; set; }

        public FiltrosDTO ConverDateToEpoch(IEpoch epoch)
        {
            FechaEpoch1 = epoch.CrearEpoch(Fecha1 ?? new());
            FechaEpoch2 = epoch.CrearEpoch(Fecha2 ?? new());
            // - - - - - - - - - - - - - - - - - - - 
            return new()
            {
                MarcaID = MarcaID,
                TipoID = TipoID,
                UbicacionID = UbicacionID,
                MotorID = MotorID,
            };
        }

    }
}
