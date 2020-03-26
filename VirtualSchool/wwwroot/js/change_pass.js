$("#changePass").on("click", function (e) {
    var pass = $("#newPass").val();
    if ((pass != $("#verPass").val()) || (pass.replace(/\s+/g, '') == "") || (pass.length < 8)) {
        $("#newPass").css("border-color", "orangered").val("");
        $("#verPass").css("border-color", "orangered").val("");
        e.preventDefault();
    }
});