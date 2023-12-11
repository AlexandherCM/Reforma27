const menuIcon = document.getElementById("menu");
const colMenu = document.getElementById("col-menu");
const content = document.getElementById('content');
const overlay = document.getElementById('overlay');
const menuLogo = document.getElementById('menuLogo');
const hamburger = document.getElementById('hamburger');
//Este querry de clase no se encuentra, realmente sirve o se te ólvido, ya que no esta en el layaout
//const formulario = document.getElementById('form-b');

const titles = document.getElementsByClassName('titles');

let isToggle = false;
const menuSrcOpen = "/images/bars-solid.svg";
const menuSrcClosed = "/images/x-solid.svg";
const toggle = (isOpen) => {
    const width = isOpen ? "6%" : "16%";
    const contentWidth = isOpen ? "94%" : "84%";
    const menuSrc = isOpen ? menuSrcOpen : menuSrcClosed;

    colMenu.style.width = width;
    content.style.width = contentWidth;
    menuIcon.classList.toggle("d-block", isOpen);
    menuIcon.src = menuSrc;
    overlay.style.display = isOpen && width === "6%" ? 'none' : 'block';
    hamburger.style.justifyContent = isOpen && width === "6%" ? 'center' : 'flex-end';
    colMenu.style.alignItems = isOpen && width === "6%" ? 'center' : 'start';
    //Lo comente ya que parece que no tiene ninguna función
    //formulario.style.display = isOpen && width === "6%" ? 'block' : 'none';
    menuLogo.style.visibility = isOpen && width === "6%" ? 'hidden' : 'visible';
    for (let i = 0; i < titles.length; i++) {
        titles[i].style.display = isOpen ? "none" : "block";
    }
};


overlay.onclick = () => {
    toggle(isToggle);
    isToggle = !isToggle;
}
menuIcon.onclick = () => {
    toggle(isToggle);
    isToggle = !isToggle;
};


