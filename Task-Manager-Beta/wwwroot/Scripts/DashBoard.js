$(document).ready(function () {
    $(".task").on("dragstart", function (event) {
        $(this).addClass("dragging");
    });

    $(".project-column").on("dragover", function (event) {
        event.preventDefault();
        $(this).addClass("dragover");
    });

    $(".project-column").on("dragleave", function (event) {
        $(this).removeClass("dragover");
    });

    $(".project-column").on("drop", function (event) {
        event.preventDefault();
        var droppedTask = $(".dragging");
        var newColumn = $(this);
        var newStatusName = newColumn.find(".project-column-heading__title").text();

        // Update task status name
        droppedTask.find(".task__stats span").text(newStatusName);

        // Move task to new column
        newColumn.find(".droppp").append(droppedTask);

        // Clean up
        $(this).removeClass("dragover");
        droppedTask.removeClass("dragging");
    });
});

//$(document).ready(function () {
//    $(".task").on("dragstart", function (event) {
//        $(this).addClass("dragging");
//    });

//    $(".project-column").on("dragover", function (event) {
//        event.preventDefault();
//        $(this).addClass("dragover");
//    });

//    $(".project-column").on("dragleave", function (event) {
//        $(this).removeClass("dragover");
//    });

//    $(".project-column").on("drop", function (event) {
//        event.preventDefault();
//        var droppedTask = $(".dragging");
//        var newColumn = $(this);
//        var newStatusName = newColumn.find(".project-column-heading__title").text();

//        // Update task status name
//        droppedTask.find(".task__stats span").text(newStatusName);

//        // Move task to new column
//        newColumn.find(".droppp").append(droppedTask);

//        // Clean up
//        $(this).removeClass("dragover");
//        droppedTask.removeClass("dragging");

//        // Fetch to update task status in database
//        var taskId = droppedTask.attr("data-task-id");
//        fetch('/Projects/UpdateTaskStatus', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify({ taskId: taskId, statusName: newStatusName })
//        })
//            .then(response => response.json())
//            .then(data => console.log(data))
//            .catch(error => console.error('Error:', error));
//    });
//});
