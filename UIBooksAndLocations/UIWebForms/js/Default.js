$(function () {
    GetBooksAndLocations();
});

function GetBooksAndLocations() {
    var mXML = "";
    $.ajax({
        url: 'DefaultReadHandler.ashx',
        type: 'GET',
        contentType: 'text/xml',
        async: false,
        success: function (data) {
            mXML = data;
        },
        error: function () {
            alert("Error getting Books and Locations' list");
        }
    });
    displayBooksAndLocations(mXML);
}

function displayBooksAndLocations(pXML) {
    var mTable = $('<table></table>').attr({ id: "tablebooks" }).appendTo("#bodydefaultdetail");
    var mHeader = $('<thead></thead>').appendTo(mTable);
    var mHeaderRow = $('<tr></tr>').appendTo(mHeader);
    var mHeaderColumn = $("<td></td>").text("BOOKS").attr({ width: '700px' }).appendTo(mHeaderRow);
    var mHeaderRow = $('<tr></tr>').appendTo(mHeader);
    var mHeaderColumn = $("<td></td>").text("Update").attr({ width: '110px' }).appendTo(mHeaderRow);
    var mHeaderColumn = $("<td></td>").text("Name").attr({ width: '110px' }).appendTo(mHeaderRow);
    var mHeaderColumn = $("<td></td>").text("Author").attr({ width: '110px' }).appendTo(mHeaderRow);
    var mHeaderColumn = $("<td></td>").text("ISBN").attr({ width: '115px' }).appendTo(mHeaderRow);
    var mHeaderColumn = $("<td></td>").text("Description").attr({ width: '300px' }).appendTo(mHeaderRow);
    var mBody = $('<tbody></tbody>').appendTo(mTable);

    $(pXML).find("book").each(function () {
        var mBodyRow = $('<tr></tr>').appendTo(mBody);
        var mBodyColumn = $("<td></td>").attr({ width: '30px' });        
        $(mBodyColumn).attr("class", "cell_properties").appendTo(mBody);
        var mBodyColumnInner = $("<td style='padding:0px'></td>").appendTo(mBodyColumn);
        var mButton1 = $("<input type='button' value='[X]' onclick='DeleteBook(this)'>")
                            .attr({"id": "DeleteBookID_" + $(this).find('bookid:first')
                            .text(), "class": "edition_button"});
        $(mButton1).appendTo(mBodyColumnInner);
        var mBodyColumnInner = $("<td style='padding:0px'></td>").appendTo(mBodyColumn);
        var mButton2 = $("<input type='button' value='Edit' onclick='LoadBook(this)'>")
                            .attr({"id": "BookID_" + $(this).find('bookid:first')
                            .text(), "class": "edition_button"});
        $(mButton2).appendTo(mBodyColumnInner);        
        var mBodyColumn = $("<td></td>").text($(this).find('bookname:first').text())
                            .attr({width: '200px', "id": "BookNameNbr_" + $(this).find('bookid:first').text()});
        $(mBodyColumn).attr("class","cell_properties").appendTo(mBody);
        var mBodyColumn = $("<td></td>").text($(this).find('bookauthor:first').text()).attr({ width: '200px' });
        $(mBodyColumn).attr("class","cell_properties").appendTo(mBody);
        var mBodyColumn = $("<td></td>").text($(this).find('bookisbn:first').text()).attr({ width: '200px' });
        $(mBodyColumn).attr("class","cell_properties").appendTo(mBody);
        var mBodyColumn = $("<td></td>").text($(this).find('bookdescription:first').text()).attr({ width: '600px' });
        $(mBodyColumn).attr("class", "cell_properties").appendTo(mBody);
    });
    $("<br id='spacer'/>").appendTo("#bodydefaultdetail");
    var mTable = $('<table></table>').attr({ id: "tablelocations" }).appendTo("#bodydefaultdetail");
    var mHeader = $('<thead></thead>').appendTo(mTable);
    var mHeaderRow = $('<tr></tr>').appendTo(mHeader);
    var mHeaderColumn = $("<td></td>").text("LOCATIONS").attr({ width: '700px' }).appendTo(mHeaderRow);
    var mHeaderRow = $('<tr></tr>').appendTo(mHeader);
    var mHeaderColumn = $("<td></td>").text("Update").attr({ width : '110px' }).appendTo(mHeaderRow);
    var mHeaderColumn = $("<td></td>").text("Name").attr({ width : '110px' }).appendTo(mHeaderRow);
    var mHeaderColumn = $("<td></td>").text("Description").attr({ width : '110px' }).appendTo(mHeaderRow);
    var mHeaderColumn = $("<td></td>").text("Address").attr({ width : '415px' }).appendTo(mHeaderRow);
    var mBody = $('<tbody></tbody>').appendTo(mTable);

    $(pXML).find("location").each(function () {
        var mBodyRow = $('<tr></tr>').appendTo(mBody);
        var mBodyColumn = $("<td></td>").attr({ width: '30px' });
        $(mBodyColumn).attr("class", "cell_properties").appendTo(mBody);
        var mBodyColumnInner = $("<td style='padding:0px'></td>").appendTo(mBodyColumn);
        var mButton1 = $("<input type='button' value='[X]' onclick='DeleteLocation(this)'>")
                            .attr({
                                "id": "DeleteLocationID_" + $(this).find('locationid:first')
                                .text(), "class": "edition_button"
                            });
        $(mButton1).appendTo(mBodyColumnInner);
        var mBodyColumnInner = $("<td style='padding:0px'></td>").appendTo(mBodyColumn);
        var mButton2 = $("<input type='button' value='Edit' onclick='LoadLocation(this)'>")
                            .attr({
                                "id": "LocationID_" + $(this).find('locationid:first')
                                .text(), "class": "edition_button"
                            });
        $(mButton2).appendTo(mBodyColumnInner);
        var mBodyColumn = $("<td></td>").text($(this).find('locationname:first').text())
                            .attr({ width: '200px', "id": "LocationNameNbr_" + $(this).find('locationid:first').text() });
        $(mBodyColumn).attr("class", "cell_properties").appendTo(mBody);
        var mBodyColumn = $("<td></td>").text($(this).find('locationdescription:first').text()).attr({ width : '200px' });
        $(mBodyColumn).attr("class", "cell_properties").appendTo(mBody);
        var mBodyColumn = $("<td></td>").text($(this).find('locationaddress:first').text()).attr({ width : '800px' });
        $(mBodyColumn).attr("class", "cell_properties").appendTo(mBody);
    });
}

