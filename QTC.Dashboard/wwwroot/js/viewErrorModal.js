/* Display error data in a modal*/
(function ($) {
    "use strict"; // Start of use strict

    $(document).ready(function () {
        $(".btn-link").click(function () {
            /*Get data from table row*/
            var name = $(this).parents("tr").find(".application-name").text();
            var severity = $(this).parents("tr").find(".severity").text();
            var modalTitle = name + " - " + severity;
            var msg = $(this).parents("tr").find(".message").text();
            var date = $(this).parents("tr").find(".date").text();

            /*Display data*/
            $("#modal-title").text(modalTitle);
            $("#modal-title").prepend("<i class='fa fa-eye'></i> ")

            $("#modal-message").text(msg);

            $("#modal-date").text(date);
        });
    });
})(jQuery);