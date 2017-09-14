<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRight.ascx.cs" Inherits="YZ.Web.Asp.UCRight" %>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<div id="sidebar">
    <ul>
        <li>
            <h2>热门文章</h2>
            <ul>
                 <%foreach (var item in ArticleList)
                  { 
                %>
                <li><a href="article.aspx?artid=<%=item.id %>"><%=item.a_Title %></a></li>

                <%
                  } %>
               
                <li class="UcnMore"><a href="articlelist.aspx?order=2">更多>></a></li>
            </ul> 
        </li>
    </ul>
</div>
