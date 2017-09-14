// +++++++++++++++++++ video  start
var video = document.getElementById("myVideo");
var curr = 0; // 当前播放的视频
var vList = ['video/mov_bbb.ogg', 'video/4CD3_Compress.mp4']; // 初始化播放列表 

video.addEventListener('ended', play);
//video.onended = function () {
//    //alert("音频已播放完成");
//     play();
//};

function playPause() {
    if (video.paused)
        video.play();
    else
        video.pause();
}

function makeBig() {
    video.width = 450;
}
function makeNormal() {
    video.width = 380;
}
function makeSmall() {
    video.width = 300;
}
play();
function play() {
    video.src = vList[curr];
    curr++;
    if (curr >= vList.length)
        curr = 0;
}
// video end 


//++++++++++++++++++拖放 start
function allowDrop(ev) {
    ev.preventDefault();
}
function drag(ev) {

    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");

    ev.target.appendChild(document.getElementById(data));
}
//拖放 end
 
//+++++++++++++++++++++++Canvas++++++++++++++
$(document).ready(function ($) {
   
    var c = $('#myCanvas')[0];//document.getElementById("myCanvas"); //$('#myCanvas');

    var cxt = c.getContext('2d');
    //画布填充
    cxt.fillStyle = '#c3c3c3';
    cxt.fillRect(0, 0, 200, 200);
    //画线
    cxt.moveTo(10, 10);
    cxt.lineTo(150, 50);
    cxt.lineTo(10, 50);
    cxt.lineTo(10, 10);
    cxt.stroke();

    //渐变
    var ctg = cxt.createLinearGradient(0, 150, 175, 50)
    ctg.addColorStop(0, "#ff0000");
    ctg.addColorStop(1, "#00ff00");
    cxt.fillStyle=ctg;
    cxt.fillRect(0, 52, 175, 50);
    //cxt.fillStyle = ctg;
    cxt.arc(180, 180, 15, 0, Math.PI * 2, true);
    cxt.fill();

    //填充图片
    var img = new Image();
    img.src = 'img/1.png';
    cxt.drawImage(img, 0, 100 ,150,150);

    $("#myCanvas").mousemove(function (event) {

        $('#axy').text("坐标：(" + event.clientX + "," + event.clientY + ")");
    })

    $('#myCanvas').mouseout(function () {
        $('#axy').text('');
    })
    //if (!$("#myCanvas").tagcanvas({
    //         textColour: 'black',
    //         outlineThickness: 1,
    //         maxSpeed: 0.03,
    //         depth: 0.75
    //     })) {
    //          //TagCanvas failed to load

    //     }



    //视频
    var v = $('#myVideo')[0];
    var vxt = $('#videoCanvas')[0].getContext('2d');

    v.addEventListener('play', function () {
 
        var i = window.setInterval(function () {
            vxt.drawImage(v, 0, 0, 200, 200)
        }, 20)
    }, false)
    v.addEventListener('pause', function () { window.clearInterval(i); }, false);
    v.addEventListener('ended', function () { clearInterval(i); }, false);

})


//++++++++++++++++++++Web Worker
var w;
function startWorker() {
    if (typeof (Worker) !== "undefined") {
        if (typeof (w) == "undefined") {
          
            w = new Worker("js/demo_workers.js");
        }
        w.onmessage = function (event) {
            document.getElementById("result").innerHTML = event.data;
        };
    }
    else {
        document.getElementById("result").innerHTML = "Sorry, your browser does not support Web Workers...";
    }
}
//startWorker();
function stopWorker() {
    w.terminate();
}