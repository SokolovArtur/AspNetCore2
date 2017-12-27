function StickyFooter() {
    let heightBrowser = $(window).height();
    let heightContainerBlock = $(document).height();

    (heightContainerBlock <= heightBrowser) ? $("footer").addClass("fixed-bottom") : $("footer").removeClass("fixed-bottom");
}

$(document).ready(function() {
    // SideNav Initialization
    $(".button-collapse").sideNav();

    // Sticky Footer
    StickyFooter();
    $(window).resize(function () {
        StickyFooter();
    });

    // Material Select
    $('.mdb-select').material_select();

    // Delite Confirm
    $("[data-confirm]").click(function() {
        let message = $(this).attr("data-confirm");
        if (!message) {
            message = "Вы действительно хотите удалить?";
        }
        if (!confirm(message)) {
            return false;
        }
    });
});
