$(window).on('load', function () {
    var pre_loader = $('#loadpage');
    pre_loader.fadeOut('slow', function () {
        $(this).remove();
    });
});