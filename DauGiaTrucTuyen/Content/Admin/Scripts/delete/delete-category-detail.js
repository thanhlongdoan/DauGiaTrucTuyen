$(document).ready(function () {
    $("#js_btnDelete").click(function () {
        var id = $(this).attr('data-id');
        swal({
            title: "Xóa tài khoản?",
            text: "Bạn có chắc chắn muốn xóa danh mục sản phẩm này không?",
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
                        url: "/Admin/Category/Delete?id=" + id,
                        type: "GET",
                        success: function (data) {
                            if (data) {
                                swal({
                                    title: "Đã xóa thành công",
                                    type: "success",
                                    confirmButtonClass: "btn-primary",
                                    confirmButtonText: "OK",
                                    closeOnConfirm: false
                                }, function () {
                                    location.href = "/Admin/Category/Index";
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
})