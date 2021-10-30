var Search = {
    Preview: function () {
        $.ajax({
            type: "POST",
            url: "/Search/Result/",
            cache: true,
            dataType: "script",
            data: {
                DepartId: $("#DepartId").val(),
                AreaId: $("#AreaId").val(),
                CruiseMonth: $("#CruiseMonth").val(),
                CompanyId: $("#CompanyId").val(),
                ShipId: $("#ShipId").val()
            },
            beforeSend: function () {
                $('#SearchResult').html("<p>正在搜索中 <img src='/assets/global/img/loading.gif' /></p>");
            },
            success: function (msg) {
                eval(msg);
            }
        });
    },
    Reset: function () {
        $.get("/Product/Index?DepartId=0&AreaId=0&CruiseMonth=&CompanyId=0&ShipId=0", function (data) {
            $("#divSearch").html(data);
        });
    },
    AppReset: function () {
        $.get("/App/Search/Cache/?DepartId=0&AreaId=0&CruiseMonth=&CompanyId=0&ShipId=0", function (data) {
            $("#divSearch").html(data);
        });
    },
    SelectHot: function (key, value) {
        $("#" + key).val(value);

        Search.Preview();
    },
    ShowRebate: function (userid,userguid,url, obj) {
        $.ajax({
            type: "POST",
            url: url + "/MyAgent/AgentUser/Editable?userguid=" + userguid,
            cache: true,
            dataType: "json",
            data: { pk: userid, name: "ShowRebateType", value: obj.value },
            beforeSend: function() {
                MyScript.blockUI();
            },
            success: function(json) {
                if (json.isSuccess) {
                    toastr.clear();
                    toastr["success"]("价格显示切换成功!", "系统提示");

                    location.reload();
                }
            }
        });
    }
}