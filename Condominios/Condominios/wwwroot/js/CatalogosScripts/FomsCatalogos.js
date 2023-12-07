const api = new ApiClient();

const catalogos = {
    MarcasPropeties: {
        Forms: 'FormsCrearMarca',
        TableRow: 'FilasMarcas',
        Chbx: 'chbxMarcas',
        Entity: 'Marca'
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
                    .then(data =>
                        ActualizarTabla(data.marcas, Propeties.TableRow, Propeties.chbx, Propeties.Entity))
                    .catch(error => console.error('POST Error:', error));
                break;
        }

    });
}

function ActualizarTabla(Lista, TableID, chbx, Entity) {
    var tbody = document.getElementById(TableID);
    tbody.innerHTML = "";

    for (var i = 0; i < Lista.length; i++) {
        var objeto = Lista[i];
        var newRow = tbody.insertRow();

        // Crear la plantilla de la fila
        var rowTemplate = `
            <input type="hidden" name="ID" value="${objeto.id}" />
            <input type="hidden" name="Entidad" value="${Entity}" />

            <td class="border-R">${objeto.nombre}</td>

            <td class="border-R">
                <label class="toggle-switch">
                    <input class="${chbx}"" type="checkbox" ${objeto.estado ? 'checked' : ''}>
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
        `;
        // Establecer el contenido de las celdas con la plantilla
        newRow.innerHTML = rowTemplate;
    }
}