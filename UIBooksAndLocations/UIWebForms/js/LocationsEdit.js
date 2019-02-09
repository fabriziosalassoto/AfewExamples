var mPriorLocationID = "";
var mPriorLocationName = "";
var mPriorLocationDescription = "";
var mPriorLocationAddress = "";
var mPriorLocationLatitude = "";
var mPriorLocationLongitude = "";

$(function () {
    GetLocation();
});

$("#cmdCancel").on("click", function () {
    CancelLocationCRUD();
});

$("#cmdSave").on("click", function () {
    SaveLocation();
});

function GetLocation() {
    var mXML = "";
    if (+$('#hdnLocationID').val() > 0) {
        $.ajax({
            url: 'LocationReadHandler.ashx',
            type: 'POST',
            processData: false,
            contentType: 'text/xml',
            async: false,
            data: $('#hdnLocationID').val(),
            success: function (result) {
                mXML = result;
            },
            error: function () {
                alert("Error getting Location");
            }
        });
        DisplayLocation(mXML);
    }
}

function ViewMap() {    
    $(parent.document).find('#bodywidgetMap').load('GeoMap.html');    
    $(parent.document).find('#widgetMap').show();    
}

function DisplayLocation(pXML) {
    $("#hdnMyLocationID").val($(pXML).find('locationid').text());
    $("#txtLocationName").val($(pXML).find('locationname').text());
    $("#txtLocationDescription").val($(pXML).find('locationdescription').text());
    $("#txtLocationAddress").val($(pXML).find('locationaddress').text());
    $("#txtLocationLatitude").val($(pXML).find('locationlatitude').text());
    $("#txtLocationLongitude").val($(pXML).find('locationlongitude').text());
    UpdateLocationUI();
}

function SaveLocation() {
    var mStrXML = "<location>";
    var mStatus = "";
    mStrXML += "<locationid>" + $("#hdnMyLocationID").val() + "</locationid>";
    mStrXML += "<locationname>" + $("#txtLocationName").val() + "</locationname>";
    mStrXML += "<locationdescription>" + $("#txtLocationDescription").val() + "</locationdescription>";
    mStrXML += "<locationaddress>" + $("#txtLocationAddress").val() + "</locationaddress>";
    mStrXML += "<locationlatitude>" + $("#txtLocationLatitude").val() + "</locationlatitude>";
    mStrXML += "<locationlongitude>" + $("#txtLocationLongitude").val() + "</locationlongitude>";
    mStrXML += "</location>";

    $.ajax({
        url: 'LocationEditionHandler.ashx',
        type: 'POST',
        processData: false,
        contentType: 'text/xml',
        async: false,
        data: mStrXML,
        success: function (result) {
            if (result != "failure") {
                mStatus = result;
                DisplayLocation(result);
            }
            mStatus = result;
            CleanForm();
            GetBooksAndLocations();
        },
        error: function () {
            mStatus = "failure";
        }
    });
    DisplayMessage(mStatus);
    $("#widgetLocationsEdit").hide();
}

function CancelLocationCRUD() {
    $("#hdnMyLocationID").val(mPriorLocationID);
    $("#txtLocationName").val(mPriorLocationName);
    $("#txtLocationDescription").val(mPriorLocationDescription);
    $("#txtLocationAddress").val(mPriorLocationAddress);
    $("#txtLocationLatitude").val(mPriorLocationLatitude);
    $("#txtLocationLongitude").val(mPriorLocationLongitude);
}

function UpdateLocationUI() {
    mPriorLocationID = $("#hdnMyLocationID").val();
    mPriorLocationName = $("#txtLocationName").val();
    mPriorLocationDescription = $("#txtLocationDescription").val();
    mPriorLocationAddress = $("#txtLocationAddress").val();
    mPriorLocationLatitude = $("#txtLocationLatitude").val();
    mPriorLocationLongitude = $("#txtLocationLongitude").val();
}

function DisplayMessage(pStatus) {
    if (pStatus == "failure" || pStatus == "") {
        alert("There was an Error, the current location was not saved");
    } else {
        alert("The current location was successfully saved");
    }
}