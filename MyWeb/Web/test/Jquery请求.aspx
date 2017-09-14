<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jquery请求.aspx.cs" Inherits="YZ.Web.Asp.test.Jquery请求" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-1.8.3.min.js"></script>
      <script>
        $(document).ready(function () {
            //var url2 = 'http://v.juhe.cn/weather/index?callback=?';
            //$.getJSON(url2, {
            //    'cityname': '北京',
            //    'dtype': 'jsonp',
            //    'key': 'xxxx',
            //    '_': new Date().getTime()
            //}, function (data) {
            //    if (data && data.resultcode == '200') {
            //        console.log(data.result.today);
            //    }
            //});
            $.ajax({
                url: 'http://v.juhe.cn/weather/index?callback=?',
                'cityname': '北京',
                'dtype': 'jsonp',
                'key': 'xxxx',
                type: 'GET',
                dataType: 'JSONP',
                '_': new Date().getTime(),

                success: function (data) {
                    $('#divtest').append("Name: " + data);
                }

            });
            //$.ajax({
            //    url: "http://v.juhe.cn/weather/index",
            //    type: 'GET',
            //    dataType: 'JSONP',
            //    success: function (data) {
            //        $('#divtest').append("Name: " + data);
            //    }
            //});

            $("#btn1").click(function () {
                $('#test').load('files/111.txt', function (responseTxt, statusTxt, xhr) {
                    if (statusTxt == "success")
                        alert("External content loaded successfully!");
                    if (statusTxt == "error")
                        alert("Error: " + xhr.status + ": " + xhr.statusText);
                });
            })
            $("#btn2").click(function () {
                $.get("WebForm1.aspx",
                    {
                        Action: "get", Name: "lulu"
                    },
                    function (data, textStatus) {

                    //返回的 data 可以是 xmlDoc, jsonObj, html, text, 等等.
                    this; // 在这里this指向的是Ajax请求的选项配置信息，请参考下图
                    alert(data);

                    //alert(textStatus);//请求状态：success，error等等。

                    //当然这里捕捉不到error，因为error的时候根本不会运行该回调函数
                    //alert(this);
                });
            })
        })




        //html中ajax用法
        //后台处理参数，生成结果数据：
        //data['plan_list'] = plans//data是字典  
        //req.send(json.dumps(data).encode('utf-8'))//转成json数据  
        //前台传送参数，接收结果
        //$.ajax({
        //    type: 'post',
        //    url: "/myTrac/projectmanage/project/plan_project",
        //    data: {
        //        "new_plan_flag": '1',
        //        "plan_name": $('#plan_name_new').val(),
        //        "project_name": project_name,
        //        "stage_list": stage_list.join('[flag]'),
        //        __FORM_TOKEN: $('#trac_form_token').val()
        //    },
        //    async: false,
        //    success: function (data) {
        //        data = eval("(" + data + ")");//解析json数据  
        //        if (data.status = "Success") {
        //            alert("已保存")
        //        } else {
        //            alert('Create Fail,Please Check !')
        //        }
        //    }
        //});
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div>
            <h3 id="test">请点击下面的按钮，通过 jQuery AJAX 改变这段文本。</h3>
            <button id="btn1" type="button">获得外部的内容</button>
            <div id="divtest"></div>
             <button id="btn2" type="button">btn2Get</button>

            <br />

            <asp:TextBox ID="textbox" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <h3 id="isa" runat="server"> ssss</h3>
        </div>
    </div>
    </form>
</body>
</html>
