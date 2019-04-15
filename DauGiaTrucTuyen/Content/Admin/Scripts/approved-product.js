$(document).ready(function () {
    $("#example").on("click", "#js_btn-approved", function () {
        var id = $(this).attr('data-id');
        $.ajax(
            {
                url: "/Admin/Product/Approved?id=" + id,
                type: "GET",
                success: function (data) {
                    if (data) {
                        swal({
                            title: "Đã duyệt thành công",
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