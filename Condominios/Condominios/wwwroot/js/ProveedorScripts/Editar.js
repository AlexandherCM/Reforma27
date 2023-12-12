const api = new ApiClient();

document.addEventListener('DOMContentLoaded', () => {


    // Datos del formulario
    var Formulario = document.querySelector("form");
    var BotonEnviar = Formulario.querySelector('[type="submit"]');
    var BtnEditar = document.querySelectorAll('.remover');

    // Datos del desplegable
    var Desplegable = document.getElementById('form');
    var FlechaAbajo = document.getElementById('down');
    var FlechaArriba = document.getElementById('up');

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
        if (!Desplegable.classList.contains('contract') && !Desplegable.classList.contains('expanded')) {
            Desplegable.classList.add('expanded');
            FlechaAbajo.classList.add('d-none');
            FlechaArriba.classList.remove('d-none');
        } else if (Desplegable.classList.contains('contract')) {
            Desplegable.classList.remove('contract');
            Desplegable.classList.add('expanded');
            FlechaAbajo.classList.add('d-none');
            FlechaArriba.classList.remove('d-none');
        }
    }

    function CerrarDesplegable() {
        Desplegable.classList.remove('expanded');
        Desplegable.classList.add('contract');
        FlechaAbajo.classList.remove('d-none');
        FlechaArriba.classList.add('d-none');
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
    FlechaArriba.addEventListener('click', function () {
        Formulario.reset();
        Formulario.action = "/Proveedores/Agregar";
        BotonEnviar.value = "Agregar";
    });
});
