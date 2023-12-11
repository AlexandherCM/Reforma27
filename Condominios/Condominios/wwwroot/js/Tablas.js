
var tabla = document.getElementById('myTable');
var filasDatos = tabla.getElementsByTagName('tbody')[0].getElementsByTagName('tr');

var filasPorPagina = 10;
var totalFilas = filasDatos.length;
var totalPaginas = Math.ceil(totalFilas / filasPorPagina);


var paginacion = document.querySelector('.pagination');
for (var i = 1; i <= totalPaginas; i++) {
    var enlace = document.createElement('a');
    enlace.href = '#';
    enlace.textContent =i;
    enlace.onclick = function (pagina) {
        return function () {
            mostrarPagina(pagina);
        };
    }(i);
    paginacion.appendChild(enlace);
}


mostrarPagina(1);

function mostrarPagina(numeroPagina) {

    for (var i = 0; i < filasDatos.length; i++) {
        filasDatos[i].style.display = 'none';
    }


    var inicio = (numeroPagina - 1) * filasPorPagina;
    var fin = inicio + filasPorPagina;
    for (var i = inicio; i < fin && i < totalFilas; i++) {
        filasDatos[i].style.display = 'table-row';
    }


    var enlacesPaginacion = document.querySelectorAll('.pagination a');
    enlacesPaginacion.forEach(function (enlace) {
        enlace.classList.remove('active');
    });
    enlacesPaginacion[numeroPagina - 1].classList.add('active');
}