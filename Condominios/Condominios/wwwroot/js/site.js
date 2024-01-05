const getElement = id => document.getElementById(id);
const getElements = className => document.getElementsByClassName(className);

const menuIcon = getElement("menu");
const colMenu = getElement("col-menu");
const content = getElement('content');
const overlay = getElement('overlay');
const menuLogo = getElement('menuLogo');
const hamburger = getElement('hamburger');
const titles = getElements('titles');
const hyperLink = getElements('hyperLink');
const icons = getElements('icon-users');
const nav = getElement('navbarResponsivo');

let isToggle = false;
const menuSrcOpen = "/images/bars-solid.svg";
const menuSrcClosed = "/images/x-solid.svg";

const toggle = isOpen => {
    const width = isOpen ? "6%" : "17.5%";
    const contentWidth = isOpen ? "94%" : "82.5%";
    const menuSrc = isOpen ? menuSrcOpen : menuSrcClosed;

    colMenu.style.width = width;
    content.style.width = contentWidth;
    menuIcon.classList.toggle("d-block", isOpen);
    menuIcon.src = menuSrc;
    overlay.style.display = isOpen && width === "6%" ? 'none' : 'block';
    hamburger.style.justifyContent = isOpen && width === "6%" ? 'center' : 'flex-end';
    colMenu.style.alignItems = isOpen && width === "6%" ? 'center' : 'start';
    menuLogo.style.visibility = isOpen && width === "6%" ? 'hidden' : 'visible';
    nav.classList.remove("show", isOpen);

    Array.from(titles).forEach(element => element.style.display = isOpen ? "none" : "block");

    Array.from(hyperLink).forEach(element => {
        element.style.justifyContent = isOpen && width === "6%" ? 'center' : 'flex-start';
        element.style.paddingLeft = isOpen && width === "6%" ? '0' : '1.8rem';
    });
    Array.from(icons).forEach(element => {
        element.style.height = isOpen && width === "6%" ? '2.3rem' : '1.8rem';
    });
};

const handleClick = () => {
    toggle(isToggle);
    isToggle = !isToggle;
};

overlay.onclick = handleClick;
menuIcon.onclick = handleClick;

window.addEventListener('resize', function () {
    var nuevoAnchoPantalla = window.innerWidth;

    if (nuevoAnchoPantalla > 900) {
        nav.classList.remove("show");
    }
});
