var relativeX = 0;
var relativeY = 0;


function GetScreenShot() {
    var myImageDesc = "";
    $.ajax({
        type: "POST",
        url: "ImageHandler.ashx",
        data: { ip: $("#txtIP").val(), port: $("#txtStartingPort").val() },
        async: false,
        success: function (data) {
            myImageDesc = data;
        }
    });
    $("#myImage").attr("src", myImageDesc);
    $("#maindiv").attr("width", $("#myImage").width());
    $("#maindiv").attr("height", $("#myImage").height());


    setTimeout(function () {
        GetScreenShot();
    }, 1000);
}

function ControlUpdate() {            
    $.ajax({
        type: "POST",
        url: "ControlsHandler.ashx",
        data: { method: 'ControlValueUpdate', ip: $("#txtIP").val(), port: $("#txtStartingPort").val(), control: $("#ControlSelected").val(), value: $("#ControlValue").val() },
        async: false,
        success: function (data) {
        
        }
    });    
}


$(function () {
    $("#myImage").draggable();
    $("#Table1").draggable();
    $("#Widget").draggable();
    GetScreenShot();

    $("#myImage").click(function (e) {
        var offset = $(this).offset();
        relativeX = (e.pageX - Math.round(offset.left));
        relativeY = (e.pageY - Math.round(offset.top));
        $("#MousePosition").val("X: " + relativeX + "  Y: " + relativeY);
        $.ajax({
            type: "POST",
            url: "ControlsHandler.ashx",
            data: { method: 'GetControl', ip: $("#txtIP").val(), port: $("#txtStartingPort").val(), x: relativeX, y: relativeY, control: $("#ControlSelected").val() },
            async: false,
            success: function (data) {
                $("#ControlSelected").val(data.controls[0].name);
                $("#ControlValue").val(data.controls[0].value);
            }
        });
    });

    $("body").keyup(function (key) {
        var keypressed = $("#ControlValue").val();
        if (key.which == 13) {
            event.preventDefault();
        } else if (key.which == 8) {            
            keypressed = keypressed.substring(0, keypressed.length - 1);
        } 
        $("#ControlValue").val(keypressed);
        $.ajax({
            type: "POST",
            url: "ControlsHandler.ashx",
            data: { method: 'ControlValueUpdate', ip: $("#txtIP").val(), port: $("#txtStartingPort").val(), control: $("#ControlSelected").val(), value: keypressed },
            async: false
        });
        ControlUpdate();
    });



    $("body").keypress(function (key) {
        var keypressed = $("#ControlValue").val();
        if (key.charCode == 13) {
            event.preventDefault();
        } else {
            if (!$("#ControlValue").is(":focus")) {
                keypressed = $("#ControlValue").val() + String.fromCharCode(key.charCode);
            }                        
        }
        $("#ControlValue").val(keypressed);
        $.ajax({
            type: "POST",
            url: "ControlsHandler.ashx",
            data: { method: 'ControlValueUpdate', ip: $("#txtIP").val(), port: $("#txtStartingPort").val(), control: $("#ControlSelected").val(), value: keypressed },
            async: false
        });
        ControlUpdate();
    });
});

