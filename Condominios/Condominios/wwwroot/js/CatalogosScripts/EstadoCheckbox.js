
let checkboxes = document.getElementsByClassName('chbxMarcas');

for (let i = 0; i < checkboxes.length; i++) {
    CheckboxListeners(checkboxes[i], `UpdateMarca${i}`);
}

function CheckboxListeners(Chbx, forms) {
    Chbx.addEventListener('change', (event) => {
        event.preventDefault();

        console.log(`Forms: ${forms}`);
    });
};

//function SendForm(FormID) {
//    const formData = new FormData(FormID);

//    const Objetos = {};
//    formData.forEach((value, key) => {
//        Objetos[key] = value;
//    });

//    console.log(Objetos);
//}






