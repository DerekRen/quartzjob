<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogQuery.aspx.cs" Inherits="QuartzJob.LogQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>QuartzJob日志</title>
    <script src="Content/scripts/jquery-2.2.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#TreeView1").css("height", $(window).height() - 20 + "px");
            $("#iframequery").css("height", $(window).height() - 50 + "px");
        })
    </script>
    <style type="text/css">
        body {
            font:bold 10px 'Microsoft YaHei';
        }
    </style>
</head>
<body>
    <div id="main">
        <form id="form1" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 20%; height: 100%; text-align: left; border: 1px solid #ccc;">
                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: scroll;">
                            <asp:TreeView ID="TreeView1" runat="server" text-align="left" Height="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                                NodeStyle-CssClass="text-align:left">
                                <NodeStyle CssClass="text-align:left" Width="150px" />
                            </asp:TreeView>
                        </asp:Panel>
                    </td>
                    <td style="width: 75%; height: 100%; text-align: center; border: 1px solid #ccc;">
                        <iframe src="" id="iframequery" frameborder="0" width="100%" height="90%"></iframe>
                        <asp:HiddenField ID="Hidhttphost" runat="server" />
                        <asp:HiddenField ID="hifAdress" runat="server" />
                    </td>
                </tr>
            </table>
            <div class="clear">
            </div>
        </form>
    </div>
</body>
</html>
