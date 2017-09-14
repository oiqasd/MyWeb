<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TablesForm.aspx.cs" Inherits="MSSqlVisual.TablesForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="index.aspx">首页</a>
            <div>
                <label>表：</label>
                <asp:DropDownList runat="server" ID="drpTable" OnSelectedIndexChanged="drpTable_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                <label>显示条数：</label>
                <asp:DropDownList runat="server" ID="drpPageSize">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                    <asp:ListItem Value="999">999</asp:ListItem>
                </asp:DropDownList>

                <label>页：</label>
                <asp:DropDownList runat="server" ID="drpPageIndex">
                    <asp:ListItem Value="1">1</asp:ListItem>
                </asp:DropDownList>

                <asp:Button runat="server" ID="btnQuery" OnClick="btnQuery_Click" Text="查询" />

            </div>
            <div style="margin-top: 20px;">
                <asp:GridView ID="tbview" runat="server" EmptyDataText="没有可显示的数据记录。">

                    <RowStyle ForeColor="#000066" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
                    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>


                    <Columns>
                        <%--<asp:CommandField ShowDeleteButton="false" ShowEditButton="True" />--%>
                        <%--  <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"
                            SortExpression="ID" />--%>

                        <asp:TemplateField>
                            <HeaderTemplate>行号</HeaderTemplate>
                            <ItemTemplate><%# Container.DataItemIndex + 1%> </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </form>
</body>
</html>
