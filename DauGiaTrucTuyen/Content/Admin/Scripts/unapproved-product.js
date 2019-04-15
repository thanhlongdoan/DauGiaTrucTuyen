$(document).ready(function () {
    $("#example").on("click", "#js_btn-unapproved", function () {
        var id = $(this).attr('data-id');
        $.ajax(
            {
                url: "/Admin/Product/UnApproved?productId=" + id,
                type: "GET",
                success: function (data) {
                    if (data) {
                        swal({
                            title: "Hoàn tác thành công",
                            type: "success",
                            confirmButtonClass: "btn-primary",
                            confirmButtonText: "OK",
                            closeOnConfirm: false
                        }, function () {
                            location.reload();
                        });
                    }
                    else {
                        swal("Duyệt thất bại", ":(", "error");
                    }
                }
            }
        )
    })
})