function LoadBook(pButton) {
    var mID = $(pButton).attr('id');
    mID = mID.replace('BookID_', '');
    $(parent.document).find('#hdnBookID').val(mID);
    $(parent.document).find('#bodywidgetBooksEdit').load('BooksEdit.html');
    $(parent.document).find('#widgetBooksEdit').show();
}

function DeleteBook(pButton) {
    var mID = $(pButton).attr('id');
    mID = mID.replace('DeleteBookID_', '');    
    if(confirm("Do you want to erase the Book: '" + $("#BookNameNbr_" + mID).text() + "'?")) {
        mResult = "failure";
        $.ajax({
            url: 'BookDeletionHandler.ashx',
            type: 'POST',
            processData: false,
            contentType: 'text/xml',
            async: false,
            data: mID,
            success: function (result) {
                mResult = result;
            }
        });
        if (mResult != "failure" && mResult != "") {
            CleanForm();
            GetBooksAndLocations();
            alert("The book you selected was successfully deleted"); 
        } else {
            alert("There was a problem deleting the current book, it wasn't deleted succesfully");
        }
    }
}

function LoadLocation(pButton) {
    var mID = $(pButton).attr('id');
    mID = mID.replace('LocationID_', '');
    $(parent.document).find('#hdnLocationID').val(mID);
    $(parent.document).find('#bodywidgetLocationsEdit').load('LocationsEdit.html');
    $(parent.document).find('#widgetLocationsEdit').show();
}

function DeleteLocation(pButton) {
    var mID = $(pButton).attr('id');
    mID = mID.replace('DeleteLocationID_', '');
    if(confirm("Do you want to erase the Location: '" + $("#LocationNameNbr_" + mID).text() + "'?")){    
        mResult = "failure";        
        $.ajax({
            url: 'LocationDeletionHandler.ashx',
            type: 'POST',
            processData: false,
            contentType: 'text/xml',
            async: false,
            data: mID,
            success: function (result) {
                mResult = result;
            }
        });
        if (mResult != "failure" && mResult != "") {
            CleanForm();
            GetBooksAndLocations();
            alert("The location you selected was successfully deleted"); 
        } else {
            alert("There was a problem deleting the current location, it wasn't deleted succesfully");
        }    
    }
}

function CleanForm() {
    $("#tablebooks").remove();
    $("#tablelocations").remove();
    $("#spacer").remove();    
}