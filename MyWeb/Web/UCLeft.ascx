<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLeft.ascx.cs" Inherits="YZ.Web.Asp.UCLeft" %>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<div id="sidebar">
    <ul>
        <li>
            <h2>最新发布</h2>
            <ul>
                 <%foreach (var item in ArticleList)
                  { 
                %>
                <li><a href="article.aspx?artid=<%=item.id %>"><%=item.a_Title %></a></li>

                <%
                  } %>
               
                <li class="UcnMore"><a href="articlelist.aspx?order=1">更多>></a></li>
            </ul> 
        </li>
    </ul>
</div>
