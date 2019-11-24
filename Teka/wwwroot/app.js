function goWasm(mainAsmName, mainNamespace, mainClassName, mainMethodName, assemblies) {
    if (debug)
        console.log("goWasm:" + mainAsmName + "," + mainNamespace + "," + mainClassName + "," + mainMethodName + "," + assemblies);

    Module.entryPoint = { "a": mainAsmName, "n": mainNamespace, "t": mainClassName, "m": mainMethodName };
    Module.assemblies = assemblies;
    initializeNavigation();
    monitorSizeChanges(1000 / 30);
}

document.addEventListener("DOMContentLoaded", function (event) {
    let asm = get_url_parameter("assembly");
    goWasm(asm, asm, "Program", "Main");
});
var App = {
    init: function () {
        if (debug)
            console.log("App.init");
        MonoRuntime.init();
    }
};
