<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="userCenter.aspx.cs" Inherits="YZ.Web.Asp.userCenter" %>

<%@ Register Src="~/UCNavigation.ascx" TagPrefix="uc1" TagName="UCNavigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style_user.css" rel="stylesheet" />
    <script src="js/bottom.js"></script>

    <script type="text/javascript">
        function dyniframesize(down) {
            var pTar = null;
            if (document.getElementById) {
                pTar = document.getElementById(down);
            }
            else {
                eval('pTar = ' + down + ';');
            }

            if (pTar && !window.opera) {
                //begin resizing iframe 
                pTar.style.display = "block"
                if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
                    //ns6 syntax 
                    pTar.height = pTar.contentDocument.body.offsetHeight + 20;
                    //pTar.width = pTar.contentDocument.body.scrollWidth + 20;
                }
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax 
                    pTar.height = pTar.Document.body.scrollHeight;
                    //pTar.width = pTar.Document.body.scrollWidth;
                }
                var fh = Number(pTar.height) + 30;
               
                document.getElementById('divFrm').style.height = fh + 'px';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divFrm" class="width_980" style="margin:0 auto; /*border:1px solid #dddddd;*/">

        <div class="width_675 margin_top_20 float_left" >
            <iframe src="userDefault.aspx" frameborder="0" scrolling="no" onload="javascript:dyniframesize('ifm');" width="100%" name="main" id="ifm"></iframe>
        </div>
        <div class="width_300 float_right">
            <uc1:UCNavigation runat="server" ID="UCNavigation" />
        </div>
    </div>
</asp:Content>
