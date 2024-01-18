var Password = document.getElementById("DatosUser_Password");

var ConfirmarPassword = document.getElementById("DatosUser_ConfPassword");
var PasswordError = document.getElementById("PasswordError");

var Errorr = document.getElementById("Error");



function UpdateAdmin() {
    let passwordOne = document.getElementById("passwordOne");
    let passwordTwo = document.getElementById("passwordTwo");

    if (passwordOne && passwordOne === '') {
        // La contraseña está vacía
        console.log('La contraseña está vacía');
    } else {
        // La contraseña no está vacía o es undefined/null
        console.log('La contraseña no está vacía');
    }

    var Enviar = false;

    if (passwordOne.value != passwordTwo.value) {
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

