<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="article.aspx.cs" Inherits="YZ.Web.Asp.article" %>

<%@ Register Src="~/UCRight.ascx" TagPrefix="uc1" TagName="UCRight" %>
<%--<%@ Register Src="~/UcComment.ascx" TagPrefix="uc2" TagName="UcComment" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/article.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () { 
            $('div.body').css('min-height', (window.innerHeight - 122) + 'px');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body " style="/*position: absolute; */width: 100%; height: 100%">
        <div class="titlenav">&nbsp;当前位置：<a href="../index.aspx">首页</a>&nbsp;>&nbsp;<a href="../articlelist.aspx?type=<%=e_Article.articleTypeId %>"><%=e_Article.articleType %></a>&nbsp;>&nbsp;<%=e_Article.articleTitle %></div>
        <article>
            <section>
                <div>
                    <p class="Detail_Title Text_Center a_title"><%=e_Article.articleTitle %></p>
                    <p>
                        <span>创建时间： <%=e_Article.articleCreateDate %></span>
                        <span><%=e_Article.articleFrom %></span>
                        <%if (e_Article.self)
                          { 
                        %>
                            &nbsp;&nbsp;<a href="ArticleDetail.aspx?aid=<%=e_Article.articleId %>" class="link">编辑</a>
                        <%
                          } %>
                    </p>
                    <div class="Detail_Content" id="details" runat="server"></div>
                    <br />
                </div>
                <div style="border-top: 2px solid #e3e3e3"><span id="PrevPage" class="float_left">上一篇:<%=prevArticle %></span><span id="NextPage" class="float_right">下一篇:<%=nextArticle %></span></div>

                <%--点评区--%>
                <asp:PlaceHolder ID="pholder" runat="server"></asp:PlaceHolder>
                <%--<uc2:UcComment runat="server" aid='<%=e_Article.articleId %>' />--%>
                <!--
                <div class="mod-comment" style="display: none">
                    <h3>点评区</h3>
                    <div class="comment-box">
                        <div class="cmtbox">
                            <p class="atv">
                                <img src="images/face/df3654.jpg" />
                            </p>
                            <div>
                                <textarea id="txtComment" placeholder="随便说点什么吧"></textarea>
                            </div>
                            <p class="btns3">
                                <span class="checknum">还可以输入<em>250</em>个字</span>
                                <a href="javascript:;" class="btn btn-info width_50">评 论</a>
                            </p>
                        </div>
                    </div>
                    <%--评论列表--%>

                    <div class="comments">
                        <div class="cmtlist">
                            <%--第一条--%>
                            <div class="li">
                                <div class="atv">
                                    <img src="images/face/df3654.jpg" />
                                </div>
                                <div class="cnt">
                                    <div class="user"><span class="nm">ttwan</span><span class="tm">今天 12:03</span></div>
                                    <div class="txt">如果真是祼眼实现，真会吓死宝宝的</div>
                                </div>
                                <div class="replay">
                                    <div class="replay-bar">
                                        <a class="cool-btn can-cool" href="javascript:;">
                                            <input type="hidden" id="2562581" />
                                            <i class="num-cool">1</i>
                                            <i class="num-add">+1</i>
                                        </a>
                                        <a class="replay-btn" href="javascript:;">回复</a>
                                    </div>
                                    <%--回复评论--%>
                                    <div class="replay-box" style="display: none;">
                                        <i class="replay-textarea">
                                            <textarea></textarea>
                                        </i>
                                        <div class="replay-status-bar clearfix">
                                            <span class="replay-checknum">还可以输入<em>250</em>个字</span>
                                            <a class="btn btn-success float_right" href="javascript:;">评 论</a>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--第一条结束--%>
                        </div>
                    </div>

                    <%--评论列表结束--%>

                </div>
                <%--点评区结束--%>
                    -->
            </section>

            <aside style="border: 1px solid #e3e3e3;" class="border_radius_6 Text_Left ">
                <uc1:UCRight runat="server" />
            </aside>
        </article>
    </div>

</asp:Content>
