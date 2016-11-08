<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobEdit.aspx.cs" Inherits="QuartzJob.JobEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>job编辑</title>
    <link href="Content/style/index.css" rel="stylesheet" />
    <script src="Content/scripts/jquery-2.2.0.min.js"></script>
    <script src="Content/scripts/maskBox.js"></script>
</head>
<body>
    <form runat="server">
        <div class="panel-Model">
            <div class="panel-title">
                <h5>job编辑</h5>
            </div>
            <div class="panel-content">
                <table style="margin-left: 100px; width: 930px;" class="table table-bordered thead-colored">
                    <tbody>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <label class="formKey">job名称：</label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <input type="text" id="TriggerName" name="TriggerName" runat="server"  style="width:400px" maxlength="50" class="col-3" placeholder="job名称" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <label class="formKey">job地址：</label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <input type="text" id="TriggerUrl" name="TriggerUrl"  runat="server" style="width:400px"  maxlength="255"  class="col-3" placeholder="job地址" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <label class="formKey">Cron表达式：</label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <input type="text" id="CronExpr" name="CronExpr"  runat="server" style="width:400px"  maxlength="255" class="col-3" placeholder="CronExpr表达式" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <label class="formKey">job说明：</label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="formItem col-xs-1">
                                    <textarea name="Explain" id="Explain" maxlength="10"  runat="server" style="width: 400px;margin-left:10px;" class="col-xs-1"></textarea>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="保  存" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
