(function($){
	
}(jQuery));
(function($){
	$(document).ready(function(e) {
	  // $(".showcat-button").showDropDown({"showDiv":".nav-category","eventX":"mouseover", "leaveToOut":true});
       $(".loginweb").loginweb();
      //  $(".loginFBweb").loginfacebookweb();
    });
     $.fn.loginweb = function () {
        $(this).click(function () {
        var Username = $("#user_login").val();
        var Password = $("#user_pass").val();
        $.ajax({
            type: 'POST',
            url: '/Home/LoginHome/',
            data: { Username: Username, Password: Password },
            traditional: true,
            dataType: 'json',
            complete: function (edata) {
                    if(edata.responseText="OK")
                    {
                        location ="/User/Profile";
                    }
                    else
                    {
                        alert(edata.responseText);
                    }
                }
            });
        });
     };

   $.fn.loginfacebookweb = function () {
        $(this).click(function () {     
            $.ajax({
                type: 'POST',
                url: '/Home/LoginFB/',
                data: { },
                traditional: true,
                dataType: 'json',
                complete: function (edata) {
               
                }
             });
        });
     };
}(jQuery));




