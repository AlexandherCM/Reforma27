
//Despelgable 1 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
let conte = document.getElementById('despegableUP');
let Mostrar = document.getElementById('up');

if (conte && Mostrar) {
    Mostrar.addEventListener('click', function () {
        conte.classList.toggle('mostrarUP');
        Mostrar.src = conte.classList.contains('mostrarUP') ? '/images/up.svg' : '/images/down.svg';
    });
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

let btnsDetails = document.getElementById('btn-edit');

btnsDetails.addEventListener('click', () => {
    conte.classList.toggle('mostrarUP');
    Mostrar.src = conte.classList.contains('mostrarUP') ? '/images/up.svg' : '/images/down.svg';
});

//Despelgable 2 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

let flagModal = false;

let contenedor = document.getElementById('despegableDown');
let MostrarMas = document.getElementById('Down');

if (contenedor && MostrarMas) {
    MostrarMas.addEventListener('click', function () {
        if (flagModal) {
            contenedor.classList.toggle('mostrarDown');
            MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '/images/up.svg';

            flagModal = false
        }
    });
}
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
let btnsMtos = document.getElementsByClassName('btn-Mto'); // Botones de los diferentes mantenimientos

let btnAdd = document.getElementById('btn-add'); // Botón para confirmar el mto pendiente 
let btnUpdate = document.getElementById('btn-update'); // Bóton para editar un mantenimiento pasado

// Inputs del formulario Update / Create
let txtMantenimientoID = document.getElementById('txtMantenimientoID'); // Input del formulario que manda el ID del mto pasado
let ddlTipoMto = document.getElementById('ddlTipoMto');
let ddlProveedor = document.getElementById('ddlProveedor');
let txtCostoMantenimiento = document.getElementById('txtCostoMantenimiento');
let txtCostoReparacion = document.getElementById('txtCostoReparacion');
let txtObservaciones = document.getElementById('txtObservaciones');
let txtFechaAplicacion = document.getElementById('txtFechaAplicacion');

Array.from(btnsMtos).forEach(btn => {
    btn.addEventListener('click', (event) => {
        // Alternar Botones - - - - - - - - - - - - - - - - - - - - - 
        btnAdd.classList.remove('d-none');
        btnUpdate.classList.add('d-none');
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        flagModal = true;
        let object = JSON.parse(event.currentTarget.getAttribute('data-object'));
        let leyendaMto = document.getElementById('Leyenda-Mto');
        let leyendaAccion = document.getElementById('accion-forms');

        txtMantenimientoID.value = parseInt(object.MantenimientoID); // asignación del ID del mto pendiente o pasado

        if (object.Pendiente) {
            txtInit(); // Igualar campos
            PerformanceModal(); //Abrir el modal
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            leyendaAccion.innerHTML = 'Registra el último mantenimiento programado';

            leyendaMto.innerHTML = `Este equipo con número de serie <strong>"${object.NumSerieEquipo}"</strong>
                                    tiene un mantenimiento programado para: <strong>"${object.ProximaAplicacion}"</strong>`;
        } else {
            txtInit(object); // Igualar campos
            PerformanceModal(); //Abrir el modal

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            leyendaAccion.innerHTML = 'Actualiza los datos de este mantenimiento pasado';

            leyendaMto.innerHTML = `Este equipo con número de serie <strong>"${object.NumSerieEquipo}"</strong>
                                    tenia un mantenimiento programado para: <strong>"${object.ProximaAplicacion}"</strong>`;
            // Alternar Botones - - - - - - - - - - - - - - - - - - - - - 
            btnAdd.classList.add('d-none');
            btnUpdate.classList.remove('d-none');

        }

    });
});

function txtInit(object) {
    if (object) {
        ddlTipoMto.value = parseInt(object.TipoMantenimientoID) === 0 ? '' : parseInt(object.TipoMantenimientoID);
        ddlProveedor.value = parseInt(object.ProveedorID) === 0 ? '' : parseInt(object.ProveedorID);
        txtCostoMantenimiento.value = parseFloat(object.CostoMantenimiento.substring(1).replace(',', ''));
        txtCostoReparacion.value = parseFloat(object.CostoReparacion.substring(1).replace(',', ''));
        txtObservaciones.value = object.Observaciones === '-' ? "" : object.Observaciones;

        var fecha = new Date(object.DiaDeAplicacionEpoch * 1000);
        var fechaFormateada = fecha.toISOString().split('T')[0];
        txtFechaAplicacion.value = object.DiaDeAplicacionEpoch != 0 ? fechaFormateada : "";
    } else {
        ddlTipoMto.value = "";
        ddlProveedor.value = "";
        txtCostoMantenimiento.value = "";
        txtCostoReparacion.value = "";
        txtObservaciones.value = "";
        txtFechaAplicacion.value = "";
    }
}

function PerformanceModal() {
    contenedor.classList.toggle('mostrarDown');
    MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '/images/up.svg';
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

document.getElementById('SelectEstado').addEventListener('change', (event) => {
    let forms = document.getElementById("formsEstado");
    let seletValue = event.target.value;

    if (parseInt(seletValue) != 0) {
        forms.submit();
    }
});

// Acciones del formulario de equipo - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

let ddlEstado = document.getElementById('Plantilla_EstatusID');
let retomarMtos = document.getElementById('container-retomarMtos');
let leyenda = UpdateddlEstado();
let OutService = 'Fuera de servicio';
function UpdateddlEstado() {
    let i = ddlEstado.selectedIndex;
    return ddlEstado.options[i].text;;
}

ddlEstado.addEventListener('change', () => {
    if (leyenda === OutService && UpdateddlEstado() !== OutService) {
        retomarMtos.classList.remove('d-none');
    } else { retomarMtos.classList.add('d-none'); }
});








