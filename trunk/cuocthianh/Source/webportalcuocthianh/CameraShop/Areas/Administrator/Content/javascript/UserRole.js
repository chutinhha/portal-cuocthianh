$.fn.UserChange = function () {
    $(this).change(function () {
        var id = jQuery(".usermanager").val();
        $.get("/Administrator/PermissionManagement/_ShowAll?userid=" + id, function (data) {
            $(".list").html("");
            $(".list").html(data);
            if ($("#tSortable").length > 0) {
                $('#tSortable').dataTable({
                    "iDisplayLength": 10,
                    "aLengthMenu": [5, 10, 25, 50, 100],
                    "sPaginationType": "full_numbers",
                    "iSortingCols": "STT"
                });
            }
        });
    });
};




function Success(data) {
    if (data != "") {
        alert("Cập nhật thành công");
        $('#ID').val(data.ID);
        $('#UserID').val(data.UserID);
        $('#ModuleID').val(data.ModuleID);
        if (data.Role.indexOf("C") != -1) {
            $('input[id=CExt]').attr('checked', false);
        }
        else {
            $('input[id=CExt]').attr('checked', true);
        }
        if (data.Role.indexOf("R") != -1) {
            $('input[id=RExt]').attr('checked', false);
        }
        else {
            $('input[id=RExt]').attr('checked', true);
        }
        if (data.Role.indexOf("U") != -1) {
            $('input[id=UExt]').attr('checked', false);
        }
        else {
            $('input[id=UExt]').attr('checked', true);
        }
        if (data.Role.indexOf("D") != -1) {
            $('input[id=DExt]').attr('checked', false);
        }
        else {
            $('input[id=DExt]').attr('checked', true);
        }
    }
    else {
        alert("Lỗi, vui lòng thử lại");
    }
}
