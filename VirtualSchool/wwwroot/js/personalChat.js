$(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/personalChatHub")
        .build();

    let userName = "";
    let userId = "";
    let toId = "";

    $("#send").on("click", function () {
        let message = $("#input").val();
        if (message.replace(/\s+/g, '') == "") {
            return;
        }
        hub.invoke("Message", toId, message, userName);
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

    hub.on("Enter", function (name, id) {
        userName = name;
        userId = id;
        toId = $("#toId").val();
    });

    hub.on("Message", function (message, name, id) {
        if (id == userId) {
            $("#chatlogs").append("<div class=\"chat self\"><p class=\"chat-message\"><b>" + name + "</b> " + message + "</p></div>");
        }
        else {
            $("#chatlogs").append("<div class=\"chat friend\"><p class=\"chat-message\"><b>" + name + "</b> " + message + "</p></div>");
        }

        $('#chatlogs').scrollTop(document.getElementById("chatlogs").scrollHeight);
    });

    $('#chatlogs').scrollTop(document.getElementById("chatlogs").scrollHeight);

    hub.start().then(() => {
        hub.invoke("Enter");
    });
});