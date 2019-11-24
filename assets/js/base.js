var debug = true;

const nodes = {};
const hasText = {};

const mouseEvents = {
    click: true,
    dblclick: true,
    mousedown: true,
    mouseenter: true,
    mouseleave: true,
    mousemove: true,
    mouseout: true,
    mouseover: true,
    mouseup: true,
    wheel: true
};

const inputEvents = {
    input: true,
    change: true,
    keyup: true
};

const elementEvents = {
    load: true
};

let socket = null;
let wasmSession = null;

function getSize() {
    return {
        height: window.innerHeight,
        width: window.innerWidth
    };
}

function setCookie(name, value, days) {

    if (debug)
        console.log("setCookie");

    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function saveSize(s) {
    if (debug)
        console.log("saveSize");

    setCookie("maouiWindowWidth", s.width, 7);
    setCookie("maouiWindowHeight", s.height, 7);
}

function get_url_parameter(name) {
    if (debug)
        console.log("geturlparameter:" + name);

    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}