
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/syncHub").build();

document.getElementById("sendButton").disabled = true;


/*adding an event handler to spark action when smth new happens*/

connection.on("ReceiveMessage", function (user, message, sentAt) {
    console.log(user, message);
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user}: ${message}`;
});


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    console.log(connection);
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();

});