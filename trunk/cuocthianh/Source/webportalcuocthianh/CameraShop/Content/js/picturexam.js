﻿
function reloadGrid() {
    $('#tSortable').dataTable({
        "iDisplayLength": 5,
        "aLengthMenu": [5, 10, 25, 50, 100],
        "sPaginationType": "full_numbers",
        "iSortingCols": "STT"
    });
}


function Success(data) {
    if (data == "exist") {
        //alert("Tên danh mục bị trùng");
    }
    else {
        if (data != "") {
            alert("Cập nhật thành công");

        }
        else {
            alert("Lỗi, vui lòng thử lại");
        }
    }
}
$(document).ready(function () {
    $(".picture-fancy").colorbox({ rel: 'picture-fancy' });
    $(".picture-samll").colorbox({ rel: 'picture-samll' });
});
       
