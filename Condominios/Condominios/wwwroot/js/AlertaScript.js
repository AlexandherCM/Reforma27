//Funcion de la alerta nromal
function AlertaJS(alertaEstado) {

    if (alertaEstado && alertaEstado.Estado) {
        Modal('¡Éxito!', alertaEstado.Leyenda, true);
    } else if (alertaEstado && !alertaEstado.Estado) {
        Modal('¡Error!', alertaEstado.Leyenda, false);
    } 
}

let modalActivo = null;
var main = document.getElementById('content-main');

function Modal(titulo, mensaje, tipo) {

    modalActivo ? modalActivo.remove() : null;

    const modalBody = document.createElement("section");
    modalBody.classList.add("modalBody", tipo);

    const iconContent = document.createElement("div");
    const icon = document.createElement("div");
    iconContent.classList.add("iconContent");
    iconContent.appendChild(icon);
    switch (tipo) {
        case true:
            icon.classList.add("check-icon", "mb-2", "iconos");
            break;
        case false:
            icon.classList.add("close", "mb-2", "iconos");
            break;
        default:
            icon.classList.add("exclamation-icon", "mb-2", "iconos", "orange");
            icon.textContent = '!';
            break;
    }
    modalBody.appendChild(iconContent);

    const tituloModal = document.createElement("h1");
    tituloModal.innerText = titulo;
    modalBody.appendChild(tituloModal);

    const mensajeModal = document.createElement("p");
    mensajeModal.innerText = mensaje;
    modalBody.appendChild(mensajeModal);

    const btnClose = document.createElement("button");
    btnClose.innerText = " Ok";
    btnClose.classList.add("btnClose");
    btnClose.addEventListener("click", () => {
        modalBody.remove();
    })
    modalBody.appendChild(btnClose);

    // document.body.appendChild(modalBody);
    main.appendChild(modalBody);


    modalActivo = modalBody;

}

const elementos = document.querySelector('.iconos');
let animacionPausada = false;



function pausar() {
    if (!animacionPausada) {
        elementos.forEach((elemento) => {
            elemento.style.animationPlayState = 'paused';
        });
        animacionPausada = true;
        setTimeout(() => {
            reanudar();
        }, 1000);
    }
}

function reanudar() {
    elementos.forEach((elemento) => {
        elemento.style.animationPlayState = 'running';
    });
    animacionPausada = false;

    setTimeout(() => {
        pausar();
    }, 6000);
}

function reiniciar() {
    elementos.forEach((elemento) => {
        elemento.style.animation = 'none';
        void elemento.offsetWidth; // Forzar un reflow
        elemento.style.animation = null;
    });
}


