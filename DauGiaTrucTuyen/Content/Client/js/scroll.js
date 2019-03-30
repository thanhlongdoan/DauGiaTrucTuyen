$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.header').css("border-bottom", "4px solid #faad3b");
        } else {
            $('.header').css("border-bottom", "4px solid #18a083");
        }
    });

})