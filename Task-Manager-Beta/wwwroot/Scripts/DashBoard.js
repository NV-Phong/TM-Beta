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
//    });
//});

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
        droppedTask.find(".status-name").text(newStatusName);
        console.log(droppedTask.find(".status-name").text);

        // Move task to new column
        newColumn.find(".droppp").append(droppedTask);

        // Clean up
        $(this).removeClass("dragover");
        var taskId = droppedTask.data("task-id");
        droppedTask.removeClass("dragging");

        // Send newStatusName to the controller
        //var taskId = droppedTask.data("task-id");
        updateTaskStatus(taskId, newStatusName);
    });

    function updateTaskStatus(taskId, newStatusName) {
        fetch("/Projects/UpdateTaskStatus/" + taskId, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newStatusName)
        })
            .then(response => {
                if (response.ok) {
                    console.log("Task status updated successfully!");
                } else {
                    console.error("Failed to update task status.");
                }
            })
            .catch(error => {
                console.error("An error occurred while updating task status:", error);
            });
    }
});