$(function () {
    try {
        if (window.console && window.console.log) {
            console.log("如果失去是苦，你还怕不怕付出\n如果坠落是苦，你还要不要幸福\n如果迷乱是苦，该开始还是结束\n");
            console.log("%c如果追求是苦，这是坚强还是执迷不悟", "color:red");
            console.log("%c心是万物之本，心正则一切皆正，心静则一切皆静，心善则一切皆善","color:red");
            
        }
    }
    catch (e)
    { }

    ///滚动
    //$(".scroll").click(function (event) {
    //    event.preventDefault();
    //    $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1200);
    //});

    
});

function DoubleRow(tableid) {
    var tab = document.getElementById(tableid);
    if (tab.localName == 'ul') {
        //ul
        for (var i = 0; i < tab.childElementCount; i++) {
            tab.children[i].style.backgroundColor = (i % 2 == 0) ? '#fff' : '#f4f4f4';
        }
    }
    else {
        //table
        for (var i = 0; i < tab.rows.length; i++) {
            tab.rows[i].style.backgroundColor = (i % 2 == 0) ? '#fff' : '#e5e5e5';
        }
    }

}