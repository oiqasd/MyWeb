<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcComment.ascx.cs" Inherits="YZ.Web.Asp.UcComment" %>
<link href="css/article.css" rel="stylesheet" />

<div style="height: 20px;"></div>

<div class="mod-comment">

    <h3>点评区</h3>
    <div class="comment-box">
        <div class="cmtbox">
            <p class="atv">
                <img src="images/face/df3654.jpg" class="h-img" />
            </p>
            <div>
                <textarea id="txtComment" placeholder="随便说点什么吧" maxlength="250"></textarea>
            </div>
            <p class="btns3">
                <span class="checknum">还可以输入<em>250</em>个字</span>
                <a href="javascript:;" data-aid="<%=articleid %>" class="btn btn-info width_50">评 论</a>
            </p>
        </div>
    </div>
    <%--评论列表--%>
    <div class="comments">
        <div class="cmtlist">
            <asp:PlaceHolder runat="server" ID="place"></asp:PlaceHolder>

        </div>
    </div>
</div>
<%--点评区结束--%>

<script>
    $(function () {

        //获取IP
        var ip; var isp; var browser; var os;
        //http://ip.taobao.com/service/getIpInfo.php?ip=117.89.35.58
        //jQuery(function ($) {
        var url = 'http://chaxun.1616.net/s.php?type=ip&output=json&callback=?&_=' + Math.random();
        $.getJSON(url, function (data) {
            ip = data.Ip;
            isp = data.Isp;
            browser = data.Browser;
            os = data.OS;
            //alert(ip + isp + browser + os);

        });
        uu = 'http://ip.taobao.com/service/getIpInfo.php';
        $.get(uu, { ip: '117.89.35.58' }, function (data1) {

            //alert(JSON.stringify(data1));
        });
        //});

        //$('.replay-btn').click(function () {
        //    //$(".replay-box").toggle(1000, function () {/*$(".replay-box").toggle();*/ });
        //    $('.replay-box').slideToggle(1000);
        //})

        //评论
        $('.btn-info').click(function () {
            cmt = $.trim($('#txtComment').val());
            if (cmt.length <= 0) {
                $.MsgBox.Alert('提示', '评论不能为空');
                return;
            }
            if (cmt.length > 250) {
                $.MsgBox.Alert('提示', '评论内容过长');
                return;
            }
            aid = $(this).attr('data-aid');
            if ($.trim(aid).length <= 0) return;
            parameter = {
                action: 'comment',
                type: '0',
                aid: aid,
                msg: cmt,
                ip: ip,
                isp: isp,
                browser: browser,
                os: os
            };
            $.post('Form_Action/WebForm_Action.aspx', parameter, function (data) {
                if (JSON.parse(data).IsSuccess)
                    window.location.reload();
                else
                    $.MsgBox.Alert('提示', '评论失败');
            });
        })
        //评论框输入字符串
        $('#txtComment').keyup(function () {
            var value = $(this).val().length;
            $(this).parent().next().find('em').text(250 - value);
        })

        //回复
        $('.replay-btn').each(function () {
            $(this).click(function () {
                $(this).parent('.replay-bar').next().slideToggle(1000);
            })
        })
        //提示输入字符数
        $('.cmtlist').find('textarea').each(function () {
            $(this).keyup(function () {
                var value = $(this).val().length;
                $(this).parent().next().find('em').text(250 - value);
                //var ss = $(this).parent('.replay-box').find('span:eq(0)').text(); 
            })
        })
        //点击评论回复按钮
        $('.cmtlist').find('.btn-success').each(function () {
            $(this).click(function () {
                var value = $.trim($(this).parent().parent().find('textarea').val());

                if (value.length <= 0) {
                    $.MsgBox.Alert('提示', '评论不能为空');
                    return;
                }

                if (value.length > 250) {
                    $.MsgBox.Alert('提示', '评论内容过长');
                    return;
                }
                pid = $(this).attr('data-id');
                if ($.trim(pid).length <= 0) return;
                parameter = {
                    action: 'comment',
                    type: '1',
                    aid: $('.btn-info').attr('data-aid'),
                    pid: pid,
                    msg: value,
                    ip: ip,
                    isp: isp,
                    browser: browser,
                    os: os
                };
                $.post('Form_Action/WebForm_Action.aspx', parameter, function (data) {
                    if (JSON.parse(data).IsSuccess)
                        window.location.reload();
                    else
                        $.MsgBox.Alert('提示', '评论失败');
                });
            })
        })

        $('.can-cool img').each(function () {
            $(this).click(function () {

                if ($(this).attr('src').indexOf('cool_b057f8a') > 0)
                    return;               

                var num = Number($(this).parent().attr('data-num')) + 1;
                $(this).parent().find('.num-cool').text(num);
                $(this).parent().attr('data-num', num);

                cid = $(this).parent().attr('data-cid');
                if (isNaN(cid))
                    return;
                parameter = {
                    action: 'numcool',
                    cid: cid
                };
                $(this).attr('src', 'images/icon-can-cool_b057f8a.png');
                $.post('Form_Action/WebForm_Action.aspx', parameter, function (data) {
                    if (JSON.parse(data).IsSuccess) {
                        
                    }
                })
            })
        })
    })
</script>
