const api = new ApiClient();
export function EditarProveedorEquipo(Pagina) {

    // Datos del formulario
    var Formulario = document.querySelector("form");
    var BotonEnviar = Formulario.querySelector('[type="submit"]');
    var BtnEditar = document.querySelectorAll('.remover');

    // Datos del desplegable
    var Desplegable = document.getElementById('despegableUP');
    var Flecha = document.getElementById('up');

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
                if (Pagina == "Proveedor") {
                    ConsultaGETProveedor(Parametro);
                } else if (Pagina == "TipoEquipo") {
                    ConsultaGETTipoEquipo(Parametro);
                } else if (Pagina == "Equipo") {
                    ConsultaGETEquipo(Parametro);

                }
            }
        });
    });

    function AbrirDesplegable() {
        Desplegable.classList.add('mostrarUP');
        Flecha.src = '/images/up.svg';
    }

    function resetearFormulario() {
        Formulario.reset();
        UltimoBotonPresionado = null;
        if (Pagina == "TipoEquipo") {
            Formulario.action = "/TipoEquipos/Agregar";
        } else if (Pagina == "Proveedor") {
            Formulario.action = "/Proveedores/Agregar";
        }
        BotonEnviar.value = "Agregar";
    }

    function CerrarDesplegable() {
        Desplegable.classList.remove('mostrarUP');
        Flecha.src = '/images/down.svg';
        resetearFormulario();
    }

    Flecha.addEventListener('click', function () {
        if (Flecha.src.endsWith('/images/down.svg')) {
            resetearFormulario();
        }
    });

    function ConsultaGETTipoEquipo(Parametro) {
        api.SendGet(`TipoEquipos/ObtenerRegistro/${Parametro}`)
            .then(data => {

                var CapacidadValue = data.capacidad;
                var CapacidadInt = parseFloat(CapacidadValue);
                var CapacidadString = CapacidadValue.replace(/^\d*(?:\.\d+)?\s*/, '');

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
                        CapacidadSelect.selectedIndex = 0;
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

    function ConsultaGETProveedor(Parametro) {
        api.SendGet(`Proveedores/ObtenerRegistro/${Parametro}`)
            .then(data => {

                document.getElementById("ID").value = data.id;
                document.getElementById("Contacto").value = data.contacto;
                document.getElementById("Empresa").value = data.empresa;
                document.getElementById("Servicio").value = data.servicio;
                document.getElementById("Numero").value = data.telefono;
                document.getElementById("Direccion").value = data.direccion;
                document.getElementById("Correo").value = data.correo;
                Formulario.action = "/Proveedores/Actualizar";
                BotonEnviar.value = "Actualizar";

            })
            .catch(error => console.error('GET Error:', error));
    }
    //////////////////////////////////////////////////////////
    //function ConsultaGETEquipo(Parametro) {
    //    api.SendGet(`Equipos/ObtenerRegistro/${Parametro}`)
    //        .then(data => {

    //            var VarianteSelect = document.getElementById("Plantilla_VarianteID");
    //            var StatusSelect = document.getElementById("Plantilla_EstatusID");
    //            var UbicacionSelect = document.getElementById("Plantilla_UbicacionID");

    //            for (let i = 0; i < VarianteSelect.options.length; i++) {
    //                const option = VarianteSelect.options[i];
    //                if (option.value == data.varianteID) {
    //                    option.selected = true;
    //                    break;
    //                }
    //            }

    //            for (let i = 0; i < StatusSelect.options.length; i++) {
    //                const option = StatusSelect.options[i];
    //                if (option.value == data.estatusID) {
    //                    option.selected = true;
    //                    break;
    //                }
    //            }

    //            document.getElementById("Plantilla_CostoAdquisicion").value = data.costoAdquisicion;

    //            for (let i = 0; i < UbicacionSelect.options.length; i++) {
    //                const option = UbicacionSelect.options[i];
    //                if (option.value == data.ubicacionID) {
    //                    option.selected = true;
    //                    break;
    //                }
    //            }

    //            document.getElementById("Plantilla_Funcion").value = data.funcion;
    //        })
    //        .catch(error => console.error('GET Error:', error));
    //}
}

document.addEventListener('DOMContentLoaded', EditarProveedorEquipo);
