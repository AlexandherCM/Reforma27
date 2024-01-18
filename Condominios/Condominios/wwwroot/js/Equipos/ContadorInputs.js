// Llama a la función al cargar la página para inicializar el número de inputs
contarInputs();

document.getElementById('SelectEstado').addEventListener('change', (event) => {
    let forms = document.getElementById("formsEstado");
    let seletValue = event.target.value;

    if (parseInt(seletValue) != 0) {
        forms.submit();
    }
});

function contarInputs() {
    const numeroDeInputs = document.getElementsByClassName("InputSerie").length;
    document.getElementById("numeroInputs").textContent = `Número de registros: ${numeroDeInputs}`;

    return numeroDeInputs;
};

function agregarNuevoInput() {
    let contenedor = document.getElementById("contenedor-inputs");

    let nuevoLabel = document.createElement("label");
    nuevoLabel.className = "label-new mb-4 mt-4 text-center control-label";
    nuevoLabel.innerHTML = "<b>Numero de serie</b>";

    // Crear un nuevo input
    let nuevoInput = document.createElement("input");
    nuevoInput.type = "text";
    nuevoInput.className = "InputSerie input-new textDes text-center";
    nuevoInput.name = "NumerosSerie";

    // Crear un nuevo span para validación
    let nuevoSpan = document.createElement("span");
    nuevoSpan.className = "text-danger";

    contenedor.appendChild(nuevoLabel);
    contenedor.appendChild(nuevoInput);
    contenedor.appendChild(nuevoSpan);
    allFields.push(nuevoInput);

    contarInputs();
}

function remover() {
    let contenedor = document.getElementById("contenedor-inputs");
    let inputs = contenedor.getElementsByClassName("input-new");
    let labels = contenedor.getElementsByClassName("label-new");

    // Verificar que hay al menos dos elementos para eliminar
    if (inputs.length >= 1) {
        // Eliminar el último elemento con la clase "InputSerie"
        contenedor.removeChild(inputs[inputs.length - 1]);
        contenedor.removeChild(labels[labels.length - 1]);

        //Eliminar el ultimo input de la lista de campos del formulario
        allFields.pop();

        contarInputs();
    } else {
        Modal('Error', 'No hay suficientes elementos para eliminar.', false)
    }
}

