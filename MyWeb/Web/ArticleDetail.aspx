<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="ArticleDetail.aspx.cs" Inherits="YZ.Web.Asp.ArticleDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="js/umeditor/themes/default/css/umeditor.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="js/umeditor/third-party/jquery.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="js/umeditor/umeditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="js/umeditor/umeditor.min.js"></script>
    <script type="text/javascript" src="js/umeditor/lang/zh-cn/zh-cn.js"></script>
    <style>
        #myEditor {
            width: 200px;
            height: 120px;
        }

        #tab {
            width: 1010px;
            margin: 30px auto 30px auto;
        }

        @media screen and (min-width:1201px) {
            #tab {
                width: 1050px;
            }

            #myEditor {
                width: 1000px;
                height: 610px;
            }
        }

        @media screen and (max-width:1200px) {
            #tab {
                width: 900px;
            }

            #myEditor {
                width: 900px;
                height: 540px;
            }
        }

        @media screen and (max-width:900px) {
            #tab {
                width: 500px;
            }

            #myEditor {
                width: 500px;
                height: 320px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form onsubmit="return funSubmit();" style=" padding-top:10px;">
        <table id="tab" style="width: 1010px;">
            <thead>
                <tr>
                    <%--<th colspan="4">添加</th>--%>
                </tr>
            </thead>
            <tr>
                <td>标题：</td>
                <td>
                    <input type="text" name="txtTitle" id="txtTitle" placeholder="标题" required="" pattern="[A-Za-z0-9\u4e00-\u9fa5.@_-~#]{1,50}" title="请输入1-50位字母数字汉字" autocomplete="off" maxlength="50" value="<%=_article.a_Title %>" />
                    <span id="spTitle" style="color: red;"></span>
                </td>
                <td>类别：</td>
                <td>
                    <select id="selArticleTypes" name="selArticleTypes">
                        <%= NewsTypeList(_article.a_TypeId) %>
                        <%--<option value="0">-请选择-</option>
                        <option value="1">其他</option>--%>
                    </select>
                </td>
                <td></td>
            </tr>

            <tr>
                <td>关键字：</td>
                <td>
                    <input type="text" placeholder="关键字(用;分组)" pattern="[A-Za-z0-9\u4e00-\u9fa5;]{0,50}" title="最大50位字母数字汉字" autocomplete="off" name="txtKeyWord" id="txtKeyWord" maxlength="50" value="<%=_article.a_KeyWord %>" />
                </td>
                <td>来源：</td>
                <td>
                    <select id="selFrom">
                        <option value="原创" <%=(_article.a_From=="原创"?"selected=''":"") %>>原创</option>
                        <option value="转载" <%=(_article.a_From=="转载"?"selected=''":"") %>>转载</option>
                        <option value="网络" <%=(_article.a_From=="网络"?"selected=''":"") %>>网络</option>
                        <%if (_article.a_From != "原创" && _article.a_From != "转载" && _article.a_From != "网络")
                          {%>
                        <option value="0" data-msg="<%=_article.a_From %>" selected="">其他</option>
                        <% }
                          else
                          {%>
                        <option value="0" data-msg="">其他</option>
                        <% }%>
                    </select>
                    <input name="txtForm" id="txtForm" type="text" placeholder="其它(请注明)" required="" pattern="[A-Za-z0-9\u4e00-\u9fa5.@_-~#]{0,20}" title="最大20位字母数字汉字" autocomplete="off" maxlength="20" value="<%=_article.a_From %>" />
                    <span id="spFrom" style="color: red;"></span>
                </td>
            </tr>

            <tr>
                <td>置顶：</td>
                <td>
                    <%if (_article.a_IsTop ?? true)
                      {
                    %>
                    <input type="checkbox" id="ckTop" checked="checked" />
                    <%}
                      else
                      { 
                    %>
                    <input type="checkbox" id="ckTop" />
                    <%}%>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <script type="text/plain" id="myEditor"><%=_article.a_Content %></script>
                    <span id="spContent" style="color: red;"></span>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center;">
                    <input type="submit" id="btnCommit" value="提 交" />&nbsp;
                    <input type="button" id="btnCancle" value="取 消" />
                    <input type="hidden" id="hidArticleId" value="<%=_article.id %>" />
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        var um = UM.getEditor('myEditor');

        $('#txtKeyWord').blur(function () {
            $(this).val($(this).val().replace(/;{2,}/g, ';'));
            $(this).val($(this).val().replace(/\s|(^;)|(;$)/g, ''));

        });
        $('#txtTitle').blur(function () {
            $(this).val($(this).val().replace(/\s/g, ''));
        });
        $('#txtForm').blur(function () {

            $(this).val($(this).val().replace(/\s/g, ''));
            value = $(this).val();
            $("#selFrom").val(value);
            if ($("#selFrom").val() == null || $("#selFrom").val() == '')
                $("#selFrom").val(0);
        });
        $('#selFrom').change(function () {
            if ($(this).val() == '0') {
                var m = $(this).find("option:selected").data("msg");
                $('#txtForm').val(m);
                //$('#txtForm').removeAttr('disabled');
            }
            else {
                //$('#txtForm').attr('disabled', '');
                $('#txtForm').val($(this).val());
            }
        })
        function funSubmit() {
            $('#spTitle').html('');
            $('#spFrom').html('');
            $('#spContent').html('');

            //var handleUrl = window.location.href;
            var handleUrl = "ArticleDetail.aspx";
            var msg = "";
            var title = $('#txtTitle').val();
            var type = $('#selArticleTypes').val();
            var keyWord = $('#txtKeyWord').val();
            var txtForm = $('#txtForm').val();
            var isTop = $('#ckTop').is(":checked");
            var content = um.getContent();

            var flg = false;
            if (title == '') {
                $('#spTitle').html('*标题不能为空');
                flg = true;
            }
            if (txtForm == '') {
                $('#spFrom').html('*文章来源不能为空');
                flg = true;
            }
            if (content == '') {
                $('#spContent').html('*文章内容不能为空');
                flg = true;
            }
            if (flg) {
                return false;
            }
            if (keyWord == '')
                msg = '关键字为空，'
            msg += '是否确定保存？';
            $.MsgBox.Confirm("提示", msg,function () {$.post(handleUrl,{
                     actId: $('#hidArticleId').val(),
                     action: 'add',
                     title: title,
                     type: type,
                     keyWord: keyWord,
                     txtForm: txtForm,
                     isTop: isTop,
                     content: content
                 },
                 //回调函数
                 function (result) {

                     if (result.Message != undefined) {
                         if (result.IsSuccess) {
                             $.MsgBox.Alert('提示', result.Message, callback);
                         }
                         else {
                             $.MsgBox.Alert('提示', result.Message);
                         }
                     }
                     else {
                         $.MsgBox.Alert('提示', result.Message);
                         //var substr = cutstr(result, "BUI.Message.Alert", "function");
                         //substr = substr.replace("'", "").replace("'", "").replace("(", "").replace(",", "");
                         //if (substr == "")
                         //    BUI.Message.Alert("操作失败，请联系技术人员", 'warning');
                         //else
                         //{
                         //    if (substr.indexOf("请重新登录") > 0) {
                         //        BUI.Message.Alert(substr, function () {
                         //            top.location.href = "@Url.Action("Login","User")";
                         //        }, 'warning');
                         //    }
                         //    else {
                         //        BUI.Message.Alert(substr, 'warning');
                         //    }
                         //}
                     }

                 },
                 //返回类型
                 "json");
                });
            return false;
        }

        function callback() {
            self.location = document.referrer;
        }

        $('#btnCancle').click(function () {
            window.history.go(-1);
        })
    </script>
</asp:Content>


