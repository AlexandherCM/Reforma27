var Password = document.getElementById("DatosUser_Password");
var ConfirmarPassword = document.getElementById("DatosUser_ConfPassword");
var PasswordError = document.getElementById("PasswordError");

var Errorr = document.getElementById("Error");


function UpdateAdmin() {
    var Enviar = false;

    if (Password.value != ConfirmarPassword.value) {
        PasswordError.style.display = 'inline';
        Enviar = true;
    }

    return !Enviar;
}

function UpdateUser() {
    var Enviar = false;

    if (Password.value != ConfirmarPassword.value) {
        Errorr.style.display = 'inline';
        Enviar = true;
    }

    return !Enviar;
}

