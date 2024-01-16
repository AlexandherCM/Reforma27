var Password = document.getElementById("DatosUser_Password");
var ConfirmarPassword = document.getElementById("DatosUser_ConfPassword");
var PasswordError = document.getElementById("PasswordError");


function UpdateAdmin() {
    var Enviar = false;

    if (Password.value != ConfirmarPassword.value) {
        PasswordError.style.display = 'inline';
        Enviar = true;
    }

    console.log(Enviar);
    return !Enviar;
}

