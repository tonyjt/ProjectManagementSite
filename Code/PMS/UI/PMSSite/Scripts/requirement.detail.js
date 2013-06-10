$(function () {

    $("#btnSave").click(save);
    $("#btnCancel").click(
    function () {
        reset();
    });


});

function save() {

    $("#hidContent").val($('#editor').html());

    $("form").submit();
}
