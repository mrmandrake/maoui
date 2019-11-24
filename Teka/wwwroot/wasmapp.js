var WebAssemblyApp = {
    init: function () {
        if (debug)
            console.log("init");

        // this.loading = document.getElementById("loading");
        this.findMethods();
        this.runApp("1", "2");
        // this.loading.hidden = true;
    },

    runApp: function (a, b) {
        var initialSize = getSize();
        if (debug)
            console.log("runApp:" + initialSize.width + "," + initialSize.height + "...");

        try {
            MonoRuntime.call_method(this.main_method, null, []);

            if (this.maoui_StartWebAssemblySession_method)
                MonoRuntime.call_method(this.maoui_StartWebAssemblySession_method, null, [MonoRuntime.mono_string(initialSize.width), MonoRuntime.mono_string(initialSize.height)]);
        } catch (e) {
            console.error(e);
        }        

        if (debug)
            console.log("...runApp");
    },

    receiveMessagesJson: function (sessionId, json) {
        if (debug)
            console.log("receiveMessagesJson");

        if (this.maoui_ReceiveWebAssemblySessionMessageJson_method)
            MonoRuntime.call_method(this.maoui_ReceiveWebAssemblySessionMessageJson_method, null, [MonoRuntime.mono_string(json)]);
    },

    findMethods: function () {
        if (debug)
            console.log("findMethods");

        this.main_module = MonoRuntime.assembly_load(Module.entryPoint.a);
        if (!this.main_module)
            throw "Could not find Main Module " + Module.entryPoint.a + ".dll";

        this.main_class = MonoRuntime.find_class(this.main_module, Module.entryPoint.n, Module.entryPoint.t);
        if (!this.main_class)
            throw "Could not find Program class " + Module.entryPoint.n + "." + Module.entryPoint.t + " in main module";

        this.main_method = MonoRuntime.find_method(this.main_class, Module.entryPoint.m, -1);
        if (!this.main_method)
            throw "Could not find method " + Module.entryPoint.m;

        this.maoui_module = MonoRuntime.assembly_load("Maoui");
        if (this.maoui_module) {

            this.maoui_class = MonoRuntime.find_class(this.maoui_module, "Maoui", "UI");
            if (!this.maoui_class)
                throw "Could not find UI class in Ooui module";

            this.maoui_StartWebAssemblySession_method = MonoRuntime.find_method(this.maoui_class, "StartWebAssemblySession", -1);
            if (!this.maoui_StartWebAssemblySession_method)
                throw "Could not find StartWebAssemblySession method";

            this.maoui_ReceiveWebAssemblySessionMessageJson_method = MonoRuntime.find_method(this.maoui_class, "ReceiveWebAssemblySessionMessageJson", -1);
            if (!this.maoui_ReceiveWebAssemblySessionMessageJson_method)
                throw "Could not find ReceiveWebAssemblySessionMessageJson method";
        }
    }
};

