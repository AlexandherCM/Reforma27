//Botones para remover detro del foreach
let btnsRemove = document.getElementsByClassName('btnRemove');
let chbxs = document.getElementsByClassName('ChbxReparacio');
//Tbody de la table
let rows = document.getElementById('RowsEquipos');
//Json del ViewModel que contiene a los equipos
let txtJsonEquipos = document.getElementById('txtJsonEquipos');

// Obtengo mi objeto de equipos para gestionar su mantenimiento
let equipos; // Objeto que contiene el MTO programado y el estado para aplicar o no el costo de reparación

function GetEquipos(json) {
    equipos = json
    txtJsonEquiposValue();
}
function txtJsonEquiposValue() {
    txtJsonEquipos.value = JSON.stringify(equipos);
    console.log(equipos);
}

Array.from(chbxs).forEach(chbx => {
    chbx.addEventListener('click', (event) => {
        let numSerie = event.currentTarget.getAttribute('data-row');
        let estado = chbx.checked;

        // Buscar el índice del objeto en la lista con NumSerie igual a numSerie
        let index = equipos.findIndex(equipo => equipo.numSerie === numSerie.substring(3));

        equipos[index].aplicarReparacion = estado;
        txtJsonEquiposValue();
    });
});

// Función listener para eliminar los objetos de la interfaz y de mi objeto

Array.from(btnsRemove).forEach(btn => {
    btn.addEventListener('click', (event) => {
        let numSerie = event.currentTarget.getAttribute('data-row');
        let object = JSON.parse(event.currentTarget.getAttribute('data-object'));

        eliminarEquipo(numSerie);
    });
});
function eliminarEquipo(numSerie) {
    // Encuentra el índice del equipo con el NumSerie dado
    const index = equipos.findIndex(equipo => equipo.numSerie === numSerie.substring(3));

    if (index !== -1) {
        equipos.splice(index, 1);
    } else {
        return;
    }

    // Elimina la fila del DOM
    let rowToRemove = document.getElementById(numSerie);
    if (rowToRemove) {
        rowToRemove.remove();
    }

    txtJsonEquiposValue();
}


