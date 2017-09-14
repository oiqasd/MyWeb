<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcSubComment.ascx.cs" Inherits="YZ.Web.Asp.UcSubComment" %>

<div class="li">
    <div class="atv">
        <img src="images/face/df3654.jpg" class="h-img"/>
        <div style="margin-left:auto;text-align:center;"><%=data_index %>#</div>
    </div>
    <div class="cnt">
        <div class="user"><span class="nm"><%=data_uname %></span>&nbsp;&nbsp;<span class="tm"><%=data_date %></span></div>
        <div class="txt"><%=data_msg %></div>
    </div>
    <div class="replay">
        <div class="replay-bar">
            <a class="cool-btn can-cool"  data-num="<%=data_cool %>" data-cid="<%=data_id %>"><%--href="javascript:;"--%>
                <%--<input type="hidden" id="2562581" />--%> 
                <img src="../images/icon-can-cool_b057f8b.png" />
                <i class="num-cool"><%= data_cool!="0"?data_cool:"" %></i>
                <%--<i class="num-add">+1</i>--%>
            </a>
           <%-- <a class="replay-btn" href="javascript:;">回复</a>--%>
        </div>
        <%--回复评论--%>
        <div class="replay-box" style="display: none;">
            <i class="replay-textarea">
                <textarea ></textarea>
            </i>
            <div class="replay-status-bar clearfix">
                <span class="replay-checknum">还可以输入<em>250</em>个字</span>
                <a class="btn btn-success float_right" href="javascript:;" data-id="<%=data_id %>">评 论</a>
            </div>
        </div>
    </div>
</div>
