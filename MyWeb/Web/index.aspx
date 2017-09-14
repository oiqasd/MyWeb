<%@ Page Title="首页" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="YZ.Web.Asp.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="search-area-wrapper">
        <div class="search-area container">
            <div class="">
                <%-- <div class=" width_200">
                    <embed src="swf/flash_colck.swf" quality="high" wmode="flashscluwei" class="Height_200  width_200 float_left" />
                </div>--%>
                <div class="<%--float_right width_80per--%> ">
                    <h3 class="search-header">Have a Question?</h3>
                    <%--<p class="search-tag-line">If you have any question you can ask below or enter what you are looking for!</p>--%>
                    <p class="search-tag-line">你可以在下面输入任何你想要寻找的答案！</p>
                    <div id="search-form" class="search-form clearfix">
                        <input class="search-term required" id="txtKeyWord" type="text" placeholder="输入您要查询的关键字" title="* 请输入您要查询的关键字!" />
                        <input class="search-btn" id="btnSearch" type="submit" value="搜  索" /><!--Search-->
                        <div id="search-error-container"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End of Search Wrapper -->

    <!-- Start of Page Container -->

    <div class="page-container">
        <div class="container">
            <div class="row">

                <!-- start of page content -->
                <div class="span8 page-content">

                    <!-- Basic Home Page Template -->
                    <div class="row separator">
                        <section class="span4 articles-list">
                            <h3><a href="articlelist.aspx?order=2">热门搜索</a></h3>
                            <ul class="articles" style="list-style-type: square">

                                <%if (HotArticleList != null && HotArticleList.Count > 0)
                                  {
                                      foreach (var item in HotArticleList)
                                      { 
                                %>
                                <li class="article-entry standard">
                                    <h4>☞<a target="_blank" href="article.aspx?artid=<%=item.id %>"><%=YZ.Common.HtmlHelper.ReplaceHtmlTag( item.a_Title ,20)%></a></h4>
                                    <span class="article-meta"><%=item.a_ModifiedDate.ToString("yyyy-MM-dd HH:MM:ss") %>
                                        <a href="userArticle.aspx?userid=<%=item.a_CreateBy %>" title="他的所有文章" style="color: #808080"><%=item.UserInfo.U_UserName %></a></span>
                                    <%--点赞功能--%>
                                    <%--<span class="like-count" onclick=""><%=item.LikeCounts.ToList().Count %></span>--%>
                                </li>
                                <%
                                      }
                                  } %>
                                <li style="list-style-type: none" class="text_right"><a class="btn btn-link" href="articlelist.aspx?order=2">更多>></a>　</li>

                            </ul>
                        </section>

                        <section class="span4 articles-list">
                            <h3><a href="articlelist.aspx?order=1">最新发布</a></h3>
                            <ul class="articles" style="list-style-type: armenian">

                                <%if (NewArticleList != null && NewArticleList.Count > 0)
                                  {
                                      foreach (var item in NewArticleList)
                                      { 
                                %>
                                <li class="article-entry standard">
                                    <h4>☞<a target="_blank" href="article.aspx?artid=<%=item.id %>"> <%=YZ.Common.HtmlHelper.ReplaceHtmlTag( item.a_Title ,20) %></a></h4>
                                    <span class="article-meta"><%=item.a_ModifiedDate.ToString("yyyy-MM-dd HH:MM:ss") %>
                                        <a href="userArticle.aspx?userid=<%=item.a_CreateBy %>" title="他的所有文章" style="color: #808080"><%=item.UserInfo.U_UserName %></a></span>
                                    <%--点赞功能--%>
                                    <%--<span class="like-count" onclick=""><%=item.LikeCounts.ToList().Count %></span>--%>
                                </li>
                                <%
                                      }
                                  } %>
                                <li class="text_right" style="list-style-type: none"><a class="btn btn-link" href="articlelist.aspx?order=1">更多>></a>　</li>

                            </ul>
                        </section>
                    </div>
                </div>
                <!-- end of page content -->

                <!-- start of sidebar -->
                <aside class="span4 page-sidebar">

                    <section class="widget">
                        <div class="support-widget">
                            <h3 class="title">更多</h3>
                            <p class="intro">Need more support? If you did not found an answer, contact us for further help.</p>
                        </div>
                    </section>

                    <section class="widget">
                        <div class="quick-links-widget">
                            <h3 class="title">快速入口</h3>
                            <ul id="menu-quick-links" class="menu clearfix">
                                <li><a class="label" href="index.aspx">首页</a></li>
                                <li><a class="label label-info" href="articlelist.aspx">文章列表</a></li>
                                <li><a class="label" href="faq.html">FAQs</a></li>
                                <li><a class="label label-info" href="feedback.aspx">反馈</a></li>
                                <li><a class="label" href="other/about.html">联系我们</a></li>
                                <li><a class="label label-info" href="other/tools.html">实用小工具</a></li>
                            </ul>
                        </div>
                    </section>

                    <section class="widget">
                        <h3 class="title">标签</h3>
                        <div class="tagcloud">
                            <a href="#" class="btn btn-mini">basic</a>
                            <a href="#" class="btn btn-mini">beginner</a>
                            <a href="#" class="btn btn-mini">blogging</a>
                            <a href="#" class="btn btn-mini">colour</a>
                            <a href="#" class="btn btn-mini">css</a>
                            <a href="#" class="btn btn-mini">date</a>
                            <a href="#" class="btn btn-mini">design</a>
                            <a href="#" class="btn btn-mini">files</a>
                            <a href="#" class="btn btn-mini">format</a>
                            <a href="#" class="btn btn-mini">header</a>
                            <a href="#" class="btn btn-mini">images</a>
                            <a href="#" class="btn btn-mini">plugins</a>
                            <a href="#" class="btn btn-mini">setting</a>
                            <a href="#" class="btn btn-mini">templates</a>
                            <a href="#" class="btn btn-mini">theme</a>
                            <a href="#" class="btn btn-mini">time</a>
                            <a href="#" class="btn btn-mini">videos</a>
                            <a href="#" class="btn btn-mini">website</a>
                            <a href="#" class="btn btn-mini">wordpress</a>
                        </div>
                    </section>

                </aside>
                <!-- end of sidebar -->
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //固定搜索框
        //var mt = 0;
        //window.onload = function () {
        //    var mydiv = document.getElementById("search-form");
        //    var mt = mydiv.offsetTop;
        //    window.onscroll = function () {
        //        var t = document.documentElement.scrollTop || document.body.scrollTop;
        //        if (t > mt) {
        //            mydiv.style.position = "fixed";
        //            mydiv.style.margin = "0";
        //            mydiv.style.top = "0";
        //        }
        //        else {
        //            mydiv.style.margin = "20px 0";
        //            mydiv.style.position = "static";
        //        }
        //    }
        //};
        $(function () {
            $("#ms-ind").addClass("current-menu-item");

            $("#btnSearch").click(function () {

                if ($("#txtKeyWord").val() == "") {
                    return;
                }
                else {
                    window.open("searchResult.aspx?keyword=" + $("#txtKeyWord").val());
                }
            });

        });

    </script>
</asp:Content>
