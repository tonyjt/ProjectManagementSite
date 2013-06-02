$(function () {

    $("#btnSave").click(save);
});

function save() {

    $("#hidContent").val($('#editor').html());

    $("form").submit();
}