var tabla = document.getElementById('myTable');
var filasDatos = tabla.getElementsByTagName('tbody')[0].getElementsByTagName('tr');

if (tabla) {

}
var filasPorPagina = 10; // Valor predeterminado
var totalFilas = filasDatos.length;
var totalPaginas = Math.ceil(totalFilas / filasPorPagina);

var paginacion = document.querySelector('.pagination');
for (var i = 1; i <= totalPaginas; i++) {
    var enlace = document.createElement('a');
    enlace.href = '#';
    enlace.textContent = i;
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

    if (enlacesPaginacion[numeroPagina - 1]) {
        enlacesPaginacion[numeroPagina - 1].classList.add('active');
    }
}

function cambiarFilasPorPagina() {
    var select = document.getElementById("rowsPerPage");
    filasPorPagina = parseInt(select.value);
    totalPaginas = Math.ceil(filasDatos.length / filasPorPagina);
    mostrarPagina(1);

    // Actualizar los enlaces de paginación
    actualizarEnlacesPaginacion();
}

function actualizarEnlacesPaginacion() {
    // Eliminar los enlaces de paginación existentes
    while (paginacion.firstChild) {
        paginacion.removeChild(paginacion.firstChild);
    }

    // Volver a crear los enlaces de paginación según el nuevo número de páginas
    for (var i = 1; i <= totalPaginas; i++) {
        var enlace = document.createElement('a');
        enlace.href = '#';
        enlace.textContent = i;
        enlace.onclick = function (pagina) {
            return function () {
                mostrarPagina(pagina);
            };
        }(i);
        paginacion.appendChild(enlace);
    }
}