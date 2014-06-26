
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




function loadpicture() {
    $.ajax({
        type: 'get',
        url: '/PictureExam/_ListPictureExamProfile/',
        traditional: true,
        dataType: 'html',
        complete: function (edata) {
            $("#listpicture").html(edata.responseText);

        }
    });

}


$.fn.uppicture = function () {
    $(this).on("click", function () {
        var title = $("#Title").val();
        var exid = $("#txtexid").val();

        if (title == "") {
            //alert
        }
        $.ajax({
            type: "POST",
            url: '/PictureExam/UploadPicture/',
            data: {
                ExamineeID: exid,
                Title: title

            },
            traditional: true,
            dataType: 'json',
            success: function (e) {
                if (e != "-1" && !isNaN(e)) {
                    $('#image').html('');
                    $('#Title').val('');
                    loadpicture();
                    UpdateDescription();
                    alert("Upload ảnh thành công");



                }
                else if (isNaN(e)) {
                    UpdateDescription();
                    alert("Cập nhật thành công");
                    $('#Title').val('');
                    $('#image').html('');
                    //   alert(e);
                }
                else {
                    $('#Title').val('');
                    $('#image').html('');
                    alert("Lỗi, vui lòng thử lại");
                }
            }
        });
    });
}

function UpdateDescription() {


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

                //  alert("Cập nhật thành công");
            }
            else {
                // alert("Lỗi, vui lòng thử lại");
            }
        }
    });

}






$(document).ready(function () {
    $(".picture-fancy").colorbox({ rel: 'picture-fancy' });
    $(".picture-samll").colorbox({ rel: 'picture-samll' });
    //  $("#btn-updateintroduce").AddIntroduce();
    $("#btn-updateintroduce").uppicture();
});
       
