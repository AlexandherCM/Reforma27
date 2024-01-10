document.addEventListener('DOMContentLoaded', function () {



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

    let txtMantenimientoID = document.getElementById('txtMantenimientoID'); // Input del formulario que manda el ID del mto pasado

    Array.from(btnsMtos).forEach(btn => {
        btn.addEventListener('click', (event) => {
            // Alternar Botones - - - - - - - - - - - - - - - - - - - - - 
            btnUpdate.classList.add('d-none');
            btnAdd.classList.remove('d-none');
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            flagModal = true;
            let object = JSON.parse(event.currentTarget.getAttribute('data-object'));
            let leyendaMto = document.getElementById('Leyenda-Mto');
            let accion = document.getElementById('accion-forms');

            txtMantenimientoID.value = parseInt(object.MantenimientoID); // asignación del ID del mto pendiente o pasado

            if (object.Pendiente) {
                accion.innerHTML = 'Registra el último mantenimiento programado';

                leyendaMto.innerHTML = `Este equipo con número de serie <strong>"${object.NumSerieEquipo}"</strong>
                                    tiene un mantenimiento programado para: <strong>"${object.ProximaAplicacion}"</strong>`;

                //Abrir el modal
                contenedor.classList.toggle('mostrarDown');
                MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '/images/up.svg';
            } else {
                accion.innerHTML = 'Actualiza los datos de este mantenimiento pasado';

                leyendaMto.innerHTML = `Este equipo con número de serie <strong>"${object.NumSerieEquipo}"</strong>
                                    tenia un mantenimiento programado para: <strong>"${object.ProximaAplicacion}"</strong>`;

                // Alternar Botones - - - - - - - - - - - - - - - - - - - - - 
                btnUpdate.classList.remove('d-none');
                btnAdd.classList.add('d-none');
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

                //Abrir el modal
                contenedor.classList.toggle('mostrarDown');
                MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '/images/up.svg';
            }

        });
    });
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    document.getElementById('SelectEstado').addEventListener('change', (event) => {
        let forms = document.getElementById("formsEstado");
        let seletValue = event.target.value;

        if (parseInt(seletValue) != 0) {
            forms.submit();
        }
    });



});






