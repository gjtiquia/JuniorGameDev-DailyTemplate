var APIHandler = {
    CallAPI: function(cmd, json) {
        console.log("Jslib cmd : " + Pointer_stringify(cmd));
        console.log("Jslib json : " + Pointer_stringify(json));
        ReceiveFromGame(Pointer_stringify(cmd), Pointer_stringify(json));
    }
}

mergeInto(LibraryManager.library, APIHandler);