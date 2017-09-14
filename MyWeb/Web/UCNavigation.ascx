<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNavigation.ascx.cs" Inherits="YZ.Web.Asp.UCNavigation" %>
<link href="css/style_user.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<div id="sidebar">
    <ul>
        <li>
            <div id="search">
                <form method="post" action="searchResult.aspx">
                    <div>
                        <input type="text" name="keyword" id="keyword" value="" />
                        <input type="submit" id="search-submit" value="GO" />
                    </div>
                </form>
            </div>
            <div style="clear: both;">&nbsp;</div>
        </li>
        <li style="display:none">
            <h2>时间戳</h2>
            <p><a id="a_Signature"><%=userinfo.U_Signature %></a><a id="a_edit" style="cursor: pointer; text-decoration: underline;">编辑</a></p>
            <div id="EditSign" class="search displayNone">
                <input type="text" name="Signature" id="txtSignature" value="" /><br />
                <input type="button" id="btnConfirm" value="确定" />
                <input type="button" id="btnCancel" value="取消" />
            </div>
        </li>

        <li>
            <h2>个人中心</h2>
            <p><a id="link_userDeault" href="userDefault.aspx" target="main">个人中心</a></p>
            <p><a href="userArticle.aspx" target="_blank">我要发布</a></p>
            <p><a href="#" target="main">我的收藏</a></p>
            <p><a href="#" target="main">我的关注</a></p>
            <p><a href="userInfo.aspx" target="main">完善个人资料</a></p>

        </li>
        <li>
            <h2>大家都在看</h2>
            <ul>
                <%foreach (var item in ArticleList)
                  { 
                %>
                <li><a target="_blank" href="article.aspx?artid=<%=item.id %>"><%=item.a_Title %></a></li>

                <%
                  } %>
                <%--<li><a href="#">Aliquam libero</a></li>
                <li><a href="#">Consectetuer adipiscing elit</a></li>
                <li><a href="#">Metus aliquam pellentesque</a></li>
                <li><a href="#">Suspendisse iaculis mauris</a></li>
                <li><a href="#">Urnanet non molestie semper</a></li>
                <li><a href="#">Proin gravida orci porttitor</a></li>--%>
                <li class="UcnMore"><a href="articlelist.aspx?order=2">更多</a></li>
            </ul>
        </li>
        <li>
            <h2>我的分享</h2>
            <ul>
                <%foreach (var item in MyArticleList)
                  { 
                %>
                <li><a target="_blank" href="article.aspx?artid=<%=item.id %>"><%=item.a_Title %></a></li>

                <%
                  } %>
                <%-- <li><a href="#">Aliquam libero</a></li>
                <li><a href="#">Consectetuer adipiscing elit</a></li>
                <li><a href="#">Metus aliquam pellentesque</a></li>
                <li><a href="#">Suspendisse iaculis mauris</a></li>
                <li><a href="#">Urnanet non molestie semper</a></li>
                <li><a href="#">Proin gravida orci porttitor</a></li>--%>
                <li class="UcnMore"><a href="userArticle.aspx?userid=<%=UserID %>&um=<%=Server.UrlEncode(UserName) %>">更多</a></li>
            </ul>
        </li>
        <li>
            <h2>浏览记录</h2>
            <ul>
                <li><a href="#" target="_blank">Aliquam libero</a></li>
                <li><a href="#">Consectetuer adipiscing elit</a></li>
                <li><a href="#">Metus aliquam pellentesque</a></li>
                <li><a href="#">Suspendisse iaculis mauris</a></li>
                <li><a href="#">Urnanet non molestie semper</a></li>
                <li><a href="#">Proin gravida orci porttitor</a></li>
                <li class="UcnMore"><a href="#">更多</a></li>
            </ul>
        </li>
    </ul>

    <script src="js/jquery-1.8.3.min.js"></script>
    <script>
        $(function () {
            $("#a_edit").click(function () {
                $("#EditSign").removeClass("displayNone");
                $("#EditSign").addClass("displayblock");
            }, function () {
                $("#EditSign").addClass("displayNone");
                $("#EditSign").removeClass("displayblock");
            })
        })

    </script>
</div>
