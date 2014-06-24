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

                    var username =$(".user_login").val();
                    var password =$(".user_pass").val();
                    $.ajax({
                        type: 'POST',
                        url: '/Home/LoginHome/',
                        data: { Username: username, Password: password },
                        traditional: true,
                        dataType: 'json',
                        complete: function (edata) {
               
                           if(edata.responseText="OK")
                           {
                              location ="/home/index";
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




