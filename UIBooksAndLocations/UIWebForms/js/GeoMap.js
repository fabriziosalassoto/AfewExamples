$(function () {
    displaymaphere();
});

function displaymaphere() {
    google.maps.visualRefresh = true;
    var myCurrentAddress = $("#txtLocationAddress").val(); //"9415 Thunder ENd, Oregon, 97617-2841, US";
    var latlng = null;
    var mapOptions = null;
    
    if(window.XMLHttpRequest){
        var request = new XMLHttpRequest();
    }
    mFormattedAddress = escape(myCurrentAddress);
    request.open("GET", "http://maps.googleapis.com/maps/api/geocode/json?address=" + mFormattedAddress + "&sensor=true", true);
    request.send();    
    request.onreadystatechange = function () {
        if (request.readyState == 4 && request.status == 200) {
            var address = eval("(" + request.responseText + ")");            
            latlng = new google.maps.LatLng(address.results[0].geometry.location.lat, address.results[0].geometry.location.lng);
            mapOptions = {
                center: latlng,
                zoom: 13,                                               
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var mMyMap = new google.maps.Map(document.getElementById("mapdetail"), mapOptions);            
            var mMyMarker = new google.maps.Marker({
                position: latlng,
                title: myCurrentAddress,                
                map: mMyMap
            });
            $("#txtLocationLatitude").val(address.results[0].geometry.location.lat);
            $("#txtLocationLongitude").val(address.results[0].geometry.location.lng);
            $('#mapnotfound').hide();            
            $('#mapmessage > h3').html(myCurrentAddress);            
            $('#mapmessage').show();
            $('#mapimage').show();
        } else {
            latlng = new google.maps.LatLng(40.585377, -98.391140);
            mapOptions = {
                zoom: 3,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var mMyMap = new google.maps.Map(document.getElementById("mapdetail"), mapOptions);
            $('#mapmessage').hide();
            $('#mapnotfound').show();
            $('#mapimage').hide();
        }
    }
}