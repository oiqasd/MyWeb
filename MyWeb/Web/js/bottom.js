
//$(window.parent.document).find("#iframe").load(function () { 
//    var main = $(window.parent.document).find("#iframe");
//    var thisheight = $(document).height() + 30;
//    main.height(thisheight); 
//});

$(function () {
    $("#iframe").load(function () {
        var mainheight = $(this).contents().find("body").height() + 30;
        $(this).height(mainheight);
    });
});