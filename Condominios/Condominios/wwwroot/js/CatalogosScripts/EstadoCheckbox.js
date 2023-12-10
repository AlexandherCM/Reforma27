
Object.keys(catalogos).forEach(key => {

    let ChbxClass = catalogos[key].Chbx;
    let checkboxes = document.getElementsByClassName(ChbxClass);

    for (let i = 0; i < checkboxes.length; i++) {
        CheckboxListeners(checkboxes[i], `${catalogos[key].FormsRowID}${i}`);
    }
});

function CheckboxListeners(Chbx, forms) {
    Chbx.addEventListener('change', (event) => {
        event.preventDefault();

        UpdateStatus(forms);
    });
};

function UpdateStatus(FormID) {
    let api = new ApiClient();

    let form = document.getElementById(FormID);
    let formData = new FormData(form);

    const Objetos = {};
    formData.forEach((value, key) => {
        Objetos[key] = value;
    });

    var CatalogoViewModel = {
        CatalogoGralViewModel: {
            Nombre: Objetos.Nombre
        },
        Entidad: Objetos.Entidad,
        ID: Objetos.ID
    };

    console.log(CatalogoViewModel);

    api.SetPost('Catalogos/UpdateById', CatalogoViewModel)
        .then(data => {
            console.log('POST Successful: Actualización de estado')
        })
        .catch(error => console.error('POST Error:', error));
}






