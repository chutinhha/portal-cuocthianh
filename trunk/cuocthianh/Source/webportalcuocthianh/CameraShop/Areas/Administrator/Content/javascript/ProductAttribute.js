
function reloadProductAttributeGrid() {
    $('#tSortableProductAttribute').dataTable({
        "iDisplayLength": 5,
        "aLengthMenu": [5, 10, 25, 50, 100],
        "sPaginationType": "full_numbers",
        "iSortingCols": "STT"
    });
}


function reloadProductAttributeGridByLink() {
    var _productid = jQuery("#ID").val();
    $.get("/Administrator/ProductAttributeManagement/_ShowAll?productid=" + _productid, function (data) {
        $(".listproductattribute").html("");
        $(".listproductattribute").html(data);
        if ($("#tSortableProductAttribute").length > 0) {
            reloadProductAttributeGrid();
        }
    });
}


function FirstLoadProductAttribute() {
    reloadProductAttributeGrid();
    jQuery(".imageattribute").hide();
    jQuery(".valueattribute").hide();
    jQuery(".cmdupdateproductattribute").hide();
    jQuery(".ckactive_row").hide();

}


function ComboboxAtributeChange() {

    var text = $("select[name='AttributeID'] option:selected").text();
    if (text == "Hình ảnh") {
        jQuery(".valueattribute").hide();
        jQuery(".imageattribute").show();
        jQuery(".cmdupdateproductattribute").show();
        jQuery(".ckactive_row").show();
        jQuery("#uniform-fileuploadproductattribute").show();
        jQuery("#uniform-ckactive").show();
    }
    else if (text != "Chọn một thuộc tính") {
        jQuery(".imageattribute").hide();

        jQuery(".valueattribute").show();
        jQuery(".cmdupdateproductattribute").show();
        jQuery(".ckactive_row").show();
        jQuery("#uniform-fileuploadproductattribute").show();
        jQuery("#uniform-ckactive").show();
    }
    else {
        jQuery(".imageattribute").hide();
        jQuery(".valueattribute").hide();
        jQuery(".cmdupdateproductattribute").hide();
        jQuery(".ckactive_row").hide();
        jQuery("#uniform-fileuploadproductattribute").hide();
        jQuery("#uniform-ckactive").hide();
    }

}


function ShowEdit(e, data) {
    e.preventDefault() ? e.preventDefault() : e.returnValue = false;
    var _id = data.attr("_id");
    var _attributename = data.attr("_attributename");
    var _value = data.attr("_value");
    var _active = data.attr("_active");
    var _attributeid = data.attr("_attributeid");
    $("#AttributeID").val(_attributeid);
    jQuery("#IDExt").val(_id);
    jQuery("#Value").val(_value);

    if (_active == "False") {
        $('input[id=ckactive]').attr('checked', false);
    }
    else {
        $('input[id=ckactive]').attr('checked', true);
    }


    if (_attributename == "Hình ảnh") {

        jQuery(".valueattribute").hide();
        jQuery(".imageattribute").show();
        jQuery('#imageattribute').html('<img width=\"70px\" height=\"70px\" src="/Media/ProductAttribute/' + _value + '" />');
        jQuery(".cmdupdateproductattribute").show();
        jQuery(".ckactive_row").show();
        jQuery("#uniform-fileuploadproductattribute").show();
        jQuery("#uniform-ckactive").show();

    }
    else {
        jQuery(".imageattribute").hide();
        jQuery(".valueattribute").show();
        jQuery(".cmdupdateproductattribute").show();
        jQuery(".ckactive_row").show();
        jQuery("#uniform-fileuploadproductattribute").show();
        jQuery("#uniform-ckactive").show();

    }
}

function DeleteProductAttribute(e, data) {
    e.preventDefault() ? e.preventDefault() : e.returnValue = false;
    var _id = data.attr("_id");
    $.ajax({
        type: "POST",
        url: '/Administrator/ProductAttributeManagement/Delete/',
        data: {
            id:_id
        },
        traditional: true,
        dataType: 'json',
        complete: function (edata) {
            if (edata.responseText == "ok") {
                alert("Xóa thành công");
                jQuery("#IDExt").val("0");
                $("#AttributeID").val("0");
                $('input[id=ckactive]').attr('checked', false);
                jQuery(".imageattribute").hide();
                jQuery("#Value").val("");
                jQuery(".valueattribute").hide();
                jQuery(".cmdupdateproductattribute").hide();
                jQuery(".ckactive_row").hide();
                reloadProductAttributeGridByLink();
            } else {
                alert(edata.responseText);
            }
        }
    });


}


$.fn.UpdateProductAttribute = function () {
    $(this).click(function () {
        var text = $("select[name='AttributeID'] option:selected").text();
        var id = jQuery("#IDExt").val();
        var _AttributeID = $("#AttributeID").val();
        var value = jQuery("#Value").val();
        var active = $('#ckactive').attr('checked') ? true : false;
        {
            $.ajax({
                type: "POST",
                url: '/Administrator/ProductAttributeManagement/AddOrUpdate/',
                data: {
                    ID: id,
                    AttributeID: _AttributeID,
                    Value: value,
                    Active: active
                },
                traditional: true,
                dataType: 'json',
                complete: function (edata) {

                    if (edata.responseText == "ok") {

                        alert("Cập nhật thành công");
                        jQuery("#IDExt").val("0");
                        $("#AttributeID").val("0");
                        $('input[id=ckactive]').attr('checked', false);
                        //FirstLoadProductAttribute();
                        jQuery(".imageattribute").hide();
                        jQuery("#Value").val("");
                        jQuery(".valueattribute").hide();
                        jQuery(".cmdupdateproductattribute").hide();
                        jQuery(".ckactive_row").hide();
                        reloadProductAttributeGridByLink();
                    } else {
                        alert(edata.responseText);
                    }
                }
            });
        }


    });
};

function SuccessProductAttribute(data) {
    
}


       
