
document.addEventListener('DOMContentLoaded', () => {

    var Checkboxs = document.querySelectorAll('.chbxEstado');

    Checkboxs.forEach(function (element) {
        element.addEventListener('change', function (event) {
            var CheckboxID = event.currentTarget.getAttribute('value');
            var URL = event.currentTarget.getAttribute('data-url');
            ConsultaGET(URL, CheckboxID);
            //console.log('ID: ' + CheckboxID + " URL: " + URL);
        });
    });

    async function ConsultaGET(URL, CheckboxID) {
        const api = new ApiClient();

        try {
            const response = await api.get(URL + CheckboxID);
            // Hacer algo con la respuesta si es necesario
            //console.log(response);
        } catch (error) {
            // Manejar errores
            console.error(error.message);
        }
    }
});

