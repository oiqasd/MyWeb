<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="custom.aspx.cs" Inherits="MSSqlVisual.custom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        table tr, td {
            border: 1px solid #eee;
            height: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="height: 800px;">


        <div style="">
            <a href="index.aspx">首页</a>

            <table style="margin: 100px auto; border: 1px solid #eee;" cellspacing="0">
                <tr>
                    <th colspan="2">更新</th>
                </tr>
                <tr>
                    <td>表：
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="drpTable" OnSelectedIndexChanged="drpTable_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>字段：
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="drpCol"></asp:DropDownList>=
                        <asp:TextBox ID="txtvalue" runat="server"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td colspan="2" style="text-align: center"><b>条件</b><br />
                        *用唯一标识</td>
                </tr>
                <tr>
                    <td>字段：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="drpsel"></asp:DropDownList>
                        =<asp:TextBox ID="txtsel" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center">

                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="更 新" /></td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="2">*请确保填入的数据类型一致。
                            <br />

                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </form>
</body>
</html>
