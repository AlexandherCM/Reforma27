export function InicializarEditar() {
    var BtnEditar = document.querySelectorAll('.remover');
    var BtnCancelar = document.querySelectorAll('.Cancelar');

    BtnCancelar.forEach(function (btn) {
        btn.classList.add('d-none');
    });

    BtnCancelar.forEach(function (element) {
        element.addEventListener('click', function (event) {
            var Formulario = event.target.form;
            var BotonCancelar = Formulario.querySelector('[value="Cancelar"]');
            var BotonEnviar = Formulario.querySelector('[type="submit"]');

            BotonCancelar.classList.add('d-none');
            BotonEnviar.value = "Agregar";
            Formulario.reset();

        });
    });


    BtnEditar.forEach(function (element) {
        element.addEventListener('click', function (event) {

            
            var Parametro = event.currentTarget.getAttribute('data-parametro');
            var ParametroFormulario = event.currentTarget.getAttribute('data-formulario');
            var Formulario = document.getElementById(ParametroFormulario);
            var BotonEnviar = Formulario.querySelector('[type="submit"]');
            var BotonCancelar = Formulario.querySelector('[value="Cancelar"]');

            ConsultaGET(Formulario, Parametro);

            if (BotonEnviar.value == "Agregar") {
                BotonEnviar.value = "Actualizar";
                BotonCancelar.classList.remove('d-none');
            }
        });

        function ConsultaGET(Formulario, Parametro) {
            const api = new ApiClient();
            var InputNombre = Formulario.querySelector('[name="Nombre"]');
            var InputHidden = Formulario.querySelector('[name="InputHidden"]');
            InputHidden.value = Parametro;
            api.get(`Catalogos/ObtenerRegistro/${Parametro}`)
                .then(data => {

                    switch (Formulario.id) {
                        case "FormsCrearMarca":
                            const MarcaID = data.marcas.find(marca => marca.id === parseInt(Parametro));
                            InputNombre.value = MarcaID.nombre;
                            break;
                        case "FormsCrearUbicacion":
                            const UbicacionID = data.ubicaciones.find(ubicaciones => ubicaciones.id === parseInt(Parametro));
                            InputNombre.value = UbicacionID.nombre;
                            break;
                        case "FormsCrearMotor":
                            const MotorID = data.motores.find(motores => motores.id === parseInt(Parametro));
                            InputNombre.value = MotorID.nombre;
                            break;
                        case "FormsCrearPeriodo":
                            var InputMeses = Formulario.querySelector('[name="Cantidad"]');
                            var Select = Formulario.querySelector('[name="Mes"]');
                            const PeriodoID = data.periodos.find(periodos => periodos.id === parseInt(Parametro));
                            InputNombre.value = PeriodoID.nombre;
                            //InputMeses.value = PeriodoID.meses;
                            if (PeriodoID.meses >= 12) {
                                InputMeses.value = PeriodoID.meses / 12;
                                Select.value = "false";
                            } else {
                                InputMeses.value = PeriodoID.meses;
                                Select.value = "true";
                            }
                            break;
                        case "FormsCrearTipoMTO":
                            const TipoMantenimientoID = data.tipoMantenimientos.find(tipoMantenimientos => tipoMantenimientos.id === parseInt(Parametro));
                            InputNombre.value = TipoMantenimientoID.nombre;
                            break;
                        case "FormsCrearEstatus":
                            const EstatusID = data.estatus.find(estatus => estatus.id === parseInt(Parametro));
                            InputNombre.value = EstatusID.nombre;
                            break;
                        case "FormsCrearTipoEquipo":
                            const TipoEquipoID = data.tipoEquipos.find(tipoEquipos => tipoEquipos.id === parseInt(Parametro));
                            InputNombre.value = TipoEquipoID.nombre;
                            break;
                        case "FormsCrearUnidadMedida":
                            const UnidadMedidadID = data.unidadMedidas.find(unidadMedidas => unidadMedidas.id === parseInt(Parametro));
                            InputNombre.value = UnidadMedidadID.nombre;
                            break;
                    }

                })
                .catch(error => console.error('GET Error:', error));
        }
    });
}

document.addEventListener('DOMContentLoaded', InicializarEditar);

