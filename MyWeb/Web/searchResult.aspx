<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="searchResult.aspx.cs" Inherits="YZ.Web.Asp.searchResult" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script>
        $(function () {

            $("#<%=txtKeyWord.ClientID%>").val("<%= Request.QueryString["keyword"]%>");

            $("#btnSearch").click(function () {
                window.location.href = "searchResult.aspx?keyword=" + $("#txtKeyWord").val();
            });

        });
    </script>--%>
    <style>
        @media screen and (min-height:600px) {
            #content {
                height: 100%;
                min-height: 400px;
                margin-top: 15px ;
                margin-left: 15px ;
                width: 600px;
                padding-left:30px;
                padding-right:30px;
            }

            ul {
                margin: auto;
            }

            .history { 
                width: 260px;
                margin-top:15px;
                margin-left:15px;
            }

            .bore6 {
                border: 1px solid #e6e6e6;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div>
            <!--Search-->
            <div class="searchback">
                <div id="search-form" class="search-form  clearfix ">
                    <input class="search-term required " style="color: black;" id="txtKeyWord" type="text" runat="server" placeholder="输入您要查询的关键字" title="* 请输入您要查询的关键字!" />
                    <input class="search-btn" id="btnSearch" type="submit" value="搜  索" />
                    <div id="search-error-container"></div>
                </div>
            </div>
            <!--history start -->
            <div class=" float_left bore6 history ">
                <div>adsa</div>
            </div>
            <!--history end -->
            <%--content start--%>

            <div id="content" class="bore6 float_left">
                <ul style="list-style-type: square; ">
                    <% 
                        {
                            foreach (var item in resultList)
                            { 
                    %><li>
                        <div style="border-bottom: 1px dotted #E7E2DC;">
                            <h4 style="font-size: 14px;"><a href="article.aspx?artid=<%=item.id %>"><%=item.a_Title %></a></h4>
                            <div class="m_content  HideFont">
                                <%=YZ.Common.HtmlHelper.ReplaceHtmlTag(item.a_Content,100) %>...<a href="article.aspx?artid=<%=item.id %>" target="_self" class="link">[详细]</a>
                            </div>
                        </div>
                    </li>
                    <%
                            }
                        }%>
                </ul>
            </div>

            <%--content end--%>
            <!--cf start -->
            <div class=" float_left bore6 history ">
                <div>adsa</div>
            </div>
            <!--cf end -->
        </div>
        <div class="clearfix"></div>
        <div class="pager">
            <webdiyer:AspNetPager ID="Pager" runat="server" HorizontalAlign="Center"
                FirstPageText="首页" LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页"
                NumericButtonTextFormatString="{0}" Width="100%" ShowCustomInfoSection="Right"
                ShowBoxThreshold="25" PageSize="15" AlwaysShow="true" TextAfterPageIndexBox=""
                OnPageChanging="Pager_PageChanging" PagingButtonSpacing="1px" TabIndex="100"
                CustomInfoHTML="<div style='white-space: nowrap;height:24px; color:#3b4348'>第<font color='#b3b3b3'>%CurrentPageIndex%</font>页，共<font color='#b3b3b3'>%PageCount%</font>页<font color='#b3b3b3'>%RecordCount%</font>条</div>">
            </webdiyer:AspNetPager>
        </div>
    </form>
</asp:Content>
