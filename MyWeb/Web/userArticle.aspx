<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="userArticle.aspx.cs" Inherits="YZ.Web.Asp.userArticle" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/UserArticle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <form runat="server">
        <div class="body">
            <div class="width_980 feed">
                <div class="mod-tab">
                    <ul>
                        <li><a><span class="item-wrap active">个人文章</span></a></li>
                        <%--<li><a><span class="item-wrap">私密文章</span></a></li>--%>
                    </ul>
                </div>

                <div class="content">
                    <div class="pad-left20">
                        <div class="write-link width_100">
                            <span onclick="javascript:window.location.href='ArticleDetail.aspx?action=new'">Ｔ写文章</span>
                        </div>
                    </div>
                    <div class="clearfix">
                        <ul id="tab">
                            <%if (MyArticleList != null && MyArticleList.Count > 0)
                              {
                                  foreach (var item in MyArticleList)
                                  {
                            %>
                            <li style="padding: 0 10px 0 10px;">
                                <a href="article.aspx?artid=<%=item.id %>" target="_blank"><%=item.a_Title%></a>
                                <div class="float_right margin_right_20">
                                    <span class="color-a8"><%=item.a_ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss") %></span> &nbsp;
                               <%if (IsSelf)
                                 { 
                               %> <a style="text-decoration: underline; color: blue;" href="ArticleDetail.aspx?aid=<%=item.id%>">编辑</a>
                                    <%
                                 }
                                 else
                                 {%>
                                    <a style="text-decoration: underline; color: blue;" href="article.aspx?artid=<%=item.id%>">查看</a>
                                    <%} %>
                                </div>
                            </li>
                            <% 
                                  }
                              } %>
                        </ul>

                    </div>

                </div>
            </div>
        </div>
        <div class="pager">
            <webdiyer:AspNetPager ID="Pager" runat="server" HorizontalAlign="Right"
                FirstPageText="首页" LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页"
                NumericButtonTextFormatString="{0}" Width="100%" ShowCustomInfoSection="Left"
                ShowBoxThreshold="25" PageSize="10" AlwaysShow="true" TextAfterPageIndexBox=""
                OnPageChanging="Pager_PageChanging" PagingButtonSpacing="1px" TabIndex="100"
                CustomInfoHTML="<div class='custominfohtml'>第<font color='#ff0000'>%CurrentPageIndex%</font>页，共<font color='#ff0000'>%PageCount%</font>页<font color='#ff0000'>%RecordCount%</font>条</div>">
            </webdiyer:AspNetPager>
        </div>
    </form>
    <script defer="defer">
        window.onload = DoubleRow('tab');
        var height = window.innerHeight - 178;
        $('div.body').css('min-height', height + "px");
    </script>
</asp:Content>
