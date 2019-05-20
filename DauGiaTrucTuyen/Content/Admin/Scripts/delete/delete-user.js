$(document).ready(function () {
    $("#example").on("click", "#js_btnDelete", function () {
        var id = $(this).attr('data-id');
        swal({
            title: "Xóa tài khoản?",
            text: "Vui lòng cân nhắc trước khi xóa tài khoản này.Vì tài khoản này có mối quan hệ với các dữ liệu khác?",
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
                        url: "/Admin/User/Delete?id=" + id,
                        type: "GET",
                        success: function (data) {
                            console.log(data)
                            if (data != "False") {
                                swal({
                                    title: "Đã xóa thành công",
                                    type: "success",
                                    confirmButtonClass: "btn-primary",
                                    confirmButtonText: "OK",
                                    closeOnConfirm: false
                                }, function () {
                                    location.href = "/Admin/User/Index";
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