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
    var Contacto = document.getElementById('Contacto');

    var Numero = document.getElementById('Numero');
    var NumeroValue = Numero.value;

    var Direccion = document.getElementById('Direccion');

    var Correo = document.getElementById('Correo');
    var CorreoValue = Correo.value;

    var Empresa = document.getElementById('Empresa');
    var Servicio = document.getElementById('Servicio');
    //console.log(CorreoValue);

    /*---------- Seleccionar span para mostrar el mensaje ----------*/
    var ContactoError = document.getElementById('ContactoError');
    var EmpresaError = document.getElementById('EmpresaError');
    var ServicioError = document.getElementById('ServicioError');
    var NumeroError = document.getElementById('NumeroError');
    var DireccionError = document.getElementById('DireccionError');
    var CorreoError = document.getElementById('CorreoError');

    /*---------- Ocultar mensajes ----------*/
    OcultarError(EmpresaError);
    OcultarError(ServicioError);
    OcultarError(ContactoError);
    OcultarError(NumeroError);
    OcultarError(DireccionError);
    OcultarError(CorreoError);

    var CamposFormulario = [
        { campo: Empresa, error: EmpresaError, mensaje: "Este campo es obligatorio" },
        { campo: Contacto, error: ContactoError, mensaje: "Este campo es obligatorio" },
        { campo: Servicio, error: ServicioError, mensaje: "Este campo es obligatorio" },
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
        CampoVacio = true;
    }

    return !CampoVacio;
}