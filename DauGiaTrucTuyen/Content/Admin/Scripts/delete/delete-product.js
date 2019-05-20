$(document).ready(function () {
    $("#js_btnDelete").on('click', function () {
        var id = $(this).attr('data-id');
        swal({
            title: "Xóa sản phẩm?",
            text: "Vui lòng cân nhắc trước khi xóa sản phẩm. Bạn có chắc chắn muốn xóa sản phẩm này không?",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "Đồng ý",
            cancelButtonText: 'Không',
            confirmButtonColor: "#ec6c62",
            closeOnConfirm: false,
            closeOnCancel: true
        }, function (result) {
            if (result) {
                $.ajax(
                    {
                        url: "/Admin/Product/Delete?productId=" + id,
                        type: "GET",
                        success: function (data) {
                            if (data != "False") {
                                swal({
                                    title: "Đã xóa thành công",
                                    type: "success",
                                    confirmButtonClass: "btn-primary",
                                    confirmButtonText: "OK",
                                    closeOnConfirm: false
                                }, function () {
                                    location.reload();
                                });
                            }
                            else {
                                swal("Xóa thất bại", ":(", "error");
                            }
                        }
                    }
                )
            }
        });
    })
});