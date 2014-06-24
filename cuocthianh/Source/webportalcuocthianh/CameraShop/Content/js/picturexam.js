
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

$.fn.AddIntroduce = function () {
    $(this).on("click", function () {
        var content = $("div.nicEdit-main").html();
       
        $.ajax({
            type: "POST",
            url: '/Examinee/UpdateDescription/',
            data: {
                content: content

            },
            traditional: true,
            dataType: 'json',
            complete: function (e) {
                if (e.responseText == "OK") {
                   
                    alert("Cập nhật thành công");
                }
                else {
                    alert("Lỗi, vui lòng thử lại");
                }
            }
        });
    });
}





$(document).ready(function () {
    $(".picture-fancy").colorbox({ rel: 'picture-fancy' });
    $(".picture-samll").colorbox({ rel: 'picture-samll' });
    $("#btn-updateintroduce").AddIntroduce();
});
       
