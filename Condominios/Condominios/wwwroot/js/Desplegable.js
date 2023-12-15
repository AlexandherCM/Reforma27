/// <reference path="tipoequiposscripts/editar.js" />
var conte = document.getElementById('despegableUP');
var Mostrar = document.getElementById('up');


if (conte && Mostrar) {
    Mostrar.addEventListener('click', function () {
        conte.classList.toggle('mostrarUP');
        Mostrar.src = conte.classList.contains('mostrarUP') ? '/images/up.svg' : '/images/down.svg';
    });
}


var contenedor = document.getElementById('despegableDown');
var MostrarMas = document.getElementById('Down');
if (contenedor && MostrarMas) {
    MostrarMas.addEventListener('click', function () {
        contenedor.classList.toggle('mostrarDown');
        MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '/images/up.svg';
    });
}
