//Despelgable 1 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
let conte = document.getElementById('despegableUP');
let Mostrar = document.getElementById('up');

if (conte && Mostrar) {
    Mostrar.addEventListener('click', function () {
        conte.classList.toggle('mostrarUP');
        Mostrar.src = conte.classList.contains('mostrarUP') ? '/images/up.svg' : '/images/down.svg';
    });
}

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

let btnsDetails = document.getElementById('btn-edit');

btnsDetails.addEventListener('click', () => {
    conte.classList.toggle('mostrarUP');
    Mostrar.src = conte.classList.contains('mostrarUP') ? '/images/up.svg' : '/images/down.svg';
});

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
let btnsMtos = document.getElementsByClassName('btn-Mto');

Array.from(btnsMtos).forEach(btn => {
    btn.addEventListener('click', (event) => {
        flagModal = true;
        let object = JSON.parse(event.currentTarget.getAttribute('data-object'));

        let leyendaMto = document.getElementById('Leyenda-Mto');
        leyendaMto.innerHTML = `Este equipo con número de serie <strong>"${object.NumSerieEquipo}"</strong>
                                tiene un mantenimiento programado para: <strong>"${object.ProximaAplicacion}"</strong>`;


        console.log(object);

        contenedor.classList.toggle('mostrarDown');
        MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '/images/up.svg';
    });
});

let btnAdd = document.getElementById('btn-add');
let btnUpdate = document.getElementById('btn-update');

btnAdd.addEventListener('click', (event) => {
    var form = document.getElementById('form-mto');
    form.action = '/' + 'Mantenimientos' + '/' + 'CreateOneMto';
    form.submit();
})

btnUpdate.addEventListener('click', (event) => {
    var form = document.getElementById('form-mto');
    form.action = '/' + 'Mantenimientos' + '/' + 'UpdateOneMto';
    form.submit();
})





