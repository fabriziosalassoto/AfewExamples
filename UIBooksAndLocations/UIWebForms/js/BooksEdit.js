var mPriorBookID = "";
var mPriorBookName = "";
var mPriorBookAuthor = "";
var mPriorBookISBN = "";
var mPriorBookDescription = "";

$(function() {
    GetBook();    
});

$("#cmdCancel").on("click", function () {
    CancelBookCRUD();
});

$("#cmdSave").on("click", function () {
    SaveBook();    
});

function GetBook() {
    var mXML = "";
    if (+$('#hdnBookID').val() > 0) {
        $.ajax({
            url: 'BookReadHandler.ashx',
            type: 'POST',
            processData: false,
            contentType: 'text/xml',
            async: false,
            data: $('#hdnBookID').val(),
            success: function (result) {                
                mXML = result;
            },
            error: function () {
                alert("Error getting Book");
            }
        });
        DisplayBook(mXML);        
    }
}

function DisplayBook(pXML) {
    $("#hdnMyBookID").val($(pXML).find('bookid').text());
    $("#txtBookName").val($(pXML).find('bookname').text());
    $("#txtBookAuthor").val($(pXML).find('bookauthor').text());
    $("#txtBookISBN").val($(pXML).find('bookisbn').text());
    $("#txtBookDescription").val($(pXML).find('bookdescription').text());
    UpdateBookUI();
}

function SaveBook() {
    var mStrXML = "<book>";
    var mStatus = "";
    mStrXML += "<bookid>" + $("#hdnMyBookID").val() + "</bookid>";    
    mStrXML += "<bookname>" + $("#txtBookName").val() + "</bookname>";
    mStrXML += "<bookauthor>" + $("#txtBookAuthor").val() + "</bookauthor>";
    mStrXML += "<bookisbn>" + $("#txtBookISBN").val() + "</bookisbn>";
    mStrXML += "<bookdescription>" + $("#txtBookDescription").val() + "</bookdescription>";
    mStrXML += "</book>";

    $.ajax({
        url: 'BookEditionHandler.ashx',
        type: 'POST',
        processData: false,
        contentType: 'text/xml',
        async: false,
        data: mStrXML,
        success: function (result) {
            if (result != "failure") {
                mStatus = result;                
                DisplayBook(result);
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
    $("#widgetBooksEdit").hide();
}

function CancelBookCRUD() {    
    $("#hdnMyBookID").val(mPriorBookID);
    $("#txtBookName").val(mPriorBookName);
    $("#txtBookAuthor").val(mPriorBookAuthor);
    $("#txtBookISBN").val(mPriorBookISBN);
    $("#txtBookDescription").val(mPriorBookDescription);    
}

function UpdateBookUI() {    
    mPriorBookID = $("#hdnMyBookID").val();
    mPriorBookName = $("#txtBookName").val();
    mPriorBookAuthor = $("#txtBookAuthor").val();
    mPriorBookISBN = $("#txtBookISBN").val();
    mPriorBookDescription = $("#txtBookDescription").val(); 
}

function DisplayMessage(pStatus) {
    if (pStatus == "failure" || pStatus == "") {
        alert("There was an Error, the current book was not saved");
    } else {
        alert("The current book was successfully saved");
    }
}