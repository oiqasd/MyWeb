<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userDefault.aspx.cs" Inherits="YZ.Web.Asp.userDefault" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<script src="js/jquery-1.8.3.min.js"></script>
    <script src="js/bottom.js"></script>--%>
    <title></title>
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/style_user.css" rel="stylesheet" />

</head>
<body>
    <form runat="server" class="width_650" style="min-height: 1350px;">
        <div>
            <%--   <div class="width_650 float_left" >--%>
            <%
                if (ArticleList == null) return;
                foreach (var item in ArticleList)
                {
            %>
            <div class="post border_radius_6" >
                <div class="title">
                    <div style="width: 150px; float: left">
                        <h3><a href="article.aspx?artid=<%=item.id %>" target="_blank"><%=item.a_Title %></a></h3>
                    </div>

                    <div class="meta">
                        <span class="date"><%=Convert.ToDateTime(item.a_CreateDate).ToString("yyyy-MM-dd HH:MM:ss") %></span>
                        <span class="posted">来源：<a target="_blank" href="userArticle.aspx?userid=<%=item.a_CreateBy %>"><%=item.UserInfo.U_UserName %></a></span>
                    </div>
                </div>
                <br />
                <%--   <p class="meta ">
                   
                </p>--%>
                <%-- <div style="clear: both;">&nbsp;</div>--%>
                <div class="entry">
                    <div style="text-indent: 2em; font-family: 'HelveticaNeue', 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 13px; line-height: 22px;">

                        <%=YZ.Common.HtmlHelper.ReplaceHtmlTag(item.a_Content,90) %>
                        <%if (item.a_Content.Length > 100)
                          { %>
                        <span>...<a class="links" href="article.aspx?artid=<%=item.id %>" target="_blank">[详细]</a>
                            <%-- &nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="#">Comments</a>--%>
                        </span>
                        <% } %>
                    </div>
                </div>
            </div>
            <%
                } %>
        </div>
        <div class="clearfix pager margin_top_20">
            <webdiyer:AspNetPager ID="Pager" runat="server" HorizontalAlign="Right"
                FirstPageText="首页" LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页"
                NumericButtonTextFormatString="{0}" Width="100%" ShowCustomInfoSection="Right"
                ShowBoxThreshold="25" PageSize="10" AlwaysShow="true" TextAfterPageIndexBox=""
                OnPageChanging="Pager_PageChanging" PagingButtonSpacing="1px" TabIndex="100"
                CustomInfoHTML="<div  class='custominfohtml' style='color:#3b4348'>第<strong>%CurrentPageIndex%</strong>页，共<strong>%PageCount%</strong>页<strong>%RecordCount%</strong>条</div>">
            </webdiyer:AspNetPager>
        </div>

    </form>
</body>
</html>
