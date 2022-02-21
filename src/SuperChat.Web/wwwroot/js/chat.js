"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, actualDate) {

    if ($("#messagesList li").length >= 50) {
        $("#messagesList li")[0].remove();
    }

    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${actualDate} - ${user}: ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    connection.invoke('getConnectionId')
        .then(function (connectionId) {

            var groupId = window.location.href.split('/').slice(-1)[0];

            connection.invoke("AddToGroup", groupId, connectionId).catch(function (err) {
                return console.error(err.toString());
            });

        });

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var actualDate = new Date();
    var message = document.getElementById("messageInput").value;
    var groupId = window.location.href.split('/').slice(-1)[0];

    connection.invoke("SendMessage", groupId, message, actualDate).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});