$(".input-form input#reg").on('click', function (e) {
    window.location.href = 'reg';
    e.preventDefault();
});

$('#repPass').change(function () {
    var pass = $("#pass").val();
    var pass_rep = $("#repPass").val();

    if (pass != pass_rep) {
        $("input#pass").attr('style', 'border: 1px solid red;');
        $("input#repPass").attr('style', 'border: 1px solid red;');
    } else {
        $("input#pass").attr('style', 'border: 1px solid #7E8D8E;');
        $("input#repPass").attr('style', 'border: 1px solid #7E8D8E;');
    }
});

$('#pass').change(function () {
    var pass = $("#pass").val();
    var pass_rep = $("#repPass").val();

    if (pass != pass_rep) {
        $("input#pass").attr('style', 'border: 1px solid red;');
        $("input#repPass").attr('style', 'border: 1px solid red;');
    } else {
        $("input#pass").attr('style', 'border: 1px solid #7E8D8E;');
        $("input#repPass").attr('style', 'border: 1px solid #7E8D8E;');
    }
});

$("#regis").on("click", function (e) {
    var pass = $("#pass").val();
    var pass_rep = $("#repPass").val();
    if (pass != pass_rep) {
        e.preventDefault();
    }
});

