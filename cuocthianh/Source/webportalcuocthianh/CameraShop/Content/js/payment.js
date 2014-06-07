//Phần code chính
(function ($) {
    $.fn.stepByStepKyers = function (options) {
        var stepLi = $(this);
        var defaults = { "paymentitem": ".payment-item", "delay": "500" };
        var settings = $.extend(defaults, options || {});
        //Thay đổi chiều dài các tab item
        $(window).on("load resize", function () {
            var widthItem = parseFloat($(".list-step-bullet").width()) / parseFloat($(".list-step-bullet").find("li").length) - 1;
            $(".list-step-bullet").find("li").css({ "width": widthItem });
        });
        //Thêm class color-inside
        var colorSlide = $("<div/>", {
            "class": "color-slide"
        });
        var colorInside = $("<div/>", {
            "class": "color-inside"
        });
        var stepBullet = $("<div/>", {
            "class": "step-bullet"
        });

        $(stepLi).append(colorSlide);
        $(".color-slide").append(colorInside).append(stepBullet);
        //>> Add attribute Index for Tab Item
        var i = 1;
        $.each($(stepLi), function () {
            $(this).attr("index", i++);
        });
        //>> Add Attribute Index for Tab Content
        i = 1;
        $.each($(settings.paymentitem), function () {
            $(this).attr("index", i++);
        });

        var itemPayment = $(this);
        //>> Add Class active cho Tab Item First vaf Tab Content First
        $(stepLi).first().addClass("active");
        $(settings.paymentitem).first().addClass("active");
        //Sử lý sự kiện click vào các tab item
        $(this).on("click", function () {
            var paymentWidth = parseFloat($(settings.paymentitem).width());
            var currIndex = $(stepLi).parent().find(".active").last().attr("index");
            var index = $(this).attr("index");
            if ($(this).attr("index") > currIndex) {
                //>>> Go to Next Tab Item
                $(this).addClass("active");
                $(this).prevAll().addClass("active");
                $(this).prevAll().find(".color-inside").css({ "width": "100%" });
                $(this).find(".color-inside").css({ "width": "0px" });
                $(this).find(".color-inside").animate({ "width": "100%" }, settings.delay);
                //>>> Go to Next Tab Content
                $(settings.paymentitem).removeClass("active");
                $(settings.paymentitem).css({ "left": "auto", "right": paymentWidth * -1 });

                $(settings.paymentitem).children().parent("li[index=" + index + "]").animate({ "right": "0px" }).addClass("active");
                //>>Show and Hide Buttons Control
                if (index > 1) {
                    $(".btn-preview").removeClass("hidden");
                }
                if ($(settings.paymentitem).children().parent("li.active").next().length <= 0) {
                    $(".btn-next").addClass("hidden");
                    $(".btn-finish").removeClass("hidden");
                }
            }
            else {
                //Back to preview Tab Item
                $(this).next().nextAll().removeClass("active");
                $(this).next().find(".color-inside").animate({ "width": "0" }, settings.delay, function () {
                    $(this).parent().parent("li").removeClass("active");
                });
                //>>> Back to Preview Tab Content
                if ($(this).attr("index") < currIndex) {
                    $(settings.paymentitem).removeClass("active");
                    $(settings.paymentitem).css({ "left": paymentWidth * -1, "right": "auto" });
                    $(settings.paymentitem).children().parent("li[index=" + index + "]").animate({ "left": "0px" }).addClass("active");
                }
                //>> Show and Hide Button Controls
                $(".btn-next").removeClass("hidden");
                $(".btn-finish").addClass("hidden");
                if ($(settings.paymentitem).children().parent("li.active").prev().length <= 0) {
                    $(".btn-preview").addClass("hidden");
                }
            }
        });

        $(".btn-next").on("click", function () {
            $(".btn-preview").removeClass("hidden");
            var paymentWidth = parseFloat($(settings.paymentitem).width());
            //>>>Go to Next Tab Content
            $(settings.paymentitem).children().parent("li").css({ "left": "auto", "right": paymentWidth * -1 });
            $(settings.paymentitem).children().parent("li.active").next().animate({ "right": "0" }).addClass("active");
            $(settings.paymentitem).children().parent("li.active").prev().removeClass("active");
            //>>Go to Next Tab Item
            stepLi.children().parent("li.active").next().addClass("active");
            stepLi.children().parent("li.active:last").find(".color-inside").css("width", "0");
            stepLi.children().parent("li.active:last").find(".color-inside").animate({ "width": "100%" }, settings.delay);
            //>> Show and Hide Button Controls
            if ($(settings.paymentitem).children().parent("li.active").next().length <= 0) {
                $(this).addClass("hidden");
                $(".btn-finish").removeClass("hidden");
            }
        });
        $(".btn-preview").on("click", function () {
            //>> Show and Hide Button Controls
            $(".btn-next").removeClass("hidden");
            $(".btn-finish").addClass("hidden");
            //>> Go to Preview Tab Content
            var paymentWidth = parseFloat($(settings.paymentitem).width());
            $(settings.paymentitem).children().parent("li").css({ "right": "auto", "left": paymentWidth * -1 });
            $(settings.paymentitem).children().parent("li.active").prev().animate({ "left": "0px" }).addClass("active");
            $(settings.paymentitem).children().parent("li.active").next().removeClass("active");
            //>> Go to Preivew Tab Item
            if (stepLi.children().parent("li:not(.active)").length <= 0) {
                stepLi.children().parent("li:last").find(".color-inside").animate({ "width": "0" }, settings.delay, function () {
                    $(this).parent().parent("li").removeClass("active");
                });
            }
            else if (stepLi.children().parent("li.active").length > 1) {
                stepLi.children().parent("li:not(.active)").prev().find(".color-inside").animate({ "width": "0" }, settings.delay, function () {
                    $(this).parent().parent("li").removeClass("active");
                });
            }
            //>>--------------------
            stepLi.children().parent("li.active").prev().addClass("active");
            if ($(settings.paymentitem).children().parent("li.active").prev().length <= 0) {
                $(this).addClass("hidden");
            }
        });
    }

    $.fn.LoginPament = function () {
        $(this).click(function () {
            var username = $(".txtusername").val();
            var password = $(".txtpassword").val();
            $.ajax({
                type: "POST",
                url: '/Payment/LoginPayment/',
                data: {
                    UserName: username,
                    Password: password
                },
                traditional: true,
                dataType: 'json',
                complete: function (edata) {
                    try {
                        if (edata.responseText == "ok") {
                            window.location = '/Payment';
                        }
                        else {
                            alert(edata.responseText);
                        }
                    } catch (ex) { }

                }
            });
        });

    };

    $.fn.UsingInfo = function () {
        $(this).change(function () {
            if ($(this).is(":checked")) {

                $.ajax({
                    type: "POST",
                    url: '/Payment/GetInfo/',

                    traditional: true,
                    dataType: 'json',
                    complete: function (edata) {

                        var obj = $.parseJSON(edata.responseText);
                        $(".txtname").val(obj.Name);
                        $(".txtaddress").val(obj.Address);
                        $(".txtphone").val(obj.Phone);
                        $(".txtemail").val(obj.Email);
                        jQuery('.txtcustomer').text(obj.Name);
                        jQuery('.txtaddresscustomer').text(obj.Address);
                        jQuery('.txtemailcustomer').text(obj.Email);
                        jQuery('.txtphonenumcustomer').text(obj.Phone);
                    }
                });
            }
            else {
                $(".txtname").val("");
                $(".txtaddress").val("");
                $(".txtphone").val("");
                $(".txtemail").val("");
                jQuery('.txtcustomer').text("");
                jQuery('.txtaddresscustomer').text("");
                jQuery('.txtemailcustomer').text("");
                jQuery('.txtphonenumcustomer').text("");
            }
            //'unchecked' event code
        });

    };

    $.fn.PaymentChange = function () {
        $(this).change(function () {
            if ($("#payChoose").val() == "1") {
                $("#waypay").show();
            }
            else {
                $("#waypay").hide();
            }

            jQuery('.txtpaymethod').text($("#payChoose option:selected").text());

        });
    };
    $.fn.PayAction = function () {
        $(this).on("click", function (e) {
            //var paymethod = $("#payChoose").val();
            var payway = $('input[name="paypal"]:checked').val();
            var name = $('.txtname').val();
            var phone = $('.txtphone').val();
            var add = $('.txtaddress').val();
            if (name == null || name == "") {
                e.preventDefault() ? e.preventDefault() : e.returnValue = false;
                alert("Vui lòng nhập tên");
                return;
            }
            if (phone == null || phone == "") {
                e.preventDefault() ? e.preventDefault() : e.returnValue = false;
                alert("Vui lòng nhập điện thoại");
                return;
            }
            if (add == null || add == "") {
                e.preventDefault() ? e.preventDefault() : e.returnValue = false;
                alert("Vui lòng nhập địa chỉ");
                return;
            }

            $("#imageloading").show();

            $.ajax({
                type: "POST",
                url: '/Payment/PaymentAction/',
                data: {
                    //paymethod: paymethod,
                    waypay: payway,
                    address: add,
                    name: name,
                    phone: phone
                },
                traditional: true,
                dataType: 'json',
                complete: function (edata) {
                    window.location.href = edata.responseText;
                }
            });

        });
    }


    $.fn.PayWithCreditCard = function () {
        $(this).click(function () {
            var Lastname = $("#Lastname").val();
            var Firstname = $("#Firstname").val();
            var Address = $("#Address").val();
            var City = $("#City").val();
            var State = $("#State").val();
            var CountryCode = $("#CountryCode").val();
            var CountryName = $("#CountryName").val();
            var Zipcode = $("#Zipcode").val();
            var Cardtype = $("#Cardtype").val();
            var CardNumber = $("#CardNumber").val();
            var CVV2 = $("#CVV2").val();
            var ExpriredMonth = $("#ExpriredMonth").val();
            var ExpriredYear = $("#ExpriredYear").val();
            if (Lastname == null || Lastname == "") {
                alert("Last name không được trống");
                return;
            }
            if (Firstname == null || Firstname == "") {
                alert("First name không được trống");
                return
            }
            if (Address == null || Address == "") {
                alert("Last name không được trống");
                return;
            }
            if (City == null || City == "") {
                alert("City không được trống");
                return;
            }
            if (State == null || State == "") {
                alert("State không được trống");
                return;
            }
            if (CountryCode == null || CountryCode == "") {
                alert("Country Code không được trống");
                return;
            }
            if (CountryName == null || CountryName == "") {
                alert("Country Name không được trống");
                return;
            }
            if (Zipcode == null || Zipcode == "") {
                alert("Zipcode không được trống");
                return;
            }
            if (Cardtype == null || Cardtype == "") {
                alert("Cardt ype không được trống");
                return;
            }
            if (CardNumber == null || CardNumber == "") {
                alert("Card Number không được trống");
                return;
            }
            if (CVV2 == null || CVV2 == "") {
                alert("CVV2 không được trống");
                return;
            }
            if (ExpriredMonth == null || ExpriredMonth == "") {
                alert("Exprired Month không được trống");
                return;
            }
            if (ExpriredYear == null || ExpriredYear == "") {
                alert("Exprired Year không được trống");
                return;
            }
            $.ajax({
                type: "POST",
                url: '/Payment/PayCreditCard/',
                data: {
                    Lastname: Lastname,
                    Firstname: Firstname,
                    Address: Address,
                    City: City,
                    State: State,
                    CountryCode: CountryCode,
                    CountryName: CountryName,
                    Zipcode: Zipcode,
                    Cardtype: Cardtype,
                    CardNumber: CardNumber,
                    CVV2: CVV2,
                    ExpriredMonth: ExpriredMonth,
                    ExpriredYear: ExpriredYear
                },
                traditional: true,
                dataType: 'json',
                complete: function (edata) {
                    try {
                        if (edata.responseText == "Error") {
                            alert("Lỗi, vui lòng thử lại");

                        }
                        else {
                            window.location.href = edata.responseText;
                        }
                    } catch (ex) { }

                }
            });
        });

    };


    jQuery.fn.changename = function () {
        jQuery(this).on('keyup', function (e) {
            var value = jQuery(this).val();
            jQuery('.txtcustomer').text(value);
        });
    };
    jQuery.fn.changeaddress = function () {
        jQuery(this).on('keyup', function (e) {
            var value = jQuery(this).val();
            jQuery('.txtaddresscustomer').text(value);
        });
    };
    jQuery.fn.changeemail = function () {
        jQuery(this).on('keyup', function (e) {
            var value = jQuery(this).val();
            jQuery('.txtemailcustomer').text(value);
        });
    };
    jQuery.fn.changephone = function () {
        jQuery(this).on('keyup', function (e) {
            var value = jQuery(this).val();
            jQuery('.txtphonenumcustomer').text(value);
        });
    };
} (jQuery));
(function ($) {
    $(document).ready(function (e) {
        $(".showcat-button").showDropDown({ "showDiv": ".nav-category", "eventX": "mouseover", "leaveToOut": true });
        $(".list-step-bullet li").stepByStepKyers({ "paymentitem": ".payment-item" });



        jQuery("#second-btn-login").LoginPament();
        jQuery("#chk-use-info").UsingInfo();
        $("#waypay").hide();
        jQuery("#payChoose").PaymentChange();

        $(".btn-finish").PayAction();
        $("#imageloading").hide();
        $(".txtname").changename();
        $(".txtaddress").changeaddress();
        $(".txtemail").changeemail();
        $(".txtphone").changephone();
        jQuery('.txtpaymethod').text($("#payChoose option:selected").text());
    });
} (jQuery));