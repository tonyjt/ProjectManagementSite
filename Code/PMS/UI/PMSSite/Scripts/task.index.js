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
        }
        else {
            $(this).text("显示所有计划");
            $(".tr-twoRow").attr("rowspan", "1");
            $(".tr-plan").hide();
        }
    });
});