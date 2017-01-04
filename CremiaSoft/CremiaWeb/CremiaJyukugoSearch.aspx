<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CremiaJyukugoSearch.aspx.cs" Inherits="CremiaWeb.CremiaJyukugoSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>クレミアニ字熟語</title>

    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />

<%--    <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width,initial-scale=1.0, maximum-scale=2.0, user-scalable=yes" />--%>

    <style type="text/css">
        <!--
        html, body {
            height: 100%;
            margin: 0; /*ページ上部の隙間(余白､空白)を消す(*/
            -webkit-text-size-adjust: 100%;
        }

        td {
            width: 100px;
            height: 100px;
        }

        .tdsearch 
        {
            text-align:center;
        }

        .tbx
         {
            width:100px;
            height:100px;
            font-size:80px;
            text-align:center;
            vertical-align:central;
            
           
        }

        .btn
        {
            Width:100%;
            Height:100px; 
            font-size:40px;
            font-weight:bold;
            color:gray;
        }

        .lb
        {
            width:300px;
            height:100px;
            font-size:80px;

        }

        p
        {
            border:0px;
            margin:0px;
            padding:0px;
            width:auto;
            height:auto;
            font-size:40px;
            font-style:italic;
            font-weight:bold;
            background-color:lightblue;
            color:#6f1c1c;
            text-align:center;
            

        }

        .CremiaImage
        {
           margin-top:3px;
           margin-right:auto;
           margin-left:-20px;
        }


        -->
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu ID="Menu1" runat="server"></asp:Menu>
        <div>
            <div>
            </div>
            <table style="background-color:yellow">
                <tr>
                    <td colspan="3" style="width:auto;height:auto"><p><img class="CremiaImage" src="img/Cremia50x50.png" />Cremia Soft</p>
</td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxA" CssClass="tbx" Text="" MaxLength="1" /></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" ID="tbxB" CssClass="tbx"  Text="" MaxLength="1" /></td>
                    <td style="vertical-align:central;text-align:center">
                        <asp:Label runat="server"  ID="lb" CssClass="lb"   Text="" ForeColor="Red"/></td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxD" CssClass="tbx"   Text="" MaxLength="1" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxC" CssClass="tbx"   Text=""  MaxLength="1" /></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="検索" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>


            <div><asp:Label ID="lbResults" runat="server"  Font-Size="15"/></div>
        </div>
    </form>
</body>
</html>
