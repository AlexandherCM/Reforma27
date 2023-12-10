﻿const api = new ApiClient();

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
    },
    MotorPropeties: {
        Forms: 'FormsCrearMotor',
        TableRowsID: 'FilasMotor',

        ClassFormTable: 'FilaMotor',
        FormsRowID: 'UpdateMotor',

        Chbx: 'chbxMotores',
        List: 'motores',
        Entity: ''
    },
    PeriodoPropeties: {
        Forms: 'FormsCrearPeriodo',
        TableRowsID: 'FilasPeriodo',

        ClassFormTable: 'FilaPeriodo',
        FormsRowID: 'UpdatePeriodo',

        Chbx: 'chbxPeriodo',
        List: 'periodos',
        Entity: ''
    },
    TipoMTOPropeties: {
        Forms: 'FormsCrearTipoMTO',
        TableRowsID: 'FilasTipoMTO',

        ClassFormTable: 'FilaTipoMTO',
        FormsRowID: 'UpdateTipoMTO',

        Chbx: 'chbxTipoMTO',
        List: 'tipoMantenimientos',
        Entity: ''
    },
    EstatusPropeties: {
        Forms: 'FormsCrearEstatus',
        TableRowsID: 'FilasEstatus',

        ClassFormTable: 'FilaEstatus',
        FormsRowID: 'UpdateEstatus',

        Chbx: 'chbxEstatus',
        List: 'estatus',
        Entity: ''
    },
    TipoEquipoPropeties: {
        Forms: 'FormsCrearTipoEquipo',
        TableRowsID: 'FilasTiposEquipo',

        ClassFormTable: 'FilaTipoEquipo',
        FormsRowID: 'UpdateTiposEquipo',

        Chbx: 'chbxTipoEquipo',
        List: 'tipoEquipos',
        Entity: ''
    },
    UnidadMedidaPropeties: {
        Forms: 'FormsCrearUnidadMedida',
        TableRowsID: 'FilasUnidadMedida',

        ClassFormTable: 'FilaUnidadMedida',
        FormsRowID: 'UpdateUnidadMedida',

        Chbx: 'chbxUnidadMedida',
        List: 'unidadMedidas',
        Entity: ''
    }
};

Object.keys(catalogos).forEach(key => {
    const catalogo = catalogos[key];

    CreateFormsListener(catalogo);
});

function CreateFormsListener(Propeties) {
    document.getElementById(Propeties.Forms).addEventListener('submit', (event) => {

        event.preventDefault();

        let ViewModel = {};
        const Objetos = {};
        const formData = new FormData(event.target);


        formData.forEach((value, key) => {
            Objetos[key] = value;
        });

        if (Objetos.Entidad === 'Periodo') {
            ViewModel = {
                PeriodoViewModel: {
                    Nombre: Objetos.Nombre,
                    Cantidad: parseInt(Objetos.Cantidad),
                    Mes: Objetos.Mes.toLowerCase() === 'true' 
                },
                Entidad: Objetos.Entidad
            };
        }
        else {
            ViewModel = {
                CatalogoGralViewModel: {
                    Nombre: Objetos.Nombre
                },
                Entidad: Objetos.Entidad
            };
        }

        //add the object entity
        Propeties.Entity = Objetos.Entidad;

        api.SendPost('Catalogos/Create', ViewModel)
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

    if (Propeties.Entity === 'Periodo') {
        row.innerHTML =
            `
        <td class="border-R">${TheObject.nombre}</td>
        <td class="border-R">${TheObject.meses}</td>
        <td class="border-R"></td> <!-- Espacio para el checkbox -->
        <td class="border-R">
            <img class="remover" src="../../images/pen-to-square-solid.svg" />
        </td>
        <td class="border-R">
            <img class="remover" src="../../images/eye-solid.svg" />
        </td>
    `;

        // Insertar el toggle-switch en el espacio del checkbox
        row.children[2].appendChild(toggleSwitchLabel);

    } else {
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
    }


    // Añadir el formulario y la fila al tbody
    tbody.appendChild(form);
    tbody.appendChild(row);

    // Asignar el listener al cambio del checkbox
    checkbox.addEventListener('change', () => {
        //console.log(`Forms: ${form.id}`);
        UpdateStatus(form.id);
    });
}


