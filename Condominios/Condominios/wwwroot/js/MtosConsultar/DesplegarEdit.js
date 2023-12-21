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
let btnsMtos = document.getElementsByClassName('btn-Mto');

Array.from(btnsMtos).forEach(btn => {
    btn.addEventListener('click', (event) => {
        flagModal = true;
        let object = JSON.parse(event.currentTarget.getAttribute('data-object'));
        let leyendaMto = document.getElementById('Leyenda-Mto');

        console.log(object);

        if (object.Pendiente) {
            leyendaMto.innerHTML = `Este equipo con número de serie <strong>"${object.NumSerieEquipo}"</strong>
                                    tiene un mantenimiento programado para: <strong>"${object.ProximaAplicacion}"</strong>`;

            contenedor.classList.toggle('mostrarDown');
            MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '/images/up.svg';
        } else {
            Modal("Hola", "Mantenimientos realizado", true);
        }

    });
});
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

//let btnAdd = document.getElementById('btn-add');
//let btnUpdate = document.getElementById('btn-update');
//let formsMto = document.getElementById('form-mto');

//btnAdd.addEventListener('click', (event) => {
//    formsMto.action = '/' + 'Mantenimientos' + '/' + 'CreateOneMto';
//    formsMto.submit();
//});

//btnUpdate.addEventListener('click', (event) => {
//    formsMto.action = '/' + 'Mantenimientos' + '/' + 'UpdateOneMto';
//    formsMto.submit();
//});

document.getElementById('SelectEstado').addEventListener('change', (event) => {
    let forms = document.getElementById("formsEstado");
    let seletValue = event.target.value;

    if (parseInt(seletValue) != 0) {
        forms.submit();
    }
});





