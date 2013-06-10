$(function () {

    $(':checkbox').click(function () {
        var varFor = $(this).attr("for");

        if ($(this).is(':checked')) {

            $("#" + varFor).show();
        } else {

            $("#" + varFor).hide();
        }
    });

    $("#btnReset").click(reset);

});

