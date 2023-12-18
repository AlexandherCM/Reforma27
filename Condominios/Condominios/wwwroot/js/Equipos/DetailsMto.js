//let api = new ApiClient(); 
//let parametro;
//let btnConsultarMto = document.getElementById('btn-ConsultarMtos');


let btnsDetails = document.getElementsByClassName('btn-details');

Array.from(btnsDetails).forEach(btn => {
    btn.addEventListener('click', () => {
        let parametro = btn.getAttribute("data-parametro");

        window.location.href = `/Mantenimientos/Consultar/${parametro}`;
    });
});

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
//Array.from(btnsDetails).forEach(btn => {
//    btn.addEventListener('click', () => {
//        parametro = btn.getAttribute("data-parametro");

//        api.get(`Mantenimientos/GetMtoProgramado`)
//            .then(data => {
//                console.log(data);
//            })
//            .catch(error => console.error('POST Error:', error));

//        contenedor.classList.toggle('mostrarDown');
//        MostrarMas.src = contenedor.classList.contains('mostrarDown') ? '/images/down.svg' : '../../images/up.svg';
//    });
//});
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

//btnConsultarMto.addEventListener('click', () => {
//    if (parametro)
//        window.location.href = `/Mantenimientos/Consultar/${parametro}`;
//});




