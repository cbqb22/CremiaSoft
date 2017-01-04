<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CremiaPolyomino.aspx.cs" Inherits="CremiaWeb.CreamiPolyomino" Trace="false" %>

<%@ Register Assembly="CremiaWeb" TagPrefix="aspPolyomino" Namespace="CremiaWeb.Controls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />


    <script type="text/javascript"><!--
    function StartAnalysis() {

        //二重クリックの防止
        if (window.document.readyState != null && window.document.readyState != 'complete') {
            alert("前の処理を実行中です。もうしばらくお待ちください。");
            return false;
        }

        myRet = confirm("解析を実行しますか？");
        return myRet;

    }
    // --></script>

    <style type="text/css">
        <!--
        html, body {
            height: 100%;
            margin: 0; /*ページ上部の隙間(余白､空白)を消す(*/
            -webkit-text-size-adjust: 100%;
        }




        .btn {
            Width: 160px;
            Height: 40px;
            font-size: 20px;
            font-weight: bold;
            color: gray;
        }

        .btnClearDrawArea {
            Width: 50px;
            Height: 40px;
            font-size: 20px;
            font-weight: bold;
            color: gray;
        }


        .PolyominoContainer {
            margin-top: 4px;
        }

        .OperationContainer {
            width: auto;
            height: auto;
            /*width:700px;
            max-height:500px;
            height:auto;*/
        }

        .PolyominoStack {
            /*width:auto;
            min-width:200px;
            height:auto;
            min-height:200px;*/
        }

        .tdleft {
            vertical-align: top;
        }

        .table5 {
            border-collapse: collapse;
            width: auto;
        }

        .thCremiaHeader {
            background-color: lightblue;
        }

        .table5 th {
            background-color: #cccccc;
            font-size: 20px;
            font-weight: bold;
        }

        .table5 td {
            text-align: center;
            vertical-align: top;
        }

        .tdleft {
            width: 400px;
            height: 280px;
        }

        .tdright {
            width: 400px;
        }


        p {
            border: 0px;
            margin: 0px;
            padding: 0px;
            width: auto;
            height: auto;
            font-size: 40px;
            font-style: italic;
            font-weight: bold;
            background-color: lightblue;
            color: #6f1c1c;
            text-align: center;
        }

        .ddl {
            vertical-align: middle;
        }

        .margintop {
            margin-top: 5px;
        }

        .explanation {
            font-size: 15px;
        }


        -->
    </style>

    <title>クレミアポリオミノ</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="OperationContainer">

            <table class="table5" border="1">
                <tr>
                    <th colspan="2" style="background-color: lightblue; height: 30px; font-size: 30px;">
                        <p>
                            <img class="CremiaImage" src="img/Cremia50x50.png" />クレミアポリオミノ
                        </p>
                    </th>
                </tr>
                <tr>
                    <th>手順１．パズルの部品を登録する</th>
                    <th>登録図形</th>
                </tr>
                <tr>
                    <td class="tdleft">
                        <div class="explanation">タップすると色が反転します。</div>
                        <div class="explanation">横(X)と縦(X)の枠数を設定できます。</div>
                        <hr />
                        <div>
                            X:
                            <asp:DropDownList runat="server" ID="ddlX" CssClass="ddl" OnSelectedIndexChanged="ddl_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="1" />
                                <asp:ListItem Value="2" />
                                <asp:ListItem Value="3" />
                                <asp:ListItem Value="4" />
                                <asp:ListItem Value="5" />
                                <asp:ListItem Value="6" />
                                <asp:ListItem Value="7" Selected="True" />
                                <asp:ListItem Value="8" />
                                <asp:ListItem Value="9" />
                                <asp:ListItem Value="10" />
                            </asp:DropDownList>
                            Y:
                            <asp:DropDownList runat="server" ID="ddlY" CssClass="ddl" OnSelectedIndexChanged="ddl_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="1" />
                                <asp:ListItem Value="2" />
                                <asp:ListItem Value="3" />
                                <asp:ListItem Value="4" />
                                <asp:ListItem Value="5" />
                                <asp:ListItem Value="6" />
                                <asp:ListItem Value="7" Selected="True" />
                                <asp:ListItem Value="8" />
                                <asp:ListItem Value="9" />
                                <asp:ListItem Value="10" />
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:Panel ID="pan" runat="server" />
                        </div>
                        <div class="margintop">
                            <asp:Button runat="server" ID="btnPolyominoAdd" Text="図形追加" CssClass="btn" OnClick="btnPolyominoAdd_Click" />
                        </div>

                    </td>
                    <td rowspan="3" class="tdright">
                        <div class="explanation">
                            登録した図形は左上の×ボタンで削除できます。
                        </div>
                        <div class="PolyominoStack">
                            <asp:Panel runat="server" ID="panelPolyominoStack" />
                        </div>

                    </td>
                </tr>

                <tr>
                    <th>手順２．描画枠を設定</th>
                </tr>
                <tr>
                    <td class="tdleft">
                        <div class="explanation">タップで不要な領域を設定できます。</div>
                        <div class="explanation">反転考慮は裏返した図形も探します。</div>
                        <div class="explanation">回転考慮は回転した図形も探します。</div>
                        <hr />
                        <div class="ddl">
                            X:
                            <asp:DropDownList runat="server" CssClass="ddl" ID="ddlX_drawArea" OnSelectedIndexChanged="ddl_drawArea_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="1" />
                                <asp:ListItem Value="2" />
                                <asp:ListItem Value="3" />
                                <asp:ListItem Value="4" />
                                <asp:ListItem Value="5" Selected="True" />
                                <asp:ListItem Value="6" />
                                <asp:ListItem Value="7" />
                                <asp:ListItem Value="8" />
                                <asp:ListItem Value="9" />
                                <asp:ListItem Value="10" />
                                <asp:ListItem Value="11" />
                                <asp:ListItem Value="12" />
                            </asp:DropDownList>
                            Y:
                            <asp:DropDownList runat="server" CssClass="ddl" ID="ddlY_drawArea" OnSelectedIndexChanged="ddl_drawArea_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="1" />
                                <asp:ListItem Value="2" />
                                <asp:ListItem Value="3" />
                                <asp:ListItem Value="4" />
                                <asp:ListItem Value="5" Selected="True" />
                                <asp:ListItem Value="6" />
                                <asp:ListItem Value="7" />
                                <asp:ListItem Value="8" />
                                <asp:ListItem Value="9" />
                                <asp:ListItem Value="10" />
                                <asp:ListItem Value="11" />
                                <asp:ListItem Value="12" />
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:Panel ID="pan_drawArea" runat="server" />
                        </div>
                        <div class="margintop">
                            <div>
                                <asp:Button runat="server" ID="btnPolyominoStart" Text="解析" CssClass="btn" OnClick="btnPolyominoStart_Click" OnClientClick="result = StartAnalysis(); return result;" />
                                <asp:Button runat="server" ID="btnClearDrawArea" Text="×" CssClass="btnClearDrawArea" OnClick="btnClearDrawArea_Click" />

                            </div>
                        </div>
                        <div style="margin-top: 5px;">
                            <asp:CheckBox runat="server" ID="cbxMirrorOn" Checked="true" Text="反転考慮" />
                            <asp:CheckBox runat="server" ID="cbxRotationOn" Checked="true" Text="回転考慮" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
