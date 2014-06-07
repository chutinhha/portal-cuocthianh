
function reloadGrid(groupid) {
    $('#tSortableuser'+groupid).dataTable({
        "iDisplayLength": 5,
        "aLengthMenu": [5, 10, 25, 50, 100],
        "sPaginationType": "full_numbers",
        "iSortingCols": "STT"
    });
}

function reloadGridGroupCustomer(data) {
  
        $(".listcus").html("");
        $(".listcus").html(data);
        if ($("#tSortable1").length > 0) {
            reloadGrid(1);
        }
}
function reloadGridGroupManager(data) {

        $(".listmanager").html("");
        $(".listmanager").html(data);
        if ($("#tSortable2").length > 0) {
            reloadGrid(2);
        }
}
function reloadGridGroupAdmin(data) {

        $(".listadmin").html("");
        $(".listadmin").html(data);
        if ($("#tSortable3").length > 0) {
            reloadGrid(3);

        }
}

function Success(data) {
    if (data != "") {
        alert("Cập nhật thành công");
        $('#ID').val(data.ID);
        $('#Name').val(data.Name);
        $('#UserName').val(data.UserName);
        $('#Password').val(data.Password);
        $('#GroupIDExt').val(data.GroupIDExt);
        if (data.Active == false) {
            $('input[id=Active]').attr('checked', false);
        }
        else {
            $('input[id=Active]').attr('checked', true);
        }
        $('#Email').val(data.Email);
        $('#Address').val(data.Address);
    }
    else {
        alert("Lỗi, vui lòng thử lại");
    }
}



$.fn.ShowSearchForm = function () {
    $(this).click(function () {
        jQuery(".formsearch").show();
    });
};
$.fn.CloseSearchForm = function () {
    $(this).click(function () {
        jQuery(".formsearch").hide();
        jQuery("#Name").val('');
        jQuery("#UserName").val('');
        jQuery("#Email").val('');




    });
};


$.fn.Search = function () {
    $(this).click(function () {
        var name = jQuery("#Name").val();
        var username = jQuery("#UserName").val();
        var email = jQuery("#Email").val();
        var group = jQuery("#GroupIDExt").val();

        $.ajax({
            type: "POST",
            url: '/Administrator/UserManagement/Search/',
            data: {
                name: name,
                username: username,
                email: email,
                groupid: group
            },
            traditional: true,
            dataType: 'json',
            complete: function (edata) {
               switch(group) {
                     case "1":
                         reloadGridGroupCustomer(edata.responseText);
                     break;
                     case "2":
                         reloadGridGroupManager(edata.responseText);
                     break;
                     case "3":
                         reloadGridGroupAdmin(edata.responseText);
                     break;
               }
            }
            
        });

    });
};

