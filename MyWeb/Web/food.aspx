<%@ Page Title="吃货福利" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="food.aspx.cs" Inherits="Web.food" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#ms-diet").addClass("current-menu-item");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="min-height: 700px; background-color: #f4f6f8;width:100%; background:url(../images/2015052211012624.jpg) no-repeat; background-size:cover">
        <div class="width_725 mid clearfix" id="da-thumbs" >
            <%--<%=HtmlList %>--%>
            <%-- <div class="bodbox">
                <h1><%=YZ.Common.HtmlHelper.ReplaceHtmlTag(m.a_Title,24) %></h1>
                <div ><%=YZ.Common.HtmlHelper.ReplaceHtmlTag(m.a_Content,110) %></div>
            </div> --%>
        </div>

        <%--<br />
        <div class=" width_100per ">
            <div id="h-more" class="mid" style="width: 80px;"><%=HtmlMore %></div>
        </div>--%>
        <br />
        <input type="hidden" id="hindex" value="0" />
    </div>
    <script src="js/jquery.hoverdir.js"></script>
    <script>
        var stop = true;
        $(function () {
            //加载
            load();
            //样式
            $('#da-thumbs > a').hoverdir();
            //点击手动加载
         
            $(window).scroll(function () {
                totalHeight = parseFloat($(window).height()) + parseFloat($(window).scrollTop());

                if ($(document).height() <= totalHeight) {
                    load();
                }
            }); 
        });
        function load() {
            var index = parseInt($('#hindex').val());

            if (stop == true && $('#h-more').val() == undefined) {

                stop = false;
                $.post("food.aspx?index=" + index + "&IsLoad=true", function (data) {
                    //$('#h-more').empty();
                    if (data != undefined) {

                        $('#da-thumbs').append(data);
                        $('#hindex').val(index + 1);
                        //添加样式
                        $('#da-thumbs > a').hoverdir();
                    } else { console.log('food.aspx data is undefined!'); }
                    stop = true;
                });
            } else { }
        }

    </script>
</asp:Content>
