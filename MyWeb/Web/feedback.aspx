<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="YZ.Web.Asp.feedback" %>

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

        @media screen and (min-width:1201px) {

            #myEditor {
                width: 1000px;
                height: 610px;
            }
        }

        @media screen and (max-width:1200px) {

            #myEditor {
                width: 900px;
                height: 540px;
            }
        }

        @media screen and (max-width:900px) {

            #myEditor {
                width: 500px;
                height: 320px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="container">
            <h3><span>反馈内容</span></h3>
            <textarea id="txtComment" style="width: 700px; height: 110px;" maxlength="250" onpropertychange="smsCount()" oninput="smsCount()"></textarea>
            <span id="spContent">还可以输入<em>250</em>个字</span>
            <div class="width_650 clearfix">
                <div class="width_300 float_left">
                    <h3><span>提交图片</span></h3>
                    <img id="cpic" style="width: 200px; height: 200px" data-key="" src="" />
                </div>
                <div class="width_300 float_right margin_top_10">
                    <%--<a class="btn btn-navbar">上传图片</a>--%>
                    <input type="file" onchange="previewImage(this)" style="width: 200px; height: 70px; opacity: 0; -moz-opacity: 0; -webkit-opacity: 0; cursor: pointer" />
                    <section style="width: 200px; height: 60px; margin-top: -70px; background: #134364; color: #fff; border-radius: 14px; text-align: center; line-height: 60px;">
                        <a style="color: #fff; font-size: 22px">上传图片</a>
                    </section>
                    <p>
                        <br />
                        <span class="color-a8">图片尽量清晰，图片大小不超过1M</span>
                    </p>
                </div>
            </div>
            <div class="clearfix">
                <section style="width: 200px; height: 60px; margin-top: -70px; background: #134364; color: #fff; border-radius: 14px; text-align: center; line-height: 60px;">
                    <a style="color: #fff; font-size: 22px">提交</a>
                </section>
            </div>
            <div>
                <h3><span>联系方式</span></h3>
                <input id="txtuser" type="text" placeholder="用户名" maxlength="10" class="width_100" />
                <input id="txtways" type="text" placeholder="电话、邮箱、QQ等" />
            </div>
        </div>
    </div>


    <script>

        ////提示输入字符数 
        //$("#txtComment").keyup(function () {
        //    var value = $(this).val().length;
        //    $(this).parent().find('em').text(250 - value);
        //    //var ss = $(this).parent('.replay-box').find('span:eq(0)').text(); 
        //})

        function smsCount() {
            var value = $('#txtComment').val().length;
            $('#txtComment').parent().find('em').text(250 - value);
        }
    </script>
</asp:Content>
