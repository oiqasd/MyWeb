<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="drug.aspx.cs" Inherits="YZ.Web.Asp.drug" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#ms-drug").addClass("current-menu-item");
            $('div.body').css('min-height', (window.innerHeight - 129) + 'px');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body" >

    </div>
</asp:Content>
