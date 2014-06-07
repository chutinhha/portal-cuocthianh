function initialize() {
    var myLatlng = new google.maps.LatLng(10.791999997896976, 106.68699999921955);
    var mapOptions = {
        zoom: 18,
        center: myLatlng
    };

    var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    var contentString = '<div style="width:300px;height:40px;line-height:40px; overflow: hidden;">456 Hai Bà Trưng, phường Tân Định, quận 1, TP.HCM</div>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });

    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map
    });
    infowindow.open(map, marker);
    google.maps.event.addListener(marker, 'click', function () {
        
    });
}

google.maps.event.addDomListener(window, 'load', initialize);
(function($){
	
}(jQuery));
(function($){
	$(document).ready(function(e) {
	   $(".showcat-button").showDropDown({"showDiv":".nav-category","eventX":"mouseover", "leaveToOut":true});
    });
}(jQuery));