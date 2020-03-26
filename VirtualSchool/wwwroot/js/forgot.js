$(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("recoverHub")
        .build();

    let Email = "";
    let Code = "";

    hub.on("CodeSent", function (recoverCode) {
        if (recoverCode == "") {
            $("#input_email").css("border-color", "orangered");
        }
        else {
            $("#user_email").val(Email);
            $("#email_form").css("display", "none");
            $("#confirm_form").css("display", "block");
            Code = recoverCode;
        }
    });

    $("#send_email").on("click", function (e) {
        e.preventDefault();
        Email = $("#input_email").val();
        hub.invoke("SendCode", Email);
    });

    $("#send_code").on("click", function (e) {
        if ($("#input_code").val() != Code) {
            $("#input_code").css("border-color", "orangered");
            e.preventDefault();
        }
    });

    hub.start();
});