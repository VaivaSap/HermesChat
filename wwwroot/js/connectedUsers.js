
"use strict";

var connectionUsersCount = new signalR.HubConnectionBuilder().withUrl("/ConnectedUsersHub").build();

//document.getElementById("sendButton").disabled = true;
connectionUsersCount.on("UserConnected", (user) => {
    document.getElementById("userInput").value = user;

});

connectionUsersCount.start().then(function () {
    connectionUsersCount.invoke("GetOnlineUsersConnectionId").then(function (usersOnlineConnectionId) {
        document.getElementById("usersOnlineConnectionId").innerHTML = usersOnlineConnectionId;
    });





    connectionUsersCount.invoke("GetUsersGroupChatList").then(function (usersGroupChats) {


        for (let chat of usersGroupChats) {

            let div = document.createElement("div");
            
            div.textContent = chat;
            div.addEventListener("click", function (event) {
                let groupName = this.textContent;
                selectGroupChat(groupName);
                groupName.classList.add("activeChatBold"); //kodėl neveikia?


                let user = document.getElementById("userInput").value;;


                connectionUsersCount.invoke("AddClickerToGroup", groupName, user).catch(function (err) {
                    return console.error(err.toString());
                });

                console.log("AddClickerToGroup", user, groupName);

                event.preventDefault();
            });
            document.getElementById("listOfGroupChats").appendChild(div);
            div.textContent = `${chat}`;

            

            console.log(listOfGroupChats);
        };
    });

    connectionUsersCount.invoke("GetAllActiveChats").then(function (allActiveChats) {
        for (let activeChat of allActiveChats) {
            let div = document.createElement("div");
            div.textContent = activeChat;

            document.getElementById("listOfGroupChats").appendChild(div);
            div.textContent = `${activeChat}`;

            div.classList.add("hoverEffect");
            div.addEventListener("click", function () {

                let groupName = this.textContent;
                selectGroupChat(groupName);
            });
        }



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

function selectGroupChat(groupName) {

    document.getElementById("jsResultGroupName").value = groupName;
    console.log(`Selected group chat: ${groupName}`);

}

connectionUsersCount.on("OnlineUsersCount", (value) => {

    var newCountSpan = document.getElementById("usersCount");
    newCountSpan.innerText = value.toString();
});



connectionUsersCount.on("OnlineUsersList", (connectedUsers) => {
    const userListElement = document.getElementById("connectedUserId");

    userListElement.innerHTML = "";

    for (let userOnline of connectedUsers) {
        let li = document.createElement("li");
        li.textContent = userOnline;
        userListElement.appendChild(li);
        console.log(userOnline);

        li.classList.add("hoverEffect");

        li.addEventListener("click", function () {

            let userName = this.textContent;
            selectUserToChat(userName);
        });
    }

    function selectUserToChat(userName) {

        document.getElementById("jsResultUserName").value = userName;
        console.log(`Selected user: ${userName}`);

    }
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




