function get_url_parameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}

document.addEventListener("DOMContentLoaded", function (event) {
    let asm = get_url_parameter("assembly");
    oouiWasm(asm, asm, "Program", "Main");
});
var App = {
    init: function () {
    MonoRuntime.init();
}
};
