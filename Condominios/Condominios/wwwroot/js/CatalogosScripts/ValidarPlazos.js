
// Inputs
const Formulario = document.getElementById('FormsCrearPeriodo');
const Cancelar = Formulario.querySelector('[value="Cancelar"]');



const ddlUnidadTiempo = document.getElementById('ddlUnidadTiempo');
const txtCantidad = document.getElementById('txtCantidad');
const txtNombre = document.getElementById('txtNombrePlazo');


ddlUnidadTiempo.addEventListener('change', ValidarPeriodo);
txtCantidad.addEventListener('input', ValidarPeriodo);

function ValidarPeriodo() {
    //ValidarRango(120); 
    ValidarRango(20);

    if (!Cancelar.classList.contains('d-none')) {
        switch (ddlUnidadTiempo.value) {
            case 'true':
                if (UpdateCount() === 12) {
                    ddlUnidadTiempo.value = 'false';
                    txtCantidad.value = 1;
                }
                break;
        }
    } else {
        if (txtCantidad.value.trim() === '') {
            txtNombre.value = '';
            return;
        }

        switch (ddlUnidadTiempo.value) {
            case 'true':
                if (UpdateCount() < 12)
                    leyend(UpdateCount(), true);

                else if (UpdateCount() === 12) {
                    ddlUnidadTiempo.value = 'false';
                    txtCantidad.value = 1;

                    leyend(UpdateCount(), false);
                }
                else
                    ddlUnidadTiempo.value = 'false';

                break;
            case 'false':
                leyend(UpdateCount(), false);
                break;
            default:
                txtNombre.value = '';
                break;
        }
    }
}

function leyend(cantidad, estado) {
    if (estado) {
        txtNombre.value = `${cantidad} ${cantidad < 2 ? 'mes' : 'meses'}`;
    } else {
        txtNombre.value = `${cantidad} ${cantidad < 2 ? 'año' : 'años'}`;
    }
}

function ValidarRango(limit) {
    if (UpdateCount() < 1 || UpdateCount() > limit) {
        txtCantidad.value = 1;
    }
}

function UpdateCount() {
    return parseInt(txtCantidad.value);
}
