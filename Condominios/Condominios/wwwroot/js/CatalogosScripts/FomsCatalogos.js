const api = new ApiClient();

//CheckboxListeners('.chbxMarcas', 'UpdateMarca');
//CheckboxListeners('.chbxPeriodo', 'UpdatePeriodo');
//CheckboxListeners('.chbxMotores', 'UpdateMotor');
//CheckboxListeners('.chbxUbicaciones', 'UpdateUbicacion');
//CheckboxListeners('.chbxEstatus', 'UpdateEstatus');
//CheckboxListeners('.chbxTipoMantenimientos', 'UpdateTipoMantenimientos');
//CheckboxListeners('.chbxTipoEquipo', 'UpdateTipoEquipo');
//CheckboxListeners('.chbxUnidadMedida', 'UpdateUnidadMedida');

const catalogos = {
    MarcasPropeties: {
        Forms: 'FormsCrearMarca',
        TableRows: 'FilasMarcas',
        FormsRowID: 'UpdateMarca',
        Chbx: 'chbxMarcas',
        Entity: 'Marca',
        List: 'marcas'
    }
};

CreateFormsListener(catalogos.MarcasPropeties);

function CreateFormsListener(Propeties) {
    document.getElementById(Propeties.Forms).addEventListener('submit', (event) => {
        event.preventDefault();
        // Obtener los datos del formulario
        const formData = new FormData(event.target);

        const Objetos = {};
        formData.forEach((value, key) => {
            Objetos[key] = value;
        });

        var CatalogoViewModel = {
            CatalogoGralViewModel: {
                Nombre: Objetos.Nombre
            },
            Entidad: Objetos.Entidad
        };

        switch (Propeties.Entity) {
            case catalogos.MarcasPropeties.Entity:
                api.post('Catalogos/Create', CatalogoViewModel)
                    .then(data => {
                        let lista = Propeties.List;
                        ActualizarTabla(data[lista], Propeties)
                    })
                    .catch(error => console.error('POST Error:', error));
                break;
        }

    });
}

function ActualizarTabla(Lista, Propeties) {
    var tbody = document.getElementById(Propeties.TableRows);
    tbody.innerHTML = "";

    for (var i = 0; i < Lista.length; i++) {
        var objeto = Lista[i];
        var newRow = tbody.insertRow();

        // Crear la plantilla de la fila
        var rowTemplate = `
            <form id="${Propeties.FormsRowID}${i}" class="formsInterno">
                <input type="hidden" name="ID" value="${objeto.id}" />
                <input type="hidden" name="Entidad" value="${Propeties.Entity}" />

                <td class="border-R">${objeto.nombre}</td>

                <td class="border-R">
                    <label class="toggle-switch">
                        <input class="${Propeties.Chbx}" type="checkbox" ${objeto.estado ? 'checked' : ''}>
                        <div class="toggle-switch-background">
                            <div class="toggle-switch-handle"></div>
                        </div>
                    </label>
                </td>

                <td class="border-R">
                    <img class="remover" src="../../images/pen-to-square-solid.svg" />
                </td>
                <td class="border-R">
                    <img class="remover" src="../../images/eye-solid.svg" />
                </td>
            </form>
        `;
        // Establecer el contenido de las celdas con la plantilla
        newRow.innerHTML = rowTemplate;
    }
}

//function CheckboxListeners(chbx, prefijo) {
//    var checkboxes = document.querySelectorAll(chbx);

//    for (var i = 0; i < checkboxes.length; i++) {
//        checkboxes[i].addEventListener('change', createChangeListener(prefijo, i));
//    }
//}

//onchange = "checkboxChanged(this, 'FormID')"

function SendForm(FormID) {
    //var formId = `${prefijo}${index}`;
    //var form = document.getElementById(formId);

    //const formData = new FormData(event.target);
    const formData = new FormData(FormID);

    const Objetos = {};
    formData.forEach((value, key) => {
        Objetos[key] = value;
    });

    console.log(Objetos);
}