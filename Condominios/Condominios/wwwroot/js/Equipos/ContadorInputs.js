// Llama a la función al cargar la página para inicializar el número de inputs
contarInputs();

document.getElementById('SelectEstado').addEventListener('change', (event) => {
    let forms = document.getElementById("formsEstado");
    let seletValue = event.target.value;

    if (parseInt(seletValue) != 0) {
        forms.submit();
    }
});

document.getElementById('formsEquipo').addEventListener('submit', (event) => {
    var inputsSerie = document.querySelectorAll('.InputSerie');

    // Crear un conjunto para almacenar los valores únicos de los inputs
    var uniqueValues = new Set();

    // Verificar cada input
    for (var i = 0; i < inputsSerie.length; i++) {
        var inputValue = inputsSerie[i].value.trim();

        // Verificar si el valor ya está en el conjunto
        if (uniqueValues.has(inputValue)) {
            Modal('Error', 'Los números de serie no pueden ser iguales.', false);

            event.preventDefault();
            return;
        }

        if (inputValue === "") {
            Modal('Error', 'Los campos no pueden ser nulos.', false);

            event.preventDefault();
            return;
        }

        uniqueValues.add(inputValue);
    }
});

function contarInputs() {
    const numeroDeInputs = document.getElementsByClassName("InputSerie").length;
    document.getElementById("numeroInputs").textContent = `Número de registros: ${numeroDeInputs}`;

    return numeroDeInputs;
};

let ddlVariante = document.getElementById('Plantilla_VarianteID');
let txtEstatus = document.getElementById('Plantilla_EstatusID');
let txtUltimaAplicacion = document.getElementById('Plantilla_UltimaAplicacion');
let txtUbicacion = document.getElementById('Plantilla_UbicacionID');
let txtFuncion = document.getElementById('Plantilla_Funcion');

let otherFields = [ddlVariante, txtEstatus, txtUltimaAplicacion, txtUbicacion, txtFuncion];
let inputsSerie = document.querySelectorAll('.InputSerie');

var allFields = [...otherFields, ...inputsSerie];

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

    console.log(allFields.length);
    console.log(allFields);

    contarInputs();
}

function remover() {
    var contenedor = document.getElementById("contenedor-inputs");
    var inputs = contenedor.getElementsByClassName("input-new");
    var labels = contenedor.getElementsByClassName("label-new");

    // Verificar que hay al menos dos elementos para eliminar
    if (inputs.length >= 1) {
        // Eliminar el último elemento con la clase "series-content"
        contenedor.removeChild(inputs[inputs.length - 1]);
        contenedor.removeChild(labels[labels.length - 1]);

        contarInputs();
    } else {
        Modal('Error', 'No hay suficientes elementos para eliminar.', false)
    }
}

