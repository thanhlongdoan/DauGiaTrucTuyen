$(document).ready(function () {
    var table = $('#example').DataTable({
        responsive: true,
        stateSave: true
    });
    new $.fn.dataTable.FixedHeader(table);
})