$(document).ready(function () {
    $("#example").on("click", "#js_btnResponsed", function () {
        var id = $(this).attr('data-id');
        $.ajax(
            {
                url: "/Admin/Report/Responed?id=" + id,
                type: "GET",
                success: function (data) {
                    if (data) {
                        swal({
                            title: "Xác nhận đã phản hồi thành công",
                            type: "success",
                            confirmButtonClass: "btn-primary",
                            confirmButtonText: "OK",
                            closeOnConfirm: false
                        }, function () {
                            location.reload();
                        });
                    }
                    else {
                        swal("Xác nhận thất bại", ":(", "error");
                    }
                }
            }
        )
    })
})