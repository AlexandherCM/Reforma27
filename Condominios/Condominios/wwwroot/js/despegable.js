//Despegable 1
var up = document.getElementById("up");
var down = document.getElementById("down");
var form = document.getElementById("form");

up.onclick = function () {
    form.classList.remove('expanded');
    form.classList.add('contract');
    down.classList.remove('d-none');
    up.classList.add('d-none');
}

down.onclick = function () {

    form.classList.add('expanded');
    form.classList.remove('contract');
    up.classList.remove('d-none');
    down.classList.add('d-none');
}


//Despegable 2
var up1 = document.getElementById("up1");
var down1 = document.getElementById("down1");
var body = document.getElementById("fBody");

up1.onclick = function () {
    body.classList.remove('contract');
    body.classList.add('expand');
    down1.classList.remove('d-none');
    up1.classList.add('d-none');
}
down1.onclick = function () {
    body.classList.remove('expand');
    body.classList.add('contract');
    down1.classList.add('d-none');
    up1.classList.remove('d-none');
}


