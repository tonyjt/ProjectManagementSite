$(function () {

    $(':checkbox').click(function () {
        var varFor = $(this).attr("for");

        if (varFor != "") {
            if ($(this).is(':checked')) {
                $("#" + varFor).show();
            } else {

                $("#" + varFor).hide();
            }
        }
    });

    $("#btnReset").click(reset);

    $("#btnSubmit").click(function () {
        callback("form1");
    });
});

