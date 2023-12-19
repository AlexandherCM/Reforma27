let btnsDetails = document.getElementById('btn-edit');

btnsDetails.addEventListener('click', () => {
    conte.classList.toggle('mostrarUP');
    Mostrar.src = conte.classList.contains('mostrarUP') ? '/images/up.svg' : '/images/down.svg';
});