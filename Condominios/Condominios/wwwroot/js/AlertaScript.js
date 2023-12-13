
function Modal(titulo, mensaje, tipo) {

    let modalActivo = null;
    var main = document.getElementById('content-main');
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
            icon.classList.add("question-icon", "mb-2", "iconos", "orange");
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
    main.appendChild(modalBody);
    modalActivo = modalBody;

}

//Funcion de la alerta con opcion
function ModalOption(titulo, mensaje) {
    const main = document.getElementById("main");

    const modalBody = document.createElement("section");
    modalBody.classList.add("modalBody");

    const iconContent = document.createElement("div");
    const icon = document.createElement("div");
    iconContent.classList.add("iconContent");
    icon.classList.add("question-icon", "mb-2", "iconos", "blue");
    icon.textContent = '?';
    iconContent.appendChild(icon);

    modalBody.appendChild(iconContent);

    const tituloModal = document.createElement("h1");
    tituloModal.innerText = titulo;
    modalBody.appendChild(tituloModal);

    const mensajeModal = document.createElement("p");
    mensajeModal.innerText = mensaje;
    modalBody.appendChild(mensajeModal);

    const row = document.createElement("div");
    row.classList.add("btnR");
    const colS = document.createElement("div");
    colS.classList.add("btnW");
    const colC = document.createElement("div");
    colC.classList.add("btnW");

    const cancel = document.createElement("button");
    cancel.textContent = "Cancelar";
    cancel.classList.add("cancel");
    colC.appendChild(cancel);

    const aceptar = document.createElement("button");
    aceptar.textContent = "Aceptar";
    aceptar.classList.add("aceptar");
    colS.appendChild(aceptar);

    row.appendChild(colS);
    row.appendChild(colC);
    modalBody.appendChild(row);

    main.appendChild(modalBody);
}

//Funcion de la alerta con tabla
function ModalTable(titulo, mensaje) {
    const main = document.getElementById("main");

    const modalBody = document.createElement("section");
    modalBody.classList.add("modalBody");

    const iconContent = document.createElement("div");
    const icon = document.createElement("div");
    iconContent.classList.add("iconContent");
    icon.classList.add("question-icon", "mb-2", "iconos", "orange");
    icon.textContent = '!';
    iconContent.appendChild(icon);

    modalBody.appendChild(iconContent);

    const tituloModal = document.createElement("h1");
    tituloModal.innerText = titulo;
    modalBody.appendChild(tituloModal);

    const mensajeModal = document.createElement("h3");
    mensajeModal.innerText = mensaje;
    modalBody.appendChild(mensajeModal);


    const contTabla = document.createElement('div');
    contTabla.classList.add("contTabla");

    var tabla = document.createElement('table');
    tabla.setAttribute('id', 'miTabla');
    tabla.classList.add("Tabla");


    var encabezado = tabla.createTHead();
    var filaEncabezado = encabezado.insertRow();
    for (var key in datos[0]) {
        var th = document.createElement('th');
        th.appendChild(document.createTextNode(key));
        filaEncabezado.appendChild(th);
    }

    var cuerpoTabla = document.createElement('tbody');
    tabla.appendChild(cuerpoTabla);

    for (var i = 0; i < datos.length; i++) {
        var fila = cuerpoTabla.insertRow(i);
        for (var key in datos[i]) {
            var celda = fila.insertCell();
            celda.appendChild(document.createTextNode(datos[i][key]));
        }
    }

    contTabla.appendChild(tabla);
    modalBody.appendChild(contTabla);

    const row = document.createElement("div");
    row.classList.add("btnR");
    const colS = document.createElement("div");
    colS.classList.add("btnW");
    const colC = document.createElement("div");
    colC.classList.add("btnW");

    const cancel = document.createElement("button");
    cancel.textContent = "Cancelar";
    cancel.classList.add("cancel");
    colC.appendChild(cancel);

    const aceptar = document.createElement("button");
    aceptar.textContent = "Aceptar";
    aceptar.classList.add("aceptar");
    colS.appendChild(aceptar);

    row.appendChild(colS);
    row.appendChild(colC);
    modalBody.appendChild(row);

    main.appendChild(modalBody);
}



var datos = [
    { id: 1, nombre: 'Juan', edad: 25 },
    { id: 2, nombre: 'María', edad: 30 },
    { id: 3, nombre: 'Pedro', edad: 22 }
];

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