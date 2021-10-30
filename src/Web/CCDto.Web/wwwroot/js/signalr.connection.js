var signalhub = function (url, qs,fnname, entity) {
    var handler = $.connection.myHub;
    $.connection.hub.transportConnectTimeout = 5000;
    $.connection.hub.url = (url+ "/signalr");
    $.connection.hub.qs = qs;
    $.connection.hub.start({ xdomain: true });
    handler.client.SendNotify = function (json) {
        toastr[json.Type](json.Content, json.Title);
    };
    handler.client.Run = function (name) {
        try { eval(name); } catch (e) {
            console.log("handler:" + name + ";error:" + e);
        }

    };
    handler.client.OpenLink = function (json) {
        toastr.options.onclick = function () {
            if (json.Url !== null) {
                if (json.Url.length > 0 && json.Url !== "null") {
                    location.href = json.Url;
                }
            }
        };
        toastr["error"](json.Content + "-" + json.From, json.Title);
    };
    $.connection.hub.start().done(function () {
        if (fnname !== undefined) {
            var method = fnname + "(" + entity + ")";
            console.log("handler.server." + method);
            eval("handler.server." + method);
        }
    });
};