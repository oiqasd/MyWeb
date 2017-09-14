<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleTypeList.aspx.cs" Inherits="YZ.Web.Asp.Admins.ArticleTypeList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="../css/bootstrap5152.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/msgbox/jquery.similar.msgbox.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="width: 98%">
        <div class="shadow"></div>
        <a href="../index.aspx" class="btn btn-link">首页</a>
        <div><a href="#" onclick="add()" class="btn btn-success">添加</a></div>
        <div>
            <table class="table" style="width: 800px">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>名称</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <%if (articleType != null)
                      {
                          foreach (var m in articleType)
                          {
                    %>
                    <tr>
                        <td><%=m.id %></td>
                        <td><%=m.a_t_ArticleCategory %></td>
                        <td><%if (m.a_t_Flag == 0)
                              { %><a onclick="btnOk('star',<%=m.id %>)" class="btn btn-link" style="color:green" href="#">启用</a><%}
                              else
                              {%><a class="btn btn-link" style="color:red" href="#" onclick="btnOk('ban',<%=m.id %>)">禁用</a><%} %>
                            <a data-id="<%=m.id %>" data-name="<%=m.a_t_ArticleCategory %>" class="btn btn-link" href="#">修改</a>
                            <a  onclick="btnOk('del',<%=m.id %>)" class="btn btn-link" href="#">删除</a>
                        </td>
                    </tr>
                    <%}
                      }%>
                </tbody>
            </table>

        </div>
    </form>
    <div id="dex" class="ex display_no">
        <div class="sbox">
            <div style="float: right; margin: 10px 10px 20px;">
                <p><a class="btn btn-mini" onclick="closed()" href="#">×</a></p>
            </div>
            <div style="margin: 40px 0 0 20px; position: absolute">
                <p>
                    类别名称：
                <input type="text" id="txtType" />
                </p>
                <p>
                    <a class="btn btn-success" onclick="btnOk('add',0)">添加</a>&nbsp;&nbsp;
                    <a class="btn" onclick="closed()">取消</a>
                </p>
            </div>
        </div>
    </div>
</body>

</html>
<script>
    function add() {
        $('.shadow').css('display', 'block');
        $('#dex').addClass('display_sh');
    }
    function closed() {
        $('#txtType').val('');
        $('.shadow').css('display', 'none');
        $('#dex').removeClass('display_sh');
    }
    function btnOk(type, id) {
        val = $.trim($('#txtType').val());
        if (type == 'add' && val.length <= 0) {
            $.MsgBox.Alert('提示', '类别名称不能空！');
            return;
        }
        $.post('ArticleTypeList.aspx', { type: type, tid: id, name: val }, function (result) {
            if (result != null && result != undefined) {
                var jsonObj;
                try {
                    jsonObj = JSON.parse(result);
                } catch (e) {
                    jsonObj = eval("(" + result + ")");
                }
                $.MsgBox.Alert('提示', jsonObj.Message, callback);
            }
        });
        //$.MsgBox.Confirm("提示", '是否确定禁用？', function () {
        //    $.post('ArticleTypeList.aspx', {name:val}, function () {
        //    })
        //})
    }

    function callback() {
        window.location.reload();
    }
</script>
