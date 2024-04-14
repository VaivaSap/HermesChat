"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SyncHub").build();

//document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, sentAt, groupName) {
    console.log("ReceiveMessage", user, message);
    var li = document.createElement("li");

    var groupNameActive = document.getElementById("jsResultGroupName").value; // aktyvią žinau pagal tai, kas paspausta

    if (!groupName || groupNameActive === groupName) {
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${user}: ${message}`;
    }

});



connection.start().then(function () {
    connection.invoke("GetConnectionId").then(function (id) {
        document.getElementById("connectionId").innerText = id;
        console.log(id);
    });
    
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    console.log(connection);

//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });

//    event.preventDefault();

//});


document.getElementById("sendToParticularUser").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;

    var receiver = document.getElementById("jsResultUserName").value; //ateina jsResultUserName paspaudus
    //var receiverConnectionId = document.getElementById("receiverId").value;
    var message = document.getElementById("messageInput").value; //backend ateina jsResulUserName siųsta privati žinutė
    console.log(message);

    connection.invoke("SendToParticularUser", user, receiver, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();

});

document.getElementById("createGroupButton").addEventListener("click", function (event) {

    var groupName = document.getElementById("groupName").value;
    console.log(groupName);

    if (!groupName) {
        alert("Group name cannot be empty.");
        return;
    }

    document.getElementById("createGroupButton").disabled = false;
    connection.invoke("CreateGroupChat", groupName)
        .then(function (event) {
            console.log(event);

            if (event === true) {
                const newGroupChat = document.createElement("div");
                newGroupChat.innerText = groupName;
                document.getElementById("listOfGroupChats").appendChild(newGroupChat);
            }

            else {
                alert("Already exists.");
            }
        })
        .catch(function (err) {
            return console.error(err);
        });
});



var joinedGroups = [];
document.getElementById("joinGroupChatButton").addEventListener("click", function (event) {

    var groupName = document.getElementById("groupName").value;
    console.log(groupName);

    if (!groupName) {
        alert("Group name cannot be empty.");
        return;
    }

    if (joinedGroups.includes(groupName)) {
        alert("You've already joined this group.");
        return;
    }

    joinedGroups.push(groupName);


    document.getElementById("joinGroupChatButton").disabled = false;

    connection.invoke("JoinGroupChat", groupName)
        .catch(function (err) {
            return console.error(err.toString());
        });

    event.preventDefault();

});

document.getElementById("SendMessageToGroup").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var groupName = document.getElementById("jsResultGroupName").value;
    var message = document.getElementById("messageInput").value;
    console.log("SendMessageToGroup", user, groupName, message);
    connection.invoke("SendMessageToGroup", user, groupName, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();

});

//connection.on("ReceiveMessage", function (messages) {
//    // Handle incoming messages and update the UI
//});