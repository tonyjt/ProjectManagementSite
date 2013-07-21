$(function () {
  
});




function start(projectId) {

    var request = $.ajax({
        url: startUrl,
        type: "POST",
        data: { projectId: projectId },
    });

    request.done(function (result) {

    });
}

 