function ValidarFormulario() {

    function MostrarError(elemento, mensaje) {
        elemento.innerText = mensaje;
        elemento.style.display = 'inline';
    }

    function OcultarError(elemento) {
        elemento.style.display = 'none';
    }

    /*---------- Obtener valores de los campos ----------*/
    var Marca = document.getElementById('VarianteEquipo_MarcaID');
    var Motor = document.getElementById('VarianteEquipo_MotorID');
    var Periodo = document.getElementById('VarianteEquipo_PeriodoID');
    var TipoEquipo = document.getElementById('VarianteEquipo_TipoEquipoID');
    var CapacidadValor   = document.getElementById('VarianteEquipo_CapacidadValor');
    var CapacidadSelect = document.getElementById('VarianteEquipo_CapacidadSelect');

    /*---------- Seleccionar span para mostrar el mensaje ----------*/
    var MarcaError = document.getElementById('MarcaError');
    var MotorError = document.getElementById('MotorError');
    var PeriodoError = document.getElementById('PeriodoError');
    var TipoEquipoError = document.getElementById('TipoEquipoError');
    var CapacidadError = document.getElementById('CapacidadError');

    /*---------- Ocultar mensajes ----------*/
    OcultarError(MarcaError);
    OcultarError(MotorError);
    OcultarError(PeriodoError);
    OcultarError(TipoEquipoError);
    OcultarError(CapacidadError);

    var CamposFormulario = [
        { campo: Marca, error: MarcaError, mensaje: "Este campo es obligatorio" },
        { campo: Motor, error: MotorError, mensaje: "Este campo es obligatorio" },
        { campo: Periodo, error: PeriodoError, mensaje: "Este campo es obligatorio" },
        { campo: TipoEquipo, error: TipoEquipoError, mensaje: "Este campo es obligatorio" }
    ];

    var CampoVacio = false;

    CamposFormulario.forEach(function (Elemento) {
        if (!Elemento.campo.value) {
            MostrarError(Elemento.error, Elemento.mensaje);
            CampoVacio = true;
        } else {
            OcultarError(Elemento.error);
        }
    })

    if (CapacidadSelect.value != 0 && !CapacidadValor.value) {
        MostrarError(CapacidadError, "Ingrese una capacidad");
        CampoVacio = true;
    }
    return !CampoVacio;
}