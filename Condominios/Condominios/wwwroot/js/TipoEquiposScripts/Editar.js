const api = new ApiClient();

//Abrir despegable
document.addEventListener('DOMContentLoaded', () => {

    var BtnEditar = document.querySelectorAll('.remover');

    var ContenedorDiv = document.getElementById("form-body");
    var Formulario = ContenedorDiv.querySelector("form");
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
        api.get(`TipoEquipos/ObtenerRegistro/${Parametro}`)
            .then(data => {
                var CapacidadValue = data.capacidad;
                var CapacidadInt = parseInt(CapacidadValue);
                var CapacidadString = CapacidadValue.replace(/[0-9\s]/g, '');

                var CapacidadSelect = document.getElementById("VarianteEquipo_CapacidadSelect");
                document.getElementById("VarianteEquipo_MarcaID").value = data.marcaID;
                document.getElementById("VarianteEquipo_MotorID").value = data.motorID;
                document.getElementById("VarianteEquipo_PeriodoID").value = data.periodoID;
                document.getElementById("VarianteEquipo_TipoEquipoID").value = data.tipoEquipoID;
                document.getElementById("VarianteEquipo_CapacidadValor").value = CapacidadInt;
                document.getElementById("VarianteEquipo_ID").value = data.id;

                for (let i = 0; i < CapacidadSelect.options.length; i++) {
                    const option = CapacidadSelect.options[i];
                    if (CapacidadString.trim() === '') {
                        option.selectedIndex = 0;
                        break;
                    } else if (option.text.trim() === CapacidadString.trim()) {
                        option.selected = true;
                        break;
                    }
                }
                Formulario.action = "/TipoEquipos/Actualizar";
                BotonEnviar.value = "Actualizar";

            })
            .catch(error => console.error('GET Error:', error));
    }


    function borrarDatosObtenidos() {
        document.getElementById("VarianteEquipo_MarcaID").value = "";
        document.getElementById("VarianteEquipo_MotorID").value = "";
        document.getElementById("VarianteEquipo_PeriodoID").value = "";
        document.getElementById("VarianteEquipo_TipoEquipoID").value = "";
        document.getElementById("VarianteEquipo_CapacidadValor").value = "";
    }













});



