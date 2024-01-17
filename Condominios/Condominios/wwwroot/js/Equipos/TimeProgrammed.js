const api = new ApiClient();

console.log(allFields.length);
console.log(allFields);

document.getElementById('formsEquipo').addEventListener('submit', (event) => {
    event.preventDefault();

    let form = document.getElementById('formsEquipo');
    let date = txtUltimaAplicacion.value;
    let id = ddlVariante.value;

    if (ValidateFields()) {
        api.SendGet(`Equipos/CacularPeriodos/?ultimaAplicacion=${date}&varianteID=${parseInt(id)}`)
            .then(data => {
                ModalOption('¿Esta seguro de esta acción?', data);
            })
            .catch(error => console.error('GET Error:', error));
    }
});

function ValidateFields() {
    for (let i = 0; i < inputsSerie.length; i++) {
        let inputValue = allFields[i].value.trim();

        if (inputValue === '') 
            return false;
    }

    return true;
}


