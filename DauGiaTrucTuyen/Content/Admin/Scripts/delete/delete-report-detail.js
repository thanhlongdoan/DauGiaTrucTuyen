﻿$(document).ready(function () {
    $("#js_btnDelete").click(function () {
        var id = $(this).attr('data-id');
        swal({
            title: "Xóa báo cáo người dùng?",
            text: "Bạn có chắc chắn muốn xóa báo cáo này không?",
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
                        url: "/Admin/Report/Delete?id=" + id,
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
                                    location.href = "/Admin/Report/Index";
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