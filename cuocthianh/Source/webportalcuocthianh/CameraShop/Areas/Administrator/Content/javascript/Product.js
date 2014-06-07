
        function reloadGridProduct() {
            $('#tSortableproduct').dataTable({
                "iDisplayLength": 5,
                "aLengthMenu": [5, 10, 25, 50, 100],
                "sPaginationType": "full_numbers",
                "iSortingCols": "STT"
            });
        }

        function reloadGrid(data) {

            $(".listproduct").html("");
            $(".listproduct").html(data);
            if ($("#tSortableproduct").length > 0) {
                reloadGridProduct();
            }
        }


        function Success(data) 
        {
            if (data == "exist") {
                alert("Tên sản phẩm bị trùng");
            }
            else {
                if (data != "") {
                    alert("Cập nhật thành công");
                    $('#ID').val(data.ID);
                    $('#Code').val(data.Code);
                    $('#Name').val(data.Name);
                    $('#ShortDescription').val(data.ShortDescription);
                    CKEDITOR.instances['FullDescription'].setData(data.FullDescription);
                    $('#ProductTypeID').val(data.ProductTypeID);
                    $('#CateID').val(data.CateID);
                    $('#ManufacturerIDExt').val(data.ManufacturerIDExt);
                    $('#ModelID').val(data.ModelID);
                    $('#Price').val(data.Price);
                    $('#Instock').val(data.Instock);
                    $('#New').val(data.New);
                    $('#Typical').val(data.Typical);
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



        $.fn.ShowSearchForm = function () {
            $(this).click(function () {
                jQuery(".formsearch").show();
            });
        };
        $.fn.CloseSearchForm = function () {
            $(this).click(function () {
                jQuery(".formsearch").hide();
                jQuery("#Name").val('');
                jQuery("#Code").val('');

                $.ajax({
                    type: "POST",
                    url: '/Administrator/ProductManagement/Search/',
            
                    traditional: true,
                    dataType: 'json',
                    complete: function (edata) {
                        reloadGrid(edata.responseText);
                    }

                });


            });
        };


        $.fn.Search = function () {
            $(this).click(function () {
                var name = jQuery("#Name").val();
                var code = jQuery("#Code").val();
                var producttype = jQuery("#ProductTypeID").val();
                var manufacturer = jQuery("#ManufacturerID").val();
                var category = jQuery("#CateID").val();
                $.ajax({
                    type: "POST",
                    url: '/Administrator/ProductManagement/Search/',
                    data: {
                        code: code,
                        name: name,
                        producttype: producttype,
                        manufacturer: manufacturer,
                        category: category
                    },
                    traditional: true,
                    dataType: 'json',
                    complete: function (edata) {
                        reloadGrid(edata.responseText);
                    }

                });

            });
        };

