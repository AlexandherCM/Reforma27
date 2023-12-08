//Abrir despegable
document.addEventListener('DOMContentLoaded', () => {

    var BtnEditar = document.querySelectorAll('.remover');

    BtnEditar.forEach(function (element) {
        element.addEventListener('click', function (event) {
            var Parametro = event.currentTarget.getAttribute('data-parametro');
            var Desplegable = document.getElementById('form');
            var FlechaAbajo = document.getElementById('down');
            var FlechaArriba = document.getElementById('up');

            api.get(`TipoEquipos/ObtenerRegistro/${Parametro}`)
                .then(data => {

                    document.getElementById("VarianteEquipo_MarcaID").value = data.marcaID;
                    document.getElementById("VarianteEquipo_MotorID").value = data.motorID;
                    document.getElementById("VarianteEquipo_PeriodoID").value = data.periodoID;
                    document.getElementById("VarianteEquipo_TipoEquipoID").value = data.tipoEquipoID;
                    
                })
                .catch(error => console.error('GET Error:', error));

           
            if (!Desplegable.classList.contains('contract') && !Desplegable.classList.contains('expanded')) {
                Desplegable.classList.add('expanded');
                FlechaAbajo.classList.add('d-none');
                FlechaArriba.classList.remove('d-none');
            } else if (Desplegable.classList.contains('expanded')) {
                Desplegable.classList.remove('expanded');
                Desplegable.classList.add('contract');
                FlechaAbajo.classList.remove('d-none');
                FlechaArriba.classList.add('d-none');
            } else if (Desplegable.classList.contains('contract')) {
                Desplegable.classList.remove('contract');
                Desplegable.classList.add('expanded');
                FlechaAbajo.classList.add('d-none');
                FlechaArriba.classList.remove('d-none');
            }
        });
    });
});
