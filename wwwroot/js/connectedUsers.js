
"use strict";

var connectionUsersCount = new signalR.HubConnectionBuilder().withUrl("/ConnectedUsersHub").build();

//document.getElementById("sendButton").disabled = true;


connectionUsersCount.start().then(function () {
    connectionUsersCount.invoke("GetOnlineUsersConnectionId").then(function (usersOnlineConnectionId) {
        document.getElementById("usersOnlineConnectionId").innerHTML = usersOnlineConnectionId;
    });

    connectionUsersCount.invoke("GetUsersGroupChatList").then(function (usersGroupChats) {

        for (let chat of usersGroupChats) {

            let div = document.createElement("div");
            document.getElementById("listOfGroupChats").appendChild(div);
            div.textContent = `${chat}`;
        }

        console.log(listOfGroupChats);

    });

    document.getElementById("sendButton").disabled = false;
    document.getElementById("sendButton").disabled = false;

    function newWindowLoadedOnClient() {
        connectionUsersCount.send("NewWindowLoaded");
    }

    function fulfilled() {
        console.log("Connection was successful");
        newWindowLoadedOnClient();
    }
})
    .catch(function (err) {
        return console.error(err.toString());
    });





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




