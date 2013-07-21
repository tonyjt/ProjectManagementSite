$(function () {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    $('#calendar').fullCalendar({
        // put your options and callbacks here
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        editable: true,
        firstDay: 1,
        defaultView: 'agendaWeek',
        selectable: true,
        selectHelper: true,
        select: function (start, end, allDay) {

            if (start.getTime() >= Date.now()) {

                if (allDay) {

                    $("#inputAllDay").val("true");

                    $("#txtStart").val(getStandardDateString(start));
                    $("#txtEnd").val(getStandardDateString(end));
                }
                else {
                    $("#inputAllDay").val("false");

                    $("#txtStart").val(getStandardDateTimeString(start));
                    $("#txtEnd").val(getStandardDateTimeString(end));

                }
                $("#modalNew").modal('show');
            }
        },
        events: [
                    {
                        title: 'All Day Event',
                        start: new Date(y, m, 1),
                        className: 'label'
                    },
                    {
                        title: 'Long Event',
                        start: new Date(y, m, d-5),
                        end: new Date(y, m, d - 2),
                        className:''
                    },
                    {
                        id: 999,
                        title: 'Repeating Event',
                        start: new Date(y, m, d-3, 16, 0),
                        allDay: false,
                        className:'label-success'
                    },
                    {
                        id: 999,
                        title: 'Repeating Event',
                        start: new Date(y, m, d+4, 16, 0),
                        allDay: false
                    },
                    {
                        title: 'Meeting',
                        start: new Date(y, m, d, 10, 30),
                        allDay: false
                    },
                    {
                        title: 'Lunch',
                        start: new Date(y, m, d, 12, 0),
                        end: new Date(y, m, d, 14, 0),
                        allDay: false
                    },
                    {
                        title: 'Birthday Party',
                        start: new Date(y, m, d+1, 19, 0),
                        end: new Date(y, m, d+1, 22, 30),
                        allDay: false
                    },
                    {
                        title: 'Click for Google',
                        start: new Date(y, m, 28),
                        end: new Date(y, m, 29),
                        url: 'http://google.com/'
                    }
            ]
    })

    $("#selectRole").change(function () {
 
        //$(".task-" + value);
        roleSelect();
    });
    if (needLoadSelect) {
        roleSelect();
    }
    $("#btnNew").click(newPlan);
});

function roleSelect() {
    var value = $("#selectRole").val();

    $("option[class^='task-']").hide().prop("selected", false);

    $(".task-" + value).show().first().prop("selected", true);
}

function newPlan() {

    //var tDiv = $(htmlObject).parents("div").children("div .span2");


    var taskId = $("#selectTask").val();

    var title = $("#inputTitle").val();

    var allDay = $("#inputAllDay").val();

    var startTime = $("#txtStart").val();

    var endTime = $("#txtEnd").val();



    var values = {
        "TaskParticipatorId": taskId,
        "Title": title,
        "AllDay": allDay,
        "StartTime": startTime,
        "EndTime": endTime
    };
    

    callbackToUrl(newUrl, values,null, function funSuccess() {
        $("#modalNew").modal('hide');
    });
}