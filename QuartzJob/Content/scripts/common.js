function QueryDataList(currentPage, divIDName) {
    $.maskBox.loadModel("wave");
    if (currentPage == null || currentPage == undefined || currentPage == "") {
        if ($("#currentPage") != null) {
            currentPage = $("#currentPage").val();
        } else {
            currentPage = "1";
        }
    }
    $("#currentPage").val(currentPage);
    if (divIDName == null || divIDName == undefined || divIDName == "") {
        divIDName = "textContent";
    }
    PostFormData("form1", divIDName);
}

//post-Ajax请求
function PostFormData(formID, contentDivID) {
    //父辈的from标签的action属性作为连接地址，并且发送其表单数据。method="post" id="formID"
    $("#" + formID).ajaxSubmit({
        success: function (responseData) {
            $("#" + contentDivID).html(responseData);
            $.maskBox.remove();
        },
        error: function () {
            $("#" + contentDivID).html("没有提交成功，请重新尝试。");
            $.maskBox.remove();
        }
    });
}