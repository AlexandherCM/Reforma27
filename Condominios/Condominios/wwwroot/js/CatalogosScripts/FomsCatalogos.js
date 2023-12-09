const api = new ApiClient();

const catalogos = {
    MarcasPropeties: {
        Forms: 'FormsCrearMarca',
        TableRowsID: 'FilasMarcas',

        ClassFormTable: 'FilaMarca',
        FormsRowID: 'UpdateMarca',

        Chbx: 'chbxMarcas',
        List: 'marcas',
        Entity: ''
    },
    UbicacionPropeties: {
        Forms: 'FormsCrearUbicacion',
        TableRowsID: 'FilasUbicacion',

        ClassFormTable: 'FilaUbicacion',
        FormsRowID: 'UpdateUbicacion',

        Chbx: 'chbxUbicaciones',
        List: 'ubicaciones',
        Entity: ''
    }
};

CreateFormsListener(catalogos.MarcasPropeties);
CreateFormsListener(catalogos.UbicacionPropeties);

function CreateFormsListener(Propeties) {
    document.getElementById(Propeties.Forms).addEventListener('submit', (event) => {
        event.preventDefault();

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

        //add the object entity
        Propeties.Entity = Objetos.Entidad;

        api.post('Catalogos/Create', CatalogoViewModel)
            .then(data => {
                let NameList = Propeties.List;
                let list = data[NameList];
                let newObject = (list[list.length - 1]);

                AddNewFile(newObject, Propeties)
            })
            .catch(error => console.error('POST Error:', error));
    });
}

function AddNewFile(TheObject, Propeties) {
    let tbody = document.getElementById(Propeties.TableRowsID);
    let formsCount = document.querySelectorAll(`.${Propeties.ClassFormTable}`).length;

    // Crear el formulario
    let form = document.createElement('form');
    form.id = `${Propeties.FormsRowID}${formsCount}`;
    form.className = Propeties.ClassFormTable;

    // Añadir los input al formulario
    form.innerHTML = `
        <input type="hidden" name="ID" value="${TheObject.id}" />
        <input type="hidden" name="Entidad" value="${Propeties.Entity}" />
    `;

    // Crear el checkbox
    let checkbox = document.createElement('input');
    checkbox.className = `${Propeties.Chbx}`;
    checkbox.type = 'checkbox';
    checkbox.dataset.formId = form.id;  // Asociar el ID del formulario al checkbox
    checkbox.checked = TheObject.estado;

    // Crear el background
    let background = document.createElement('div');
    background.className = "toggle-switch-background";
    background.innerHTML = `<div class="toggle-switch-handle"></div>`;

    // Crear la etiqueta del switch
    let toggleSwitchLabel = document.createElement('label');
    toggleSwitchLabel.className = 'toggle-switch';
    toggleSwitchLabel.appendChild(checkbox);
    toggleSwitchLabel.appendChild(background);

    // Crear la fila
    var row = document.createElement('tr');
    row.innerHTML = 
    `
        <td class="border-R">${TheObject.nombre}</td>
        <td class="border-R"></td> <!-- Espacio para el checkbox -->
        <td class="border-R">
            <img class="remover" src="../../images/pen-to-square-solid.svg" />
        </td>
        <td class="border-R">
            <img class="remover" src="../../images/eye-solid.svg" />
        </td>
    `;

    // Insertar el toggle-switch en el espacio del checkbox
    row.children[1].appendChild(toggleSwitchLabel);

    // Añadir el formulario y la fila al tbody
    tbody.appendChild(form);
    tbody.appendChild(row);

    // Asignar el listener al cambio del checkbox
    checkbox.addEventListener('change', () => {
        console.log(`Forms: ${form.id}`);
    });
}


