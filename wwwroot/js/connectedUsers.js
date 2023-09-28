
"use strict";

var connectionUsersCount = new signalR.HubConnectionBuilder().withUrl("/ConnectedUsersHub").build();

//document.getElementById("sendButton").disabled = true;


/*adding an event handler to spark action when smth new happens*/

connectionUsersCount.on("OnlineUsersCount", (value) => {
    console.log("successful", value);
    var newCountSpan = document.getElementById("usersCount");
    newCountSpan.innerText = value.toString();
});


connectionUsersCount.on("OnlineUsersList", (value) => {
    document.getElementById("connectedUserId").innerHTML = value;

});
function newWindowLoadedOnClient() {
    connectionUsersCount.send("NewWindowLoaded");
}

function fulfilled() {
    console.log("Connection was successful");
    newWindowLoadedOnClient();
}

function rejected() {
}




connectionUsersCount.start().then(function () {
    connectionUsersCount.invoke("GetOnlineUsersConnectionId").then(function (usersOnlineConnectionId) {
        document.getElementById("usersOnlineConnectionId").innerHTML = usersOnlineConnectionId;
    });
    document.getElementById("sendButton").disabled = false;

    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});





