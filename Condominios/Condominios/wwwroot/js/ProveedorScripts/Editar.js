const api = new ApiClient();

document.addEventListener('DOMContentLoaded', () => {


    // Datos del formulario
    var Formulario = document.querySelector("form");
    var BotonEnviar = Formulario.querySelector('[type="submit"]');
    var BtnEditar = document.querySelectorAll('.remover');

    // Datos del desplegable
    var Desplegable = document.getElementById('despegableUP');
    var Flecha = document.getElementById('up');

    //var FlechaAbajo = document.getElementById('up');
    //var FlechaArriba = document.getElementById('up');

    // Bandera
    var UltimoBotonPresionado = null;

    BtnEditar.forEach(function (element) {
        element.addEventListener('click', function (event) {

            var Parametro = event.currentTarget.getAttribute('data-parametro');

            if (UltimoBotonPresionado === Parametro) {
                CerrarDesplegable();
                UltimoBotonPresionado = null;
                Formulario.reset();
            } else {
                AbrirDesplegable();
                UltimoBotonPresionado = Parametro;
                ConsultaGET(Parametro);
            }
        });
    });

    function AbrirDesplegable() {
        Desplegable.classList.add('mostrarUP');
        Flecha.src = '/images/up.svg';
    }

    function CerrarDesplegable() {
        Desplegable.classList.remove('mostrarUP');
        Flecha.src = '/images/down.svg';
        Formulario.action = "/Proveedores/Agregar";
        BotonEnviar.value = "Agregar";
    }

    function ConsultaGET(Parametro) {
        api.get(`Proveedores/ObtenerRegistro/${Parametro}`)
            .then(data => {

                document.getElementById("ID").value = data.id;
                document.getElementById("Nombre").value = data.nombre;
                document.getElementById("Numero").value = data.telefono;
                document.getElementById("Direccion").value = data.direccion;
                document.getElementById("Correo").value = data.correo;
                Formulario.action = "/Proveedores/Actualizar";
                BotonEnviar.value = "Actualizar";

            })
            .catch(error => console.error('GET Error:', error));
    }
    console.log(Flecha.src);

    if (Flecha.src == '/images/up.svg') {
        console.log("hola");
        //Formulario.reset();
        //Formulario.action = "/Proveedores/Agregar";
        //BotonEnviar.value = "Agregar";
    }
    //FlechaArriba.addEventListener('click', function () {
    //    Formulario.reset();
    //    Formulario.action = "/Proveedores/Agregar";
    //    BotonEnviar.value = "Agregar";
    //});
});
