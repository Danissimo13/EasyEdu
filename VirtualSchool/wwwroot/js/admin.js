(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("adminHub")
        .build();

    $("#requestBtn").on("click", function (e) {
        $("#newsTab").css("display", "none");
        $("#timetableTab").css("display", "none");
        $("#requestTab").css("display", "flex");
    });

    $("#timetableBtn").on("click", function (e) {
        $("#requestTab").css("display", "none");
        $("#newsTab").css("display", "none");
        $("#timetableTab").css("display", "block");
    });

    $("#newsBtn").on("click", function (e) {
        $("#requestTab").css("display", "none");
        $("#timetableTab").css("display", "none");
        $("#newsTab").css("display", "block");
    });

    $(".request_accept").on("click", function (e) {
        let userId = $(this).attr('userId');
        hub.invoke("Accept", userId);
    });

    $(".request_deny").on("click", function (e) {
        let userId = $(this).attr('userId');
        hub.invoke("Deny", userId);
    });

    $("#news_add").on("click", function (e) {
        let head = $("#news_header").val();
        let body = $("#news_body").val();
        console.log(body);

        hub.invoke("AddNews", head, body);
    });

    hub.on("Delete", function (userId) {
        $("div[userId=" + userId + "]").css("display", "none");
    });

    hub.on("AddNews", function (info) {
        $("#news_add_info").text(info);
    });

    hub.start();
})()