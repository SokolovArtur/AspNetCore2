// Write your JavaScript code.
$(document).ready(function() {
    // SideNav Initialization
    $(".button-collapse").sideNav();

    // Delite Confirm
    $("[data-confirm]").click(function() {
        let message = $(this).attr("data-confirm");
        if (!message) {
            message = "Вы действительно хотите удалить?";
        }
        confirm(message);
    });

    // Select
    $('.mdb-select').material_select();
});
