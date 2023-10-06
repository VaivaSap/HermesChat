"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SyncHub").build();

document.getElementById("sendButton").disabled = true;


/*adding an event handler to spark action when smth new happens*/

connection.on("ReceiveMessage", function (user, message, sentAt) {
    console.log(user, message);
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user}: ${message}`;
});



connection.start().then(function () {
    connection.invoke("GetConnectionId").then(function (id) {
        document.getElementById("connectionId").innerText = id;
        console.log(id);
    });
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    //var groupElement = document.getElementById("group");
    //var groupValue = groupElement.options[groupElement.selectedIndex].value;
    //var method = "SendMessage";
    console.log(connection);

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();

});


document.getElementById("sendToParticularUser").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var receiverConnectionId = document.getElementById("receiverId").value;
    var message = document.getElementById("messageInput").value;
    console.log(connection);
    connection.invoke("SendToParticularUser", user, receiverConnectionId, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();

});

document.getElementById("createGroupButton").addEventListener("click", function (event) {

    var groupName = document.getElementById("groupName").value;
    console.log(groupName);

    document.getElementById("createGroupButton").disabled = false;
    connection.invoke("CreateGroupChat", groupName)
        .then(function (event) {
            console.log(event);
            const newGroupChat = document.createElement("div");

            newGroupChat.innerText = groupName;

            document.getElementById("listOfGroupChats").appendChild(newGroupChat);
        })
        .catch(function (err) {
            return console.error(err);
        });
})

document.getElementById("joinGroupChatButton").addEventListener("click", function (event) {
    connection.invoke("JoinGroupChat", groupName).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();

});