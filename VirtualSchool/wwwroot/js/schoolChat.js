$(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/schoolChatHub")
        .build();

    let userName = '';
    let groupName = '';

    $("#send").on("click", function (e) {
        let message = $("#input").val();
        if (message.replace(/\s+/g, '') == "") {
            return;
        }
        hub.invoke("Message", userName, message, groupName);
        $("#input").val('');
    });

    addEventListener("keydown", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            if (e.shiftKey) {
                $("#input").val($("#input").val() + "\r\n");
            }
            else {
                $("#send").trigger("click");
            }
        }
    });

    $('#chatlogs').scrollTop(document.getElementById("chatlogs").scrollHeight);

    hub.on("Message", function (name, message) {
        if (name == userName) {
            $("#chatlogs").append("<div class=\"chat self\"><p class=\"chat-message\"><b>" + name + "</b> " + message + "</p></div>");
        }
        else {
            $("#chatlogs").append("<div class=\"chat friend\"><p class=\"chat-message\"><b>" + name + "</b> " + message + "</p></div>");
        }

        $('#chatlogs').scrollTop(document.getElementById("chatlogs").scrollHeight);
    });

    hub.on("Enter", function (name, group) {
        userName = name;
        groupName = group;
    });

    hub.start().then(() => {
        hub.invoke("Enter");
    });
});