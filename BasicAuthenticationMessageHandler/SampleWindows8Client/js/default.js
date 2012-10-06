// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };

    app.start();
})();

function echo() {

    var echoStr = document.getElementById("echoStr").value;
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    
    //uri something like http://{host}/api/echo?echoStr=...
    var uri = "http://localhost:9235/api/echo?echoStr=" + encodeURIComponent(echoStr);
    
    var authValue = btoa(username + ":" + password);
    var headers = {};
    headers["Authorization"] = "Basic " + authValue;
   
    WinJS.xhr({ type: "GET", url: uri, headers: headers})
        .then(function (xhr) {
            if (xhr.status == 200 && xhr.responseText)
                document.getElementById("resultDiv").innerHTML = xhr.responseText;
            else
                document.getElementById("resultDiv").innerHTML = "ERROR";
        });

    /*
        other version, IE Engine does a first request without authorization header. In presence of the 401, 
        it's issued another request with basic authentication using the provided username and password
    */

    //WinJS.xhr({ type: "GET", url: uri, headers: headers, user: username, password: password })
    //    .then(function (xhr) {
    //        if (xhr.status == 200 && xhr.responseText)
    //            document.getElementById("resultDiv").innerHTML = xhr.responseText;
    //        else
    //            document.getElementById("resultDiv").innerHTML = "ERROR";
    //    });

}
