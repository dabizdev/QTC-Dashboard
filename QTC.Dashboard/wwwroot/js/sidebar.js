$(document).ready(function () {
    const url = window.location.href;
    const lob = url.split("=")[1].split("&")[0];
    const integrationPoint = url.split("=")[2];

    $('.collapse-item').each(function (index) {
        let integrationPointLink = $( this );
        let applicationLink = $(this).parent().parent().parent().find("span");

        if (integrationPointLink.text() == integrationPoint && applicationLink.text() == lob) {
            applicationLink.parent().parent().addClass("active");
            integrationPointLink.parent().parent().addClass("show");
            integrationPointLink.addClass("active")
        }

    });
});
