<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="articlelist.aspx.cs" Inherits="YZ.Web.Asp.articlelist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/UCRight.ascx" TagPrefix="uc1" TagName="UCRight" %>
<%@ Register Src="~/UCLeft.ascx" TagPrefix="uc1" TagName="UCLeft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/osSlider.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" class="Text_Center">
        <%--导航图片--%>
        <div class="" style="width: 100%">
            <div class="slider">
                <ul class="slider-main">
                    <li>
                        <img src="images/1.jpg" alt="" />
                    </li>
                    <li>
                        <img src="images/2.jpg" alt="" />
                    </li>
                    <li>
                        <img src="images/3.jpg" alt="" />
                    </li>
                    <li>
                        <img src="images/4.jpg" alt="" />
                    </li>
                </ul>
            </div>
        </div>
        <%--导航图片--%>
        <div style="width: 1156px; display: inline-block; min-height: 494px;" class="<%--padding-top20 --%>Text_Left ">

            <div id="divLeft" class="width_230 border_radius_6 margin_top_15" style="border: 1px solid #E9EDF1;">

                <aside>
                    <%-- <section class="widget">
                        <div class="tagcloud margin_left_15 margin_top_20">
                            <a href="articlelist.aspx?order=1" target="_self" class="btn btn-mini">按时间</a>
                            <a href="articlelist.aspx?order=2" target="_self" class="btn btn-mini">按点击量</a>
                            <a href="articlelist.aspx?order=3" target="_self" class="btn btn-mini">按用户</a>

                        </div>
                    </section>--%>

                    <section>
                        <uc1:UCLeft runat="server" />
                    </section>
                </aside>


            </div>
            <div id="divCenter" class="width_620 border_radius_6 box_shadow">
                <section>
                    <div class="tagcloud margin_top_20 margin_right_20  float_right border_bottom_dot">
                        <a href="articlelist.aspx?order=1" target="_self" class="btn btn-mini">按时间</a>
                        <a href="articlelist.aspx?order=2" target="_self" class="btn btn-mini">按点击量</a>
                        <a href="articlelist.aspx?order=3" target="_self" class="btn btn-mini">按用户</a>
                    </div>
                </section>
                <%
                    if (ArticleList == null) return;
                    foreach (var item in ArticleList)
                    {
                        if (item == null)
                            continue;
                %>
                <div class="post margin_left_6 width_600" style="border-bottom: 1px solid #E3E3EC; margin-top: 10px; padding-left: 5px; display: inline-block; border-radius: 0px 20px;">
                    <div class="wb_face float_left margin_top_20">
                        <a href="userArticle.aspx?userid=<%=item.a_CreateBy %>&um=<%=Server.UrlEncode(item.UserInfo.U_NickName) %>">
                            <img src="images/face/<%=item.UserInfo.U_HeadPic??"th.jpg" %>" title="<%=item.UserInfo.U_NickName %>" width="50" height="50" class="wb_face_radius" />
                        </a>
                    </div>
                    <div class="wb_detail">
                        <h3 class="title14"><a href="article.aspx?artid=<%=item.id %>"><%=item.a_Title %></a></h3>

                        <div>
                            <%=YZ.Common.HtmlHelper.ReplaceHtmlTag(item.a_Content,80) %>
                            <%--  <%if (item.a_Content.Length > 85)
                              { %>--%>
                            <span class="text_right">&nbsp;<a href="article.aspx?artid=<%=item.id %>" target="_self" class="link">[详细]</a>
                                <%-- &nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="#">Comments</a>--%>
                            </span>
                            <%--   <% } %>--%>
                        </div>
                        <div>
                            <span class="date"><%=Convert.ToDateTime(item.a_CreateDate).ToString("yyyy-MM-dd HH:MM:ss") %>&nbsp;|</span>
                            <span class="posted">From：<a href="article.aspx?artid=<%=item.id %>"><%=item.UserInfo.U_NickName %></a></span>

                        </div>
                    </div>

                </div>
                <%
                    } %>
                <br />
                <div class="pager margin_top_20">
                    <webdiyer:AspNetPager ID="Pager" runat="server" HorizontalAlign="Right"
                        FirstPageText="首页" LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页"
                        NumericButtonTextFormatString="{0}" Width="100%" ShowCustomInfoSection="Right"
                        ShowBoxThreshold="25" PageSize="15" AlwaysShow="true" TextAfterPageIndexBox=""
                        OnPageChanging="Pager_PageChanging" PagingButtonSpacing="1px" TabIndex="100"
                        CustomInfoHTML="<div style='white-space: nowrap;height:24px; color:#3b4348'>第<font color='#b3b3b3'>%CurrentPageIndex%</font>页，共<font color='#b3b3b3'>%PageCount%</font>页<font color='#b3b3b3'>%RecordCount%</font>条</div>">
                    </webdiyer:AspNetPager>
                </div>
            </div>

            <div id="divRight" class="width_230 border_radius_6 margin_top_15" style="border: 1px solid #E9EDF1;">
                <aside>
                    <section>
                        <uc1:UCRight runat="server" />
                    </section>
                </aside>

            </div>
        </div>
    </form>

    <script src="js/osSlider.js"></script>
    <script type="text/javascript">
        var slider = new osSlider({ //开始创建效果
            pNode: '.slider', //容器的选择器 必填
            cNode: '.slider-main li', //轮播体的选择器 必填
            speed: 3000, //速度 默认3000 可不填写
            autoPlay: true //是否自动播放 默认true 可不填写
        });
    </script>
</asp:Content>
