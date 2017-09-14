<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.index" %>

<!DOCTYPE html>
<%--应用程序缓存 manifest 文件的建议的文件扩展名是：".appcache"。
请注意，manifest 文件需要配置正确的 MIME-type，即 "text/cache-manifest"。必须在 web 服务器上进行配置。--%>
<html xmlns="http://www.w3.org/1999/xhtml" manifest="cache/cache.manifest">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        div {
            border: 1px dashed #808080;
            margin: 5px 5px;
        }

        .bor {
            box-shadow: 0 0 5px #e3e3e3;
            height: 350px;
            border: 1px solid #000;
        }

        .fl_left {
            float: left;
        }

        .fl_right {
            float: right;
        }

        .clear {
            clear: both;
        }
    </style>
</head>
<body>

    <div id="divVideo" class="bor fl_left" style="width: 450px;">
        <a>视频</a><br />
        <video id="myVideo" width="400" height="284" <%--autoplay="autoplay"--%>>
            Your browser does not support the video tag.
        </video>
        <br />
        <input type="button" onclick="playPause()" value="播放/暂停" />
        <input type="button" onclick="makeBig()" value="大" />
        <input type="button" onclick="makeNormal()" value="中" />
        <input type="button" onclick="makeSmall()" value="小" />
    </div>
    <div id="divImage" class="bor fl_left" style="width: 425px;">
        <a>图片拖放</a><br />
        <div id="div1" ondrop="drop(event)" ondragover="allowDrop(event)" style="height: 200px; width: 200px; float: left">
            <img id="dragimg" src="img/1.png" draggable="true" ondragstart="drag(event)" />
        </div>
        <div id="div2" ondrop="drop(event)" ondragover="allowDrop(event)" style="height: 200px; width: 200px; float: right"></div>

    </div>
    <div id="divCanvas" class="bor fl_right" style="width: 420px;">
        <a>canvas</a><br />
        <canvas id="myCanvas" style="border: 1px dashed #808080;" width="200" height="200" title="pic">Your browser does not support the HTML5 canvas tag.
        </canvas>
        <canvas id="videoCanvas" style="border: 1px dashed #808080;" width="200" height="200" title="video">Your browser does not support the HTML5 canvas tag.
        </canvas>
        <a id="axy"></a>
        <svg xmlns="http://www.w3.org/2000/svg" version="1.1" height="190">
            <polygon points="100,10 40,180 190,60 10,60 160,180"
                style="fill: red; stroke: blue; stroke-width: 3; fill-rule: evenodd;" />
        </svg>
        <div id="myCanvasContainer">
            <canvas id="labCan" style="width: 200px; height: 150px;"></canvas>
            <div id="tags">
                <%-- <ul>
                    <li><a href="http://www.scutephp.com" target="_blank">PHP建站门户</a></li>
                    <li><a href="/fish">Fish</a></li>
                    <li><a href="/chips">Chips</a></li>
                    <li><a href="/salt">Salt</a></li>
                    <li><a href="/vinegar">Vinegar</a></li>
                </ul>--%>
                <ul>
                    <li><a href="#" onclick="return f('another')">Another</a></li>
                    <li><a href="#" onclick="return f('example')">Example</a></li>
                    <li><a href="#" onclick="return f('different')">Different</a></li>
                    <li><a href="#" onclick="return f('options')">Options</a></li>
                    <li><a href="#" onclick="return f('another')">Another</a></li>
                    <li><a href="#" onclick="return f('example')">Example</a></li>
                    <li><a href="#" onclick="return f('different')">Different</a></li>
                    <li><a href="#" onclick="return f('options')">Options</a></li>
                    <li><a href="#" onclick="return f('another')">Another</a></li>
                    <li><a href="#" onclick="return f('example')">Example</a></li>
                    <li><a href="#" onclick="return f('different')">Different</a></li>
                    <li><a href="#" onclick="return f('options')">Options</a></li>
                </ul>
            </div>
        </div>
    </div>
    <p class="clear" onclick="aclick()">跳转localStorage存储</p>
    <p>
        <output id="output"></output>
    </p>

</body>
<%--<script src="js/jquery-1.8.1.min.js"></script>--%>
<script src="js/jquery-1.10.0.min.js"></script>
<script src="js/index.js"></script>
<%--<script src="js/jquery.tagcanvas.js"></script>--%>  //http://www.scutephp.com/topic-id117.html
<script src="js/tagcanvas.min.js"></script>
//http://www.goat1000.com/tagcanvas.php
<script>

    //参考资料
    //http://www.goat1000.com/tagcanvas.php
    window.onload = function () {
        TagCanvas.textFont = 'Trebuchet MS, Helvetica, sans-serif';
        TagCanvas.textColour = '#00f';
        TagCanvas.textHeight = 25;
        //TagCanvas.outlineMethod = 'block';
        TagCanvas.outlineColour = '#FFD700';
        TagCanvas.maxSpeed = 0.1;
        TagCanvas.minBrightness = 0.2;
        TagCanvas.depth = 0.92;
        TagCanvas.pulsateTo = 0.6;
        TagCanvas.initial = [0.1, -0.1];
        TagCanvas.decel = 0.98;
        TagCanvas.reverse = true;
        TagCanvas.hideTags = true;
        TagCanvas.shadow = '#ccf';
        TagCanvas.shadowBlur = 3;
        TagCanvas.weight = false;
        TagCanvas.imageScale = null;
        TagCanvas.fadeIn = 1000;
        TagCanvas.clickToFront = 600;
        try {
            TagCanvas.Start('labCan', 'tags');
            //TagCanvas.Start('smallCanvas', 'moreTags', oopts);
            //f('options');
        } catch (e) {
            //document.getElementById('cmsg').style.display = 'none';
            //document.getElementsByTagName('canvas')[0].style.border = '0';
        }
    };
    function aclick() {
        localStorage.sendvalue = "长期保存";
        //sessionStorage.data = "当此有效，新开浏览器重新计数";
        window.location.href = " HtmlPage1.html";
    }
    function f(obj) {
        return obj;
    }
    //$(function () {

    //    //参考资料 
    //    //http://www.scutephp.com/topic-id117.html
    //    if (!$('#labCan').tagcanvas({
    //        textColour: '#000',
    //        textFont: "Helvetica, Arial,sans-serif",
    //        textHeight: 18,
    //        outlineColour: '#fff',
    //        reverse: true,
    //        depth: 0.9,
    //        maxSpeed: 0.05
    //    }, 'tags')) { $('#myCanvasContainer').hide(); }
    //})
</script>
</html>
