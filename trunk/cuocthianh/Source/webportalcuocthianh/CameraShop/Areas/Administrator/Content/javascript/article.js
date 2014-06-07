
function reloadGridArticle() {
    $('#tSortablearticle').dataTable({
        "iDisplayLength": 5,
        "aLengthMenu": [5, 10, 25, 50, 100],
        "sPaginationType": "full_numbers",
        "iSortingCols": "STT"
    });
}


function Success(data) {
    if (data == "exist") {
        alert("Tên bài viết bị trùng");
    }
    else {
        if (data != "") {
            //alert("Cập nhật thành công");
            $.topAlertPopup({ "text": "Cập nhật bài viết thành công", "typeAlert": "success" });
            $('#ID').val(data.ID);
            $('#Title').val(data.Title);
            $('#ShortDescription').val(data.ShortDescription);
            CKEDITOR.instances['Content'].setData(data.FullDescription);
            $('#CateID').val(data.CateID);
            $('#ShowHomePage').val(data.ShowHomePage);
            $('#Active').val(data.Active);
            $('#Link').val(data.Link);
            $('#MetaTitle').val(data.MetaTitle);
            $('#MetaKeyword').val(data.MetaKeyword);
            $('#MetaDescription').val(data.MetaDescription);


            if (data.Active == false) {
                $('input[id=Active]').attr('checked', false);
            }
            else {
                $('input[id=Active]').attr('checked', true);
            }
        }
        else {
            alert("Lỗi, vui lòng thử lại");
        }
    }
}