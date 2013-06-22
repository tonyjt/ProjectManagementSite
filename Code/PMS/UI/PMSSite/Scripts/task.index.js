$(function () {

    $(".hrefPlan").click(function () {
        if ($(this).text() == "显示计划") {
            $(this).text("隐藏计划");
            var tbody = $(this).parents("tbody");
            tbody.find(".tr-twoRow").attr("rowspan", "2");
            tbody.find(".tr-plan").show();
        }
        else {
            $(this).text("显示计划");
            var tbody = $(this).parents("tbody");
            tbody.find(".tr-twoRow").attr("rowspan", "1");
            tbody.find(".tr-plan").hide();
        }
    });


    $(".plan").click(function () {
        if ($(this).text() == "显示所有计划") {
            $(this).text("隐藏所有计划");
            $(".tr-twoRow").attr("rowspan", "2");
            $(".tr-plan").show();
            $(".hrefPlan").text("隐藏计划");
        }
        else {
            $(this).text("显示所有计划");
            $(".tr-twoRow").attr("rowspan", "1");
            $(".tr-plan").hide();
            $(".hrefPlan").text("显示计划");
        }
    });


    $(".hrefHistory").click(function () {
        var tbody = $(this).parents("tbody");

        var history = tbody.find(".hidHistory").val();

        $("#modalContent").text(history);

        $("#modalHistory").modal('toggle');
    });

    $("#hrefSearch").click(function () {
        $('form').submit();
    });

    
});


function roleDisable(htmlTag) {
    if (confirm("确认删除?")) {

        var url = disableUrl;

        roleAction(url, htmlTag);
    }
}

function roleEnable(htmlTag) {

    var url = enableUrl;

    roleAction(url, htmlTag);
}

function roleAction(url, htmlObject) {

    var tDiv = $(htmlObject).parents("div");

    var taskId = tDiv.children(".hidTask").val();

    var role = tDiv.children(".hidRole").val();

    var values = {
        "TaskId": taskId,
        "Role" :role
    };

    callbackToUrl(url, values, function roleActionSuccess(content) {
        if (content != "") {
            $(tDiv).parents("td").html(content);
        }
        else {
            alert('出现异常');
        }
    });
}



