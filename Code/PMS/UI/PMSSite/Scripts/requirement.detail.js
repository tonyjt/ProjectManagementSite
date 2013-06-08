$(function () {

    $("#btnSave").click(save);
    $("#btnCancel").click(
    function () {
        window.location.href = window.location.href;
    });


});

function save() {

    $("#hidContent").val($('#editor').html());

    $("form").submit();
}
