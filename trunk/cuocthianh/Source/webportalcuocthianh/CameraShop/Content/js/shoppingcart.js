(function($){
    $.fn.tickCheckBox = function (options) {
        var defaults = { "checkitem": ".chk-item" };
        var settings = $.extend(defaults, options || {});
        $(this).on("click", function () {
            $(this).is(':checked') ? $(settings.checkitem).prop('checked', true) : $(settings.checkitem).prop('checked', false);
        });
    }
    //Get array of active checkbox
    $.getArraychkID = function (options) {
        var defaults = { "checkitem": ".chk-item" };
        var settings = $.extend(defaults, options || {});
        var array = [];
        var _chk = $(settings.checkitem);
        $.each(_chk, function (i, e) {
            if ($(this).is(":checked") == true) {
                _text = $(this).attr("chkid");
                array.push(_text);
            }
        });
        return array;
    }
    $.fn.countTotalPrice = function () {
        var sumcount = 0;
        for (var vl in $(".amount-price")) {
            temStr = $(".amount-price")[vl].textContent;
            sumcount += parseFloat(temStr.replace(/[.]/gi, ""));
        }
        return sumcount;
    }
    //Click To Delete select cart
    $.fn.deleteSelectCart = function () {
        $(this).on("click", function (e) {
            e.preventDefault() ? e.preventDefault() : e.returnValue;
            var arrSelect = $.getArraychkID();
            $.ajax({
                type: 'POST',
                url: '/ShoppingCart/RemoveSelectCart/',
                traditional: true,
                dataType: 'json',
                data: {
                    arrSelect: arrSelect
                },
                complete: function (e, responseText) {
                    if (responseText !== "error") {
                        $.topAlertPopup({ "text": "Xóa giỏ hàng thành công", "typeAlert": "success", "delay": "900" });
                        setInterval(function () {
                            RefreshHeader();
                            RefreshCart();
                        }, 900);
                    }
                    else {
                        $.topAlertPopup({ "text": "Xóa thất bại, do truy xuất dữ liệu hoặc bạn không chọn giỏ hàng nào", "typeAlert": "error", "delay": "2000" });
                    }
                }
            })
        });
    }
    function RefreshHeader() {
        $.get("/ShoppingCart/_CartHeader", function (data) {
            $(".nav-cart-box").html(data);
        });
    }
    function RefreshCart() {
        $.get("/ShoppingCart/_ListCart", function (data) {
            $(".cart-view").html(data);
        });
    }
    //Key up to update cart price
    $.fn.updateCartPrice = function () {
        $(this).on("keyup", function (e) {
            var singlePrice = parseFloat($(this).parent("td").parent("tr").find(".single-price").text().replace(/[.]/gi, ""));
            var amountPrice = $(this).parent("td").parent("tr").find(".amount-price");
            var priceFormat = formatPrice(parseFloat(singlePrice * parseFloat($(this).val())), ".");
            amountPrice.text(priceFormat + " ₫");
            //$("#totalPrice").text($.fn.countTotalPrice());
            var sumcount = 0;
            $.each($(".amount-price"), function (index, data) {
                sumcount += parseFloat(data.textContent.replace(/[.]/gi, ""));
                var newPrice = formatPrice(sumcount, ".");
                $("#totalPrice").text(newPrice + " ₫");
            });
        });
    }
    
    //Hàm format giá
    function formatPrice(str, insertChar) {
        if (isFinite(str) === false) {
            str = 0;
        }
        str = str.toString();
        for (var i = str.length - 1; i > 0; i--) {
            if (i % 3 === 0) {
                str = str.insertAt(str.length - i, insertChar);
            }
        }
        return str;
    }
}(jQuery));
(function($){
	$(document).ready(function(e) {
	    $(".showcat-button").showDropDown({"showDiv":".nav-category","eventX":"mouseover", "leaveToOut":true});
        $(".chk-all").tickCheckBox({"checkitem":".chk-item"});
        $(".txt-count").updateCartPrice();
        $(".btn-delete-select").deleteSelectCart();
        $(".slideProduct").belloSlideSet({"numItem": 6});
    });
}(jQuery));