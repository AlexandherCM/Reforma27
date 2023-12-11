const api = new ApiClient();

document.addEventListener('DOMContentLoaded', () => {

    var BtnEditar = document.querySelectorAll('.remover');
    var Formulario = document.querySelector("form");
    var BotonEnviar = Formulario.querySelector('[type="submit"]');

    BtnEditar.forEach(function (element) {
        element.addEventListener('click', function (event) {
            var Parametro = event.currentTarget.getAttribute('data-parametro');
            var Desplegable = document.getElementById('form');
            var FlechaAbajo = document.getElementById('down');
            var FlechaArriba = document.getElementById('up');

            if (BotonEnviar.value == "Agregar") {
                ConsultaGET(Parametro);
            } else {
                BotonEnviar.value = "Agregar";
            }

            if (!Desplegable.classList.contains('contract') && !Desplegable.classList.contains('expanded')) {
                Desplegable.classList.add('expanded');
                FlechaAbajo.classList.add('d-none');
                FlechaArriba.classList.remove('d-none');
            } else if (Desplegable.classList.contains('expanded')) {
                borrarDatosObtenidos();
                Desplegable.classList.remove('expanded');
                Desplegable.classList.add('contract');
                FlechaAbajo.classList.remove('d-none');
                FlechaArriba.classList.add('d-none');
            } else if (Desplegable.classList.contains('contract')) {
                Desplegable.classList.remove('contract');
                Desplegable.classList.add('expanded');
                FlechaAbajo.classList.add('d-none');
                FlechaArriba.classList.remove('d-none');
            }
        });
    });

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

    function borrarDatosObtenidos() {
        document.getElementById("ID").value = "";
        document.getElementById("Nombre").value = "";
        document.getElementById("Numero").value = "";
        document.getElementById("Direccion").value = "";
        document.getElementById("Correo").value = "";
    }
});