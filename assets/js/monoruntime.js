
var MonoRuntime = {
    init: function () {
        if (debug)
            console.log("MonoRuntime::runtime init cwrapping...");

        this.load_runtime = Module.cwrap('mono_wasm_load_runtime', null, ['string', 'number']);
        this.assembly_load = Module.cwrap('mono_wasm_assembly_load', 'number', ['string']);
        this.find_class = Module.cwrap('mono_wasm_assembly_find_class', 'number', ['number', 'string', 'string']);
        this.find_method = Module.cwrap('mono_wasm_assembly_find_method', 'number', ['number', 'string', 'number']);
        this.invoke_method = Module.cwrap('mono_wasm_invoke_method', 'number', ['number', 'number', 'number']);
        this.mono_string_get_utf8 = Module.cwrap('mono_wasm_string_get_utf8', 'number', ['number']);
        this.mono_string = Module.cwrap('mono_wasm_string_from_js', 'number', ['string']);
        if (debug) {
            console.log("...cwrapping");
            console.log("Done initializing the runtime.");
        }

        WebAssemblyApp.init();
    },

    conv_string: function (mono_obj) {
        if (debug)
            console.log("MonoRuntime::conv_string");

        if (mono_obj === 0)
            return null;
        var raw = this.mono_string_get_utf8(mono_obj);
        var res = Module.UTF8ToString(raw);
        Module._free(raw);
        return res;
    },

    call_method: function (method, this_arg, args) {
        if (debug)
            console.log("MonoRuntime::call_method" + method);

        var args_mem = Module._malloc(args.length * 4);
        var eh_throw = Module._malloc(4);
        for (var i = 0; i < args.length; ++i)
            Module.setValue(args_mem + i * 4, args[i], "i32");
        Module.setValue(eh_throw, 0, "i32");

        var res = this.invoke_method(method, this_arg, args_mem, eh_throw);

        var eh_res = Module.getValue(eh_throw, "i32");

        Module._free(args_mem);
        Module._free(eh_throw);

        if (eh_res !== 0) {
            var msg = this.conv_string(res);
            throw new Error(msg);
        }

        return res;
    }
};