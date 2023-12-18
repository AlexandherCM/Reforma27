
document.addEventListener('DOMContentLoaded', () => {

    var Checkboxs = document.querySelectorAll('.chbxEstado');

    Checkboxs.forEach(function (element) {
        element.addEventListener('change', function (event) {
            var CheckboxID = event.currentTarget.getAttribute('value');
            var URL = event.currentTarget.getAttribute('data-url');
            ConsultaGET(URL, CheckboxID);
           
        });
    });

    function ConsultaGET(URL, CheckboxID) {
        const api = new ApiClient();

        api.get(URL + CheckboxID);
    }
});

