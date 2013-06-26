$(function () {


    $(".plan").click(function () {
        if ($(this).text() == "显示所有计划") {
            $(this).text("隐藏所有计划");
            //$(".tr-twoRow").attr("rowspan", "2");
            $(".tr-plan").show();
            $(".hrefPlan").text("隐藏计划");
        }
        else {
            $(this).text("显示所有计划");
            //$(".tr-twoRow").attr("rowspan", "1");
            $(".tr-plan").hide();
            $(".hrefPlan").text("显示计划");
        }
    });


    $("#hrefSearch").click(function () {
        $('form').submit();
    });

    
});
function viewPlan(htmlTag) {
    if ($(htmlTag).text() == "显示计划") {
        $(htmlTag).text("隐藏计划");
        var tbody = $(htmlTag).parents("tbody");
            //tbody.find(".tr-twoRow").attr("rowspan", "2");
            tbody.find(".tr-plan").show();
        }
        else {
        $(htmlTag).text("显示计划");
        var tbody = $(htmlTag).parents("tbody");
            //tbody.find(".tr-twoRow").attr("rowspan", "1");
            tbody.find(".tr-plan").hide();
        }
   
}
function viewHistory(htmlTag) {
    var tbody = $(htmlTag).parents("tbody");

    var history = tbody.find(".hidHistory").val();

    $("#modalContent").text(history);

    $("#modalHistory").modal('toggle');
}

function roleDisable(htmlTag) {
    if (confirm("确认删除?")) {

        var url = disableUrl;

        roleAction(url, htmlTag, null, null);
    }
}

function roleEnable(htmlTag) {

    var url = enableUrl;

    roleAction(url, htmlTag, null, null);
}

function userAssign(htmlTag) {
    var url = assignUrl;

    var user = $(htmlTag).val();

    roleAction(url, htmlTag, "userId",user);
}

function userTake(htmlTag) {

    var url = takeUrl;

    roleAction(url, htmlTag, null, null);
}

function roleStart(htmlTag) {

    var url = startUrl;

    roleAction(url, htmlTag, null, null);
}
function roleFinish(htmlTag) {

    var url = finishUrl;

    roleAction(url, htmlTag, null, null);
}
function roleAction(url, htmlObject,key,value) {

    var tDiv = $(htmlObject).parents("div").children("div .span2");

    var taskId = tDiv.children(".hidTask").val();

    var role = tDiv.children(".hidRole").val();

    var values = {
        "TaskId": taskId,
        "Role" :role
    };
    if (key != null) {
        values[key] = value;
    }


    callbackToUrl(url, values, function roleActionSuccess(content) {
        if (content != "") {
            var tbody = $(tDiv).parents("tbody");
            $(tbody).html(content);
        }
        else {
            alert('出现异常');
        }
    });
}



