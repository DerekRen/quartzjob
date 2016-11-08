<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="QuartzJob.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Content/style/index.css" rel="stylesheet" />
    <link href="Content/style/ListPageStyle2.css" rel="stylesheet" />
    <script src="Content/scripts/jquery-2.2.0.min.js"></script>
    <script src="Content/scripts/maskBox.js"></script>
    <script src="Content/scripts/jquery.form.js"></script>
    <script src="Content/scripts/common.js"></script>
</head>
<body>
    <div class="panel-Model">
        <div class="panel-title">job列表</div>
        <div class="panel-content">
            <form id="form1" action="AjaxCall.ashx" class="form">
                <input type="hidden" id="currentPage" name="currentPage" value="1" />
                <input type="hidden" id="actionName" name="actionName" value="GetSceneryTriggerInfo" />
                <div class="formItem col-xs-1">
                    <label class="formKey">job名称：</label><input type="text" id="TriggerName" name="TriggerName" class="col-xs-1" placeholder="job名称" />
                </div>
                <div class="formItem col-xs-4">
                    <label class="formKey">运行状态：</label>
                    <select name="RunStatus" id="RunStatus" class="col-xs-1" onchange="QueryDataList(1)">
                        <option value="-1">请选择</option>
                        <option value="1" selected="selected">运行</option>
                        <option value="0">停止</option>
                    </select>
                </div>
                <div class="formItem col-xs-4">
                    <label class="formKey">数据状态：</label>
                    <select name="IfValid" id="IfValid" class="col-xs-1" onchange="QueryDataList(1)">
                        <option value="-1">请选择</option>
                        <option value="1" selected="selected">有效</option>
                        <option value="0">无效</option>
                    </select>
                </div>
            </form>
        </div>
        <div class="panel-footer">
            <div class="formBtns">
                <button class="btn btn-sm btn-success" id="searchBtn">筛选</button>
                <button class="btn btn-sm btn-success" onclick="AddOrEdit(0)">新增</button>
                <button class="btn btn-sm btn-success" id="btnLogQuery">查看日志</button>
            </div>
        </div>
    </div>
    <div class="panel-Model col-1">
        <div class="panel-title">
            <h5>数据展示</h5>
        </div>
        <div id="textContent" class="panel-content">
        </div>
    </div>
    <script type="text/javascript">
        QueryDataList(1);
        $("#searchBtn").click(function (event) {
            QueryDataList(1);
        });
        function SetJobRunStatus(id, runStatus, triggerName) {
            var runStatusName = "停止";
            if (runStatus == 1) {
                runStatusName = "开启";
            }
            if (window.confirm("您确认设置" + runStatusName + "么？")) {
                $.ajax({
                    type: "GET",
                    url: "AjaxCall.ashx",
                    data: "action=SetJobRunStatus&id=" + id + "&runStatus=" + runStatus + "&triggerName=" + triggerName + "&date=" + new Date(),
                    success: function (data) {
                        QueryDataList();
                    }
                });
            }
        }
        function SetInValid(id, rowStatus, triggerName) {
            var rowStatusName = "无效";
            if (rowStatus == 1) {
                rowStatusName = "有效";
            }
            if (window.confirm("您确认设置" + rowStatusName + "么？")) {
                $.ajax({
                    type: "GET",
                    url: "AjaxCall.ashx",
                    data: "action=SetRowStatus&id=" + id + "&rowStatus=" + rowStatus + "&triggerName=" + triggerName + "&date=" + new Date(),
                    success: function (data) {
                        QueryDataList();
                    }
                });
            }
        }
        function SetRestart(id) {
            $.ajax({
                type: "GET",
                url: "AjaxCall.ashx",
                data: "action=SetRestart&id=" + id + "&date=" + new Date(),
                success: function (data) {
                    QueryDataList();
                }
            });
        }
        function AddOrEdit(id) {
            var url = "JobEdit.aspx?id=" + id;
            window.open(url);
        }
        $("#btnLogQuery").click(function () {
            var url = "LogQuery.aspx";
            window.open(url);
        });
    </script>
</body>
</html>
