$.fn.Genneral = function () {
    $(this).click(function () {
        var title = jQuery("#Title").val();
        var metadescription = jQuery("#Metadescription").val();
        var metakeyword = jQuery("#Metakeyword").val();
        var facebook = jQuery("#Facebook").val();
        var data = "Title+" + title + "##" + "Metakeyword+" + metakeyword + "##" + "Metadescription+" + metadescription + "##" + "Facebook+" + facebook;
        $.ajax({
            type: "POST",
            url: '/Administrator/GeneralConfigManagement/General/',
            data: {
                data: data
            },
            traditional: true,
            dataType: 'json',
            complete: function (edata) {

                alert(edata.responseText);

            }

        });

    });
};



$.fn.Email = function () {
    $(this).click(function () {
        var email = jQuery("#Email").val();
        var emailpass = jQuery("#EmailPassword").val();
        var data = "Email+" + email + "##" + "EmailPassword+" + emailpass;
        $.ajax({
            type: "POST",
            url: '/Administrator/GeneralConfigManagement/Email/',
            data: {
                data: data
            },
            traditional: true,
            dataType: 'json',
            complete: function (edata) {
                alert(edata.responseText);
            }

        });

    });
};

$.fn.ShowAddnewForm = function () {
    $(this).click(function () {
        jQuery(".formsearch").show();
        jQuery("#txtid").text("0");
        jQuery("#NameTemplate").val("");
        jQuery("#cbtypeemail").val("1");
        CKEDITOR.instances['txtcontent'].setData("");
    });
};
$.fn.CloseForm = function () {
    $(this).click(function () {
        jQuery(".formsearch").hide();
        jQuery("#NameTemplate").val("");
        jQuery("#txtid").text("0");
        jQuery("#cbtypeemail").val("1");
        CKEDITOR.instances['txtcontent'].setData("");
    });
};


function ShowEdit(e, data) {
    e.preventDefault() ? e.preventDefault() : e.returnValue = false;
    var _id = data.attr("_id");
    $.ajax({
        type: "POST",
        url: '/Administrator/GeneralConfigManagement/GetTemplateByID/',
        data: {
            id: _id
        },
        traditional: true,
        dataType: 'json',
        complete: function (edata) {
            var item = $.parseJSON(edata.responseText);
            jQuery(".formsearch").show();
            jQuery("#NameTemplate").val(item.Name);
            jQuery("#cbtypeemail").val(item.Type);
            jQuery("#txtid").text(item.ID);
            CKEDITOR.instances['txtcontent'].setData(item.Template);
        }
    });
}
$.fn.CreateOrUpdateTemplate = function () {
    $(this).click(function () {

        var id = jQuery("#txtid").text();
        var name = jQuery("#NameTemplate").val();
        var type = jQuery("#cbtypeemail").val();
        var template = CKEDITOR.instances['txtcontent'].getData();
        if (name == null || name == "") {
            alert("Vui lòng nhập tên cho template");
        }
        else {
            $.ajax({
                type: "POST",
                url: '/Administrator/GeneralConfigManagement/EmailTemplate/',
                data: {
                    ID: id,
                    Code: name,
                    Name: name,
                    Template: template,
                    Type: type

                },
                traditional: true,
                dataType: 'json',
                complete: function (edata) {
                    if (edata.responseText == "") {
                        alert("Lỗi, vui lòng thử lại");
                    }
                    else {
                        alert("Cập nhật thành công");
                        var item = $.parseJSON(edata.responseText);
                        jQuery(".formsearch").hide();
                        jQuery("#NameTemplate").val("");
                        jQuery("#txtid").text("0");
                        jQuery("#cbtypeemail").val("1");
                        CKEDITOR.instances['txtcontent'].setData("");
                        reloadGrid();
                    }
                }
            });
        }

    });

};


function Delete(e, data) {
    e.preventDefault() ? e.preventDefault() : e.returnValue = false;
    var _id = data.attr("_id");
    $.ajax({
        type: "POST",
        url: '/Administrator/GeneralConfigManagement/DeleteEmailTemplate/',
        data: {
            id: _id
        },
        traditional: true,
        dataType: 'json',
        complete: function (edata) {
            if (edata.responseText == "") {
                alert("Lỗi, vui lòng thử lại");
            }
            else {
                alert("Xóa thành công");
                reloadGrid();
            }
        }
    });
}

function reloadGrid() {

    $.get("/Administrator/GeneralConfigManagement/_EmailTemplate", function (data) {
        $(".listmail").html("");
        $(".listmail").html(data);
        if ($("#tSortable").length > 0) {
            $('#tSortable').dataTable({
                "iDisplayLength": 5,
                "aLengthMenu": [5, 10, 25, 50, 100],
                "sPaginationType": "full_numbers",
                "iSortingCols": "STT"
            });
        }
    });
}