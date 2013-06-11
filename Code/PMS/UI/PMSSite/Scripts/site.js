$(function () {


});
function reset()
{
    window.location.href = window.location.href;
}

function callback(formId) {

    //var $inputs = $('#'+formId+' :input');

    var url = $("#" + formId).attr("action");

    // not sure if you wanted this, but I thought I'd add it.
    // get an associative array of just the values.
    var values = $("#"+formId).serializeArray();
    $("#overlay").show();

    var request = $.ajax({
        url: url,
        type: "POST",
        data: values,
        dataType: "html"
    });

    request.done(function (jsonResult) {

        $("#overlay").hide();

        var result = $.parseJSON(jsonResult);

        if (result.Success) {
            if (!result.Redirect) {
                showMessage(true, result.Message);
            }
            else {
                window.location.href = result.RedirectUrl;
            }
        }
        else {
            showMessage(false, result.Message);
        }



    });

    request.fail(function (jqXHR, textStatus) {

        $("#overlay").hide();
        showMessage(false, "请求失败:出现异常:" + textStatus);

    });
}

function showMessage(isSuccess, msg) { 
    var strCalss = isSuccess ? "alert-success" : "alert-error";

    showViewMessage(strCalss,msg);
}