const api = new ApiClient();

let ddlVariante = document.getElementById('Plantilla_VarianteID');
let txtEstatus = document.getElementById('Plantilla_EstatusID');
let txtUltimaAplicacion = document.getElementById('Plantilla_UltimaAplicacion');
let txtUbicacion = document.getElementById('Plantilla_UbicacionID');
let txtFuncion = document.getElementById('Plantilla_Funcion');

let otherFields = [ddlVariante, txtEstatus, txtUltimaAplicacion, txtUbicacion, txtFuncion];
let inputsSerie = document.querySelectorAll('.InputSerie');

let allFields = [...otherFields, ...inputsSerie];

document.getElementById('formsEquipo').addEventListener('submit', (event) => {
    event.preventDefault();

    if (modalAlert !== null)
        modalAlert.remove();

    let date = txtUltimaAplicacion.value;
    let id = ddlVariante.value;

    if (ValidateFields() && ValidateSeries()) {
        let parameters = `?ultimaAplicacion=${date}&varianteID=${id}&inputs=${inputsSerie.length}`;
        api.SendGet(`Equipos/CacularPeriodos/${parameters}`)
            .then(leyenda => {

                let pluralSingular = inputsSerie.length > 1
                    ? `${inputsSerie.length} equipos`
                    : "un equipo";

                let numEquipos =
                    `Se realizará el registro de ${pluralSingular} con las caracteristicas seleccionadas previamente.
                    `;

                let finalMessage =
                    `${numEquipos} 
                     ${leyenda}`;

                ModalOption('¿Esta seguro de esta acción?', `${finalMessage}`);

                document.getElementById('btn-send').addEventListener('click', () => {
                    // Se manda el formulario si el usuario confirma la acción
                    if (ValidateFields() && ValidateSeries())
                        event.target.submit();
                });
            })
            .catch(error => console.error('GET Error:', error));
    }
});

function ValidateFields() {
    for (let i = 0; i < allFields.length; i++) {
        let inputValue = allFields[i].value.trim();

        if (inputValue === '') {
            Modal('Error', '¡Los campos no pueden ser nulos!', false);
            return false;
        }
    }
    return true;
}

function ValidateSeries() {
    // Conjunto para almacenar los valores únicos de los inputs
    let uniqueValues = new Set();
    inputsSerie = document.querySelectorAll('.InputSerie');

    // Verificar cada input
    for (let i = 0; i < inputsSerie.length; i++) {
        let inputValue = inputsSerie[i].value.trim();

        // Verificar si el valor ya está en el conjunto
        if (uniqueValues.has(inputValue)) {
            Modal('Error', '¡Los números de serie no pueden ser iguales!', false);
            return false;
        }
        uniqueValues.add(inputValue);
    }
    return true;
}



