
document.addEventListener('DOMContentLoaded', () => {
    CheckboxListeners('.chbxPeriodo', 'UpdatePeriodo');
    CheckboxListeners('.chbxMarcas', 'UpdateMarca');
    CheckboxListeners('.chbxMotores', 'UpdateMotor');
    CheckboxListeners('.chbxUbicaciones', 'UpdateUbicacion');
    CheckboxListeners('.chbxEstatus', 'UpdateEstatus');
    CheckboxListeners('.chbxTipoMantenimientos', 'UpdateTipoMantenimientos');
    CheckboxListeners('.chbxTipoEquipo', 'UpdateTipoEquipo');
    CheckboxListeners('.chbxUnidadMedida', 'UpdateUnidadMedida');

    function CheckboxListeners(selector, prefijo) {
        var checkboxes = document.querySelectorAll(selector);

        for (var i = 0; i < checkboxes.length; i++) {
            checkboxes[i].addEventListener('change', createChangeListener(prefijo, i));
        }
    }

    function createChangeListener(prefijo, index) {
        return () => {
            var formId = `${prefijo}${index}`;
            var form = document.getElementById(formId);

            form ? form.submit() : null;
        };
    }
});