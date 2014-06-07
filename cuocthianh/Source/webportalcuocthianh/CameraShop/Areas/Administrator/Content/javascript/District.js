
function reloadGrid() {
    $('#tSortable').dataTable({
        "iDisplayLength": 5,
        "aLengthMenu": [5, 10, 25, 50, 100],
        "sPaginationType": "full_numbers",
        "iSortingCols": "STT"
    });
}


function Success(data) {
    if (data != "") {
        alert("Cập nhật thành công");
        $('#ID').val(data.ID);
        $('#Name').val(data.Name);
        $('#Code').val(data.Code);
        $('#ProvinceID').val(data.ProvinceID);
       
    }
    else {
        alert("Lỗi, vui lòng thử lại");
    }
}


       
