document.addEventListener('DOMContentLoaded', () => {

    var checkboxPeriodo = document.querySelectorAll('.chbxPeriodo');
    var checkboxMarca = document.querySelectorAll('.chbxMarcas');
    var checkboxMotor = document.querySelectorAll('.chbxMotores');
    var checkboxUbicacion = document.querySelectorAll('.chbxUbicaciones');
    var checkboxEstatus = document.querySelectorAll('.chbxEstatus');
    var checkboxTipoMant = document.querySelectorAll('.chbxTipoMantenimientos');


    for (var i = 0; i < checkboxPeriodo.length; i++) {
        checkboxPeriodo[i].addEventListener('change', createChangeListener('UpdatePeriodo', i));
        /* console.log(`UpdatePeriodo${i}`);*/
    }

    for (var i = 0; i < checkboxMarca.length; i++) {
        checkboxMarca[i].addEventListener('change', createChangeListener('UpdateMarca', i));
    }

    for (var i = 0; i < checkboxMotor.length; i++) {
        checkboxMotor[i].addEventListener('change', createChangeListener('UpdateMotor', i));
    }

    for (var i = 0; i < checkboxUbicacion.length; i++) {
        checkboxUbicacion[i].addEventListener('change', createChangeListener('UpdateUbicacion', i));
    }
    for (var i = 0; i < checkboxEstatus.length; i++) {
        checkboxEstatus[i].addEventListener('change', createChangeListener('UpdateEstatus', i));
    }

    for (var i = 0; i < checkboxTipoMant.length; i++) {
        checkboxTipoMant[i].addEventListener('change', createChangeListener('UpdateTipoMantenimientos', i));
    }

    function createChangeListener(Prefijo, index) {
        return () => {
            var formId = `${Prefijo}${index}`;
            var form = document.getElementById(formId);

            if (form) {
                form.submit();
            }
        };
    }
});