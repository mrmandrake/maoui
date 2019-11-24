
function send(json) {
    if (debug)
        console.log("Send", json);

    WebAssemblyApp.receiveMessagesJson(wasmSession, json);
}

function initializeNavigation() {
    if (debug)
        console.log("initializeNavigation");
    monitorHashChanged();
    const em = {
        m: "event",
        id: "window",
        k: "hashchange",
        v: window.location
    };
    saveSize(em.v);
    const ems = JSON.stringify(em);
    send(ems);
    if (debug)
        console.log("Event", em);
}

function monitorHashChanged() {
    if (debug)
        console.log("monitorHashChanged");

    function hashChangeHandler() {
        const em = {
            m: "event",
            id: "window",
            k: "hashchange",
            v: window.location
        };
        saveSize(em.v);
        const ems = JSON.stringify(em);
        send(ems);
        if (debug)
            console.log("Event", em);
    }

    window.addEventListener("hashchange", hashChangeHandler, false);
}

function monitorSizeChanges(millis) {
    var resizeTimeout;
    function resizeThrottler() {
        if (!resizeTimeout)
            resizeTimeout = setTimeout(function () {
                resizeTimeout = null;
                resizeHandler();
            }, millis);
    }

    function resizeHandler() {
        const em = {
            m: "event",
            id: "window",
            k: "resize",
            v: getSize()
        };
        saveSize(em.v);
        const ems = JSON.stringify(em);
        send(ems);
        if (debug)
            console.log("Event", em);
    }

    window.addEventListener("resize", resizeThrottler, false);
}

function getNode(id) {
    if (debug)
        console.log("getNode:" + id);

    switch (id) {
        case "window": return window;
        case "document": return document;
        case "document.body":
            const bodyNode = document.getElementById("ooui-body");
            return bodyNode || document.body;
        default: return nodes[id];
    }
}

function getOrCreateElement(id, tagName) {
    if (debug)
        console.log("getOrCreateElement");

    var e = document.getElementById(id);
    if (e) {
        if (e.firstChild && e.firstChild.nodeType === Node.TEXT_NODE)
            hasText[e.id] = true;
        return e;
    }
    return document.createElement(tagName);
}

function msgCreate(m) {
    if (debug)
        console.log("msgcreate");

    const id = m.id;
    const tagName = m.k;
    const node = tagName === "#text" ? document.createTextNode("") : getOrCreateElement(id, tagName);
    if (tagName !== "#text")
        node.id = id;
    nodes[id] = node;
    if (debug)
        console.log("Created node", node);
}

function msgSet(m) {
    if (debug)
        console.log("msgset");

    const id = m.id;
    const node = getNode(id);
    if (!node) {
        console.error("Unknown node id", m);
        return;
    }
    const parts = m.k.split(".");
    let o = node;
    for (let i = 0; i < parts.length - 1; i++)
        o = o[parts[i]];

    const lastPart = parts[parts.length - 1];
    const value = lastPart === "htmlFor" ? m.v.id : m.v;
    o[lastPart] = value;
    if (debug)
        console.log("Set", node, parts, value);
}

function msgSetAttr(m) {
    if (debug)
        console.log("msgsetattr");

    const id = m.id;
    const node = getNode(id);
    if (!node) {
        console.error("Unknown node id", m);
        return;
    }
    node.setAttribute(m.k, m.v);
    if (debug)
        console.log("SetAttr", node, m.k, m.v);
}

function msgRemAttr(m) {
    if (debug)
        console.log("msgremattr");

    const id = m.id;
    const node = getNode(id);
    if (!node) {
        console.error("Unknown node id", m);
        return;
    }
    node.removeAttribute(m.k);
    if (debug)
        console.log("RemAttr", node, m.k);
}

function getCallerProperty(target, accessorStr) {
    if (debug)
        console.log("getcallerprop");

    const arr = accessorStr.split('.');
    var caller = target;
    var property = target;
    arr.forEach(function (v) {
        caller = property;
        property = caller[v];
    });
    return [caller, property];
}

function msgCall(m) {
    if (debug)
        console.log("msgcall");

    const id = m.id;
    const node = getNode(id);
    if (!node) {
        console.error("Unknown node id", m);
        return;
    }
    const target = node;
    if (m.k === "insertBefore" && m.v[0].nodeType === Node.TEXT_NODE && m.v[1] === null && hasText[id]) {
        // Text is already set so it clear it first
        if (target.firstChild)
            target.removeChild(target.firstChild);
        delete hasText[id];
    }

    const f = getCallerProperty(target, m.k);
    if (debug)
        console.log("Call", node, f, m.v);

    const r = f[1].apply(f[0], m.v);
    if (typeof m.rid === 'string' || m.rid instanceof String)
        nodes[m.rid] = r;
}

function msgListen(m) {
    if (debug)
        console.log("msglisten");

    const node = getNode(m.id);
    if (!node) {
        console.error("Unknown node id", m);
        return;
    }
    if (debug)
        console.log("Listen", node, m.k);
    node.addEventListener(m.k, function (e) {
        const em = {
            m: "event",
            id: m.id,
            k: m.k
        };
        if (inputEvents[m.k]) {
            em.v = (node.tagName === "INPUT" && node.type === "checkbox") ?
                node.checked :
                node.value;
        }
        else if (mouseEvents[m.k]) {
            em.v = {
                offsetX: e.offsetX,
                offsetY: e.offsetY
            };
        }
        else if (elementEvents[m.k]) {
            em.v = {
                clientHeight: node.clientHeight,
                clientWidth: node.clientWidth
            };
        }
        const ems = JSON.stringify(em);
        send(ems);
        if (debug) console.log("Event", em);
        if (em.k === "submit")
            e.preventDefault();
    });
}

function processMessage(m) {
    if (debug)
        console.log("processmessage");

    switch (m.m) {
        case "nop":
            break;
        case "create":
            msgCreate(m);
            break;
        case "set":
            msgSet(m);
            break;
        case "setAttr":
            msgSetAttr(m);
            break;
        case "remAttr":
            msgRemAttr(m);
            break;
        case "call":
            msgCall(m);
            break;
        case "listen":
            msgListen(m);
            break;
        default:
            console.error("Unknown message type", m.m, m);
    }
}

function fixupValue(v) {
    var x, n;
    if (Array.isArray(v)) {
        for (x in v) {
            v[x] = fixupValue(v[x]);
        }
        return v;
    }
    else if (typeof v === 'string' || v instanceof String) {
        if ((v.length > 1) && (v[0] === "\u2999")) {
            // console.log("V", v);
            return getNode(v);
        }
    }
    else if (!!v && v.hasOwnProperty("id") && v.hasOwnProperty("k")) {
        return fixupValue(v["id"])[v["k"]];
    }
    return v;
}

// == WASM Support ==
window["__maouiReceiveMessages"] = function (sessionId, messages) {
    if (debug)
        console.log("WebAssembly Receive", messages);

    messages.forEach(function (m) {
        if (debug)
            console.log('Raw value from server', m.v);

        m.v = fixupValue(m.v);
        processMessage(m);
    });
};
