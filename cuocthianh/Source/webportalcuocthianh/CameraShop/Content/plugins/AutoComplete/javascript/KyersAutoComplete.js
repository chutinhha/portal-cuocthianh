(function ($) {
    $.fn.kyersAutoComplete = function (options) {
        var defaults = { "dropdownid": "search-dropdown", "url": "" };
        var settings = $.extend(defaults, options || {});
        var $thistextbox = $(this);
        var $drwid = "";
        if (settings.dropdownid.length > 1) {
            $(this).after('<div id="' + settings.dropdownid + '"></div>');
        }
        $thistextbox.unbind().on("keydown", function (e) {
            $drwid = "#" + settings.dropdownid;
            var $showdr = $($drwid);
            if (e.keyCode !== 38 && e.keyCode !== 40 && e.keyCode !== 39 && e.keyCode !== 37) {
                if ($thistextbox.val().length < 2) {
                    $showdr.removeClass("active").children().remove();
                }
            }
        });
        var liHeight = 0;
        $.checkArrowUpDown = function (option) {
            var def = { "drSearch": "#search-dropdown", "textBox": $thistextbox };
            var setng = $.extend(def, option || {});
            var chooseItem = $(setng.drSearch).find(".dropdown-li.focus");
            var eItem = $(setng.drSearch).find(".dropdown-li");
            var ekey = setng.eventX;
            if (ekey.keyCode === 40 || ekey.keyCode === 39) { // down
                if (chooseItem.length === 0) {
                    eItem.first().addClass("focus");
                    liHeight = 0;
                }
                else if (chooseItem.length !== 0) {
                    if (chooseItem.next().length === 0) {
                        liHeight = 0;
                        eItem.removeClass("focus");
                        eItem.first().addClass("focus");
                    }
                    else {
                        liHeight += eItem.height();
                        chooseItem.next().addClass("focus");
                        chooseItem.removeClass("focus");
                    }
                }
                $(setng.drSearch).find("ul").scrollTop(liHeight);
                setng.textBox.val($(setng.drSearch).find(".dropdown-li.focus").find(".atcp-title").text());
            }

            if (ekey.keyCode === 38 || ekey.keyCode === 37) { // up
                if (chooseItem.length === 0) {
                    eItem.last().addClass("focus");
                    liHeight = eItem.height() * eItem.length;
                }
                else if (chooseItem.length !== 0) {
                    if (chooseItem.prev().length === 0) {
                        liHeight = $(setng.drSearch).find("ul").height();
                        eItem.removeClass("focus");
                        eItem.last().addClass("focus");
                    }
                    else {
                        liHeight -= eItem.height();
                        chooseItem.prev().addClass("focus");
                        chooseItem.removeClass("focus");
                    }
                }
                $(setng.drSearch).find("ul").scrollTop(liHeight);
                setng.textBox.val($(setng.drSearch).find(".dropdown-li.focus").find(".atcp-title").text());
            }
        };
        $(document).on("click", function (e) {
            $drwid = "#" + settings.dropdownid;
            var $showdr = $($drwid);
            if (!$(e.target).closest($thistextbox, $drwid).length) {
                $showdr.removeClass("active").children().remove();
            }
        });
        $thistextbox.unbind().on('keyup', function (e) {
            $drwid = "#" + settings.dropdownid;
            var $showdr = $($drwid);
            var eventX = e;
            $.checkArrowUpDown({ "textBox": $thistextbox, "drSearch": $drwid, "eventX": eventX });
            if (e.keyCode !== 38 && e.keyCode !== 40 && e.keyCode !== 39 && e.keyCode !== 37 && e.keyCode != 17) {
                if ($thistextbox.val().length >= 3) {
                    $($drwid).addClass("active");
                    var catId = $("#selectSearch").find("option:selected").val();
                    $.ajax({
                        type: 'get',
                        url: settings.url,
                        dataType: 'json',
                        data: { value: $thistextbox.val(), id: catId },
                        contentType: "application/json; charset=utf-8",
                        //traditional: true,
                        success: function (edata) {
                            edata.responseText != null ? jsonString = $.parseJSON(edata.responseText) : jsonString = edata;
                            if (jsonString.length > 0) {
                                $showdr.find("ul").remove();
                                $showdr.find(".auto-dropdown-arrow").remove();

                                var htmlStr = '<div class="auto-dropdown-arrow"></div><ul class="auto-dropdwon-ul">';
                                $.each(jsonString, function (eIndex, jData) {
                                    htmlStr += '<li class="dropdown-li">';
                                    htmlStr += '<a class="atcp-ahref" href="' + jData.Link + '">';
                                    htmlStr += '<div class="atcp-img"><img src="' + jData.Image + '"/></div>';
                                    htmlStr += '<div class="atcp-infor-block"><span class="atcp-title">' + jData.Name + '</span>';
                                    var deScription = jData.Description;
                                    if (deScription.length >= 50) {
                                        deScription = deScription.substring(0, 50) + "...";
                                    }
                                    htmlStr += '<span class="atcp-description">' + deScription + '</span><span class="atcp-type">' + jData.Type + '</span></div>';
                                    htmlStr += '</a>';
                                    htmlStr += '</li>';
                                });
                                htmlStr += '</ul>';
                                $showdr.append(htmlStr).addClass("active");
                                if (e.keyCode == 13) {
                                    $showdr.removeClass("active").children().remove();
                                }
                            }
                        },
                        error: function () {
                            return;
                        }
                    });
                }
                else {
                    $showdr.find(".auto-dropdown-arrow").remove();
                    $showdr.find("ul").remove();
                    return;
                }
            }
        });
    };
})(jQuery);