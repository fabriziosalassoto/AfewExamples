$(document).ready(function () {
    $('.BooksAndLocationsWidget').draggable();
    $('.MapWidget').draggable();
});

function FocusMe(pObject) {
    /*var x = +$('#MyZindex').val();
    x++;
    $(pObject).attr('z-index', x);
    $('widgetDefault').val(x.toString());*/
}

function closewidget(pObject) {
    $(pObject).parent().hide();
}

function DisplayWidget(pWhichOne) {
    switch (pWhichOne.innerHTML) {
        case "Home": $('#bodywidgetDefault').load('Default.html');
            $('#widgetDefault').show();
            break;
        case "Books": $('#hdnBookID').val("-1");
            $('#bodywidgetBooksEdit').load('BooksEdit.html');
            $('#widgetBooksEdit').show();
            break;
        case "Locations": $('#hdnLocationID').val("-1");
            $('#bodywidgetLocationsEdit').load('LocationsEdit.html');
            $('#widgetLocationsEdit').show();
            break;

    }
}