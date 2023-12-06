const menuIcon = document.getElementById("menu");
const colMenu = document.getElementById("col-menu");
const content = document.getElementById('content');
const overlay = document.getElementById('overlay');
const menuLogo = document.getElementById('menuLogo');
const hamburger = document.getElementById('hamburger');
const formulario = document.getElementById('form-b');
const titles = document.getElementsByClassName('titles');

let isToggle = false;
const menuSrcOpen = "/images/bars-solid.svg";
const menuSrcClosed = "/images/x-solid.svg";
const toggle = (isOpen) => {
    const width = isOpen ? "5%" : "16%";
    const contentWidth = isOpen ? "95%" : "84%";
    const menuSrc = isOpen ? menuSrcOpen : menuSrcClosed;

    colMenu.style.width = width;
    content.style.width = contentWidth;
    menuIcon.classList.toggle("d-block", isOpen);
    menuIcon.src = menuSrc;
    overlay.style.display = isOpen && width === "5%" ? 'none' : 'block';
    hamburger.style.justifyContent = isOpen && width === "5%" ? 'center' : 'flex-end';
    colMenu.style.alignItems = isOpen && width === "5%" ? 'center' : 'start';
    formulario.style.display = isOpen && width === "5%" ? 'block' : 'none';
    menuLogo.style.visibility = isOpen && width === "5%" ? 'hidden' : 'visible';
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


