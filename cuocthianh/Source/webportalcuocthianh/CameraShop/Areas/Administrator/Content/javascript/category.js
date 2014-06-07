
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
                alert("Tên danh mục bị trùng");
            }
            else {
                if (data != "") {
                    alert("Cập nhật thành công");
                    $('#ID').val(data.ID);
                    $('#Name').val(data.Name);
                    $('#Note').val(data.Note);
                    if (data.Active == false) {
                        $('input[id=Active]').attr('checked', false);
                    }
                    else {
                        $('input[id=Active]').attr('checked', true);
                    }
                    $('#Link').val(data.Link);
                    $('#MetaTitle').val(data.MetaTitle);
                    $('#MetaKeyword').val(data.MetaKeyword);
                    $('#MetaDescription').val(data.MetaDescription);
                }
                else {
                    alert("Lỗi, vui lòng thử lại");
                }
            }
        }


       
