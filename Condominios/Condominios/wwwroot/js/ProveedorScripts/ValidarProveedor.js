function ValidarFormulario() {
    function MostrarError(elemento, mensaje) {
        elemento.innerText = mensaje;
        elemento.style.display = 'inline';
    }

    function OcultarError(elemento) {
        elemento.style.display = 'none';
    }

    /*---------- Expresiones regulares ----------*/
    var ValidarNumero = /^\d{10}$/;
    var ValidarEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    /*---------- Obtener valores de los campos ----------*/
    var Nombre = document.getElementById('Nombre');
    var Numero = document.getElementById('Numero');
    var NumeroValue = Numero.value;
    var Direccion = document.getElementById('Direccion');
    var Correo = document.getElementById('Correo');
    var CorreoValue = Correo.value;
    console.log(CorreoValue);

    /*---------- Seleccionar span para mostrar el mensaje ----------*/
    var NombreError = document.getElementById('NombreError');
    var NumeroError = document.getElementById('NumeroError');
    var DireccionError = document.getElementById('DireccionError');
    var CorreoError = document.getElementById('CorreoError');

    /*---------- Ocultar mensajes ----------*/
    OcultarError(NombreError);
    OcultarError(NumeroError);
    OcultarError(DireccionError);
    OcultarError(CorreoError);

    var CamposFormulario = [
        { campo: Nombre, error: NombreError, mensaje: "Este campo es obligatorio" },
        { campo: Numero, error: NumeroError, mensaje: "Este campo es obligatorio" },
        { campo: Direccion, error: DireccionError, mensaje: "Este campo es obligatorio" },
        { campo: Correo, error: CorreoError, mensaje: "Este campo es obligatorio" }
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

    if (!ValidarNumero.test(NumeroValue) && Numero.value !== "") {
        MostrarError(NumeroError, "Por favor, ingresa un número de teléfono válido");
        CampoVacio = true;
    }

    if (!ValidarEmail.test(CorreoValue) && CorreoValue !== "") {
        MostrarError(CorreoError, "Por favor, ingresa una dirección de correo electrónico válida");
    }

    return !CampoVacio;
}