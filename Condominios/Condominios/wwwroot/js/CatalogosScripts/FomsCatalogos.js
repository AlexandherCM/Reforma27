const api = new ApiClient();
import { InicializarEditar } from './Editar.js';

Object.keys(catalogos).forEach(key => {
    const catalogo = catalogos[key];

    CreateFormsListener(catalogo);
});

function CreateFormsListener(Propeties) {
    document.getElementById(Propeties.Forms).addEventListener('submit', (event) => {
        event.preventDefault();

        var BotonPresionado = event.submitter;
        let BotonValor = BotonPresionado.value;
        var Formulario = document.getElementById(Propeties.Forms);
        var ValorID = Formulario.querySelector('[name="InputHidden"]');
        var InputName = Formulario.querySelector('[name="Nombre"]');
        
        
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
                Entidad: Objetos.Entidad,
                ID: parseInt(ValorID.value)
            };
        }

        //add the object entity
        Propeties.Entity = Objetos.Entidad;

        if (BotonValor == "Agregar") {
            api.SendPost(`Catalogos/Create`, ViewModel)
                .then(data => {
                    let NameList = Propeties.List;
                    let list = data[NameList];
                    let newObject = (list[list.length - 1]);

                    AddNewFile(newObject, Propeties);
                    InputName.value = "";
                })
                .catch(error => console.error('POST Error:', error));
        } else if (BotonValor == "Actualizar") {
            api.SendPost(`Catalogos/Update`, ViewModel)
                .then(data => {
                    CambiarNombre(Propeties.TableRowsID, data.id, data.catalogoGralViewModel.nombre);
                })
            InputName.value = "";
            
        }
    });
}

function CambiarNombre(Tabla, ValorID, Nombre) {

    var tbody = document.getElementById(Tabla);

    var filas = tbody.getElementsByTagName('tr');

    for (var i = 0; i < filas.length; i++) {
        var NombreCell = filas[i].children[0];

        var removerImg = filas[i].querySelector('.remover');
        var ParametroValor = removerImg.dataset.parametro;

        if (ParametroValor == ValorID) {
            NombreCell.textContent = Nombre;
        }
    }



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

    row.innerHTML = `
    <td class="border-R">${TheObject.nombre}</td>

    ${Propeties.Entity === 'Periodo'
            ? '<td class="border-R">' + TheObject.meses + '</td>' : ''
        }
    <td class="border-R"></td> <!-- Espacio para el checkbox -->
    <td class="border-R">
        <img class="remover" src="../../images/pen-to-square-solid.svg" data-formulario="${Propeties.Forms}" data-parametro="${TheObject.id}"/>
    </td>

    ${Propeties.Entity === 'Marca' ?
        '<td class="border-R" >' +
            '<img class="remover" src="../../images/eye-solid.svg" />' +
        '</td >' : ''
        }
    `;

    // Ajustar el índice para insertar el toggle-switch
    const toggleSwitchIndex = Propeties.Entity === 'Periodo' ? 2 : 1;
    row.children[toggleSwitchIndex].appendChild(toggleSwitchLabel);

    // Añadir el formulario y la fila al tbody
    tbody.appendChild(form);
    tbody.appendChild(row);

    // Asignar el listener al cambio del checkbox
    checkbox.addEventListener('change', () => {
        //console.log(`Forms: ${form.id}`);
        UpdateStatus(form.id);
    });
    
    InicializarEditar();

}



