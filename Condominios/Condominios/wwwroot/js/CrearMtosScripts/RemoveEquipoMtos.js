//Botones para remover detro del foreach
let btnsRemove = document.getElementsByClassName('btnRemove');
let chbxs = document.getElementsByClassName('ChbxReparacion');
//Tbody de la table
let rows = document.getElementById('RowsEquipos');
//Json del ViewModel que contiene a los equipos
let txtJsonEquipos = document.getElementById('txtJsonEquipos');

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//Textos del total de deducciones de gastos
let h3TotalMto = document.getElementById('h3TotalMto');
let h3TotalRep = document.getElementById('h3TotalRep');

//Inputs para el calculo de posibles gastos
let txtTotalMto = document.getElementById('Mantenimiento_CostoMantenimiento');
let txtTotalRep = document.getElementById('Mantenimiento_CostoReparacion');
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

// Obtengo mi objeto de equipos para gestionar su mantenimiento
let equipos; // Objeto que contiene el MTO programado y el estado para aplicar o no el costo de reparación

// Función para obtener el objeto de mi página de razor
function GetEquipos(json) {
    equipos = json
    txtJsonEquiposValue();
}

//Serialización de mi objeto en el input txtJsonEquipos
function txtJsonEquiposValue() {
    txtJsonEquipos.value = JSON.stringify(equipos);
    //console.log(equipos);
}

// Función para asignar true o false el costo de reparación
Array.from(chbxs).forEach(chbx => {
    chbx.addEventListener('click', (event) => {
        let numSerie = event.currentTarget.getAttribute('data-row');
        let estado = chbx.checked;

        // Buscar el índice del objeto en la lista con NumSerie igual a numSerie
        let index = equipos.findIndex(equipo => equipo.numSerie === numSerie.substring(3));

        equipos[index].aplicarReparacion = estado;
        txtJsonEquiposValue();
        updateGtos()
    });
});

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Función listener para eliminar los objetos de la interfaz y de mi objeto
Array.from(btnsRemove).forEach(btn => {
    btn.addEventListener('click', (event) => {
        let numSerie = event.currentTarget.getAttribute('data-row');
        eliminarEquipo(numSerie);
    });
});
// - - - - - - - - - - - - - - - - - - - - - -
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
    updateGtos()
}
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

txtTotalMto.addEventListener('input', updateGtos);
txtTotalRep.addEventListener('input', updateGtos);

// Actualiza los numeros de Gtos de mantenimiento y reparación
function updateGtos() {
    let NumRepDisabled = 0;
    let TotalObjects = equipos.length;

    Object.keys(equipos).forEach(key => {

        if (equipos[key].aplicarReparacion)
            NumRepDisabled++
    });

    h3TotalMto.innerText = formatCurrency(UpdateTXTsGtos().TotalMtoGtos * TotalObjects);
    h3TotalRep.innerText = formatCurrency(UpdateTXTsGtos().TotalRepGtos * NumRepDisabled);
}

//Get txt´s
function UpdateTXTsGtos() {
    let gtos = {
        TotalMtoGtos : parseFloat(txtTotalMto.value) || 0,
        TotalRepGtos : parseFloat(txtTotalRep.value) || 0
    }
    return gtos;
}

function formatCurrency(amount) {
    const formattedAmount = amount.toLocaleString('es-MX', {
        style: 'currency',
        currency: 'MXN',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });

    return formattedAmount;
}


//function formatCurrency(amount) {
//    const formattedAmount = amount.toFixed(2);

//    // Formato de pesos
//    return `$ ${formattedAmount}`;
//}







































////Botones para remover detro del foreach
//let btnsRemove = document.getElementsByClassName('btnRemove');
//let chbxs = document.getElementsByClassName('ChbxReparacion');
////Tbody de la table
//let rows = document.getElementById('RowsEquipos');
////Json del ViewModel que contiene a los equipos
//let txtJsonEquipos = document.getElementById('txtJsonEquipos');

//// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
////Textos del total de deducciones de gastos
//let h3TotalMto = document.getElementById('h3TotalMto');
//let h3TotalRep = document.getElementById('h3TotalRep');

////Inputs para el calculo de posibles gastos
//let txtTotalMto = document.getElementById('Mantenimiento_CostoMantenimiento');
//let txtTotalRep = document.getElementById('Mantenimiento_CostoReparacion');
//// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

//// Obtengo mi objeto de equipos para gestionar su mantenimiento
//let equipos; // Objeto que contiene el MTO programado y el estado para aplicar o no el costo de reparación

//function GetEquipos(json) {
//    equipos = json
//    txtJsonEquiposValue();
//}
//function txtJsonEquiposValue() {
//    txtJsonEquipos.value = JSON.stringify(equipos);
//    console.log(equipos);
//}

//Array.from(chbxs).forEach(chbx => {
//    chbx.addEventListener('click', (event) => {
//        let numSerie = event.currentTarget.getAttribute('data-row');
//        let estado = chbx.checked;

//        // Buscar el índice del objeto en la lista con NumSerie igual a numSerie
//        let index = equipos.findIndex(equipo => equipo.numSerie === numSerie.substring(3));

//        equipos[index].aplicarReparacion = estado;
//        txtJsonEquiposValue();
//    });
//});

//// Función listener para eliminar los objetos de la interfaz y de mi objeto

//Array.from(btnsRemove).forEach(btn => {
//    btn.addEventListener('click', (event) => {
//        let numSerie = event.currentTarget.getAttribute('data-row');
//        let object = JSON.parse(event.currentTarget.getAttribute('data-object'));

//        eliminarEquipo(numSerie);
//    });
//});
//function eliminarEquipo(numSerie) {
//    // Encuentra el índice del equipo con el NumSerie dado
//    const index = equipos.findIndex(equipo => equipo.numSerie === numSerie.substring(3));

//    if (index !== -1) {
//        equipos.splice(index, 1);
//    } else {
//        return;
//    }

//    // Elimina la fila del DOM
//    let rowToRemove = document.getElementById(numSerie);
//    if (rowToRemove) {
//        rowToRemove.remove();
//    }

//    txtJsonEquiposValue();
//    h3TotalMto.innerText = formatCurrency(UpdateCount() * equipos.length);
//}

//txtTotalMto.addEventListener('input', (event) => {
//    h3TotalMto.innerText = formatCurrency(UpdateCount() * equipos.length);
//});

//function UpdateCount() {
//    return parseFloat(txtTotalMto.value) || 0; // Convertir a número decimal
//}
//function formatCurrency(amount) {
//    const formattedAmount = amount.toFixed(2); // Redondear a dos decimales
//    return `$ ${formattedAmount}`;
//}


