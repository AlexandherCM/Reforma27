// Llama a la función al cargar la página para inicializar el número de inputs
contarInputs();

function contarInputs() {
    const numeroDeInputs = document.getElementsByClassName("InputSerie").length;
    document.getElementById("numeroInputs").textContent = `Número de Inputs: ${numeroDeInputs}`;
};

function agregarNuevoInput() {
    var contenedor = document.getElementById("contenedor-inputs");

    var nuevoLabel = document.createElement("label");
    nuevoLabel.className = "mb-3 control-label";
    nuevoLabel.innerHTML = "<b>Numero de serie</b>";

    // Crear un nuevo input
    var nuevoInput = document.createElement("input");
    nuevoInput.type = "text";
    nuevoInput.className = "InputSerie text-center text mb-3";
    nuevoInput.name = "NumerosSerie";
    nuevoInput.setAttribute("asp-for", "NumerosSerie");

    var nuevoSpan = document.createElement("span");

    nuevoSpan.id = "NumerosSerie-error";
    nuevoSpan.classList.add("text-danger");

    contenedor.appendChild(nuevoLabel);
    contenedor.appendChild(nuevoInput);
    contenedor.appendChild(nuevoSpan);

    contarInputs();
}