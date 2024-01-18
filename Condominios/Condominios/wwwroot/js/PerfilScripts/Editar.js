const api = new ApiClient();

var Formulario = document.querySelector("form");
var BotonEnviar = Formulario.querySelector('[type="submit"]');
var BtnEditar = document.querySelectorAll('.remover');

var Desplegable = document.getElementById('despegableUP');
var Flecha = document.getElementById('up');

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
            ConsultaGETUsuario(Parametro);

        }
    });
});

function AbrirDesplegable() {
    Desplegable.classList.add('mostrarUP');
}

function CerrarDesplegable() {
    Desplegable.classList.remove('mostrarUP');
}


function ConsultaGETUsuario(Parametro) {
        api.SendGet(`Perfil/ObtenerRegistro/${Parametro}`)
            .then(data => {

                document.getElementById("ID").value = data.user.id;
                document.getElementById("DatosUser_Nombre").value = data.user.nombre;
                document.getElementById("DatosUser_Correo").value = data.user.correo;

            })
            .catch(error => console.error('GET Error:', error));
    }