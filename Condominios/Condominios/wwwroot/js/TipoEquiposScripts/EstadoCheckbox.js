
document.addEventListener('DOMContentLoaded', () => {

    var Checkboxs = document.querySelectorAll('.chbxEstado');

    var Boton = document.querySelectorAll('.editarUsuario');

    Checkboxs.forEach(function (element) {
        element.addEventListener('change', function (event) {
            var CheckboxID = event.currentTarget.getAttribute('value');
            var URL = event.currentTarget.getAttribute('data-url');
            ConsultaGET(URL, CheckboxID);
            //console.log('ID: ' + CheckboxID + " URL: " + URL);
        });
    });

    Boton.forEach(function (element) {
        element.addEventListener('click', function () {
            var ID = event.currentTarget.getAttribute('value');
            var URL = event.currentTarget.getAttribute('data-url');
            ConsultaGET(URL, ID);
        })
    });



    async function ConsultaGET(URL, CheckboxID) {
        const api = new ApiClient();

        try {
            const response = await api.get(URL + CheckboxID);

            if (URL == "Perfil/Delete/") {
                window.location.href = window.location.href;
            }

            var hola = "hola";
            // Hacer algo con la respuesta si es necesario
            //console.log(response);
        } catch (error) {
            // Manejar errores
            console.error(error.message);
        }
    }
});

