$(function () {
    var num = 1;
    var num2 = 1;
    $(".kelong").delegate(".js-add", "click", function () {
        var sum = parseInt($(this).prev(".goods-input").val()) + 1;
        if (sum == 0) { return; }
        $(this).prev(".goods-input").val(sum);

    })

    $(".kelong").delegate(".js-sub", "click", function () {
        var sum = parseInt($(this).next(".goods-input").val()) - 1;
        if (sum == 0) { return; }
        $(this).next(".goods-input").val(sum);

    })


    //触发123改变123
    // 	$(".js-new-input2").change(function(){
    // 	var _this=$(this);
    // 	$(".include .info").hide();
    // 	$(".include .category"+_this.val()).show();
    // });
    //触发123改变12
    $(".js-new-input2").change(function () {
        if ($(this).val() == '1' || $(this).val() == '263') {
            $(".include .info").hide();
            $(".include .category1").show();
        }
        else {
            $(".include .info").hide();
            $(".include .category2").show();
        }

    });
    //点击增加
    $(".input01").click(function () {
        num++;
        $("#zhudiv").append($(".tbl-type01").first().clone());

        $("#zhudiv .tbl-type01:last input[type='text']").val("1");
    });
    $(".input02").click(function () {
        num2++;
        $("#zhudiv01").append($(".tbl-type02").first().clone());
        $("#zhudiv01 .tbl-type02:last input[type='text']").val("1");
    });
    $(".input03").click(function () {
        $("#zhudiv02").append($(".tbl-type03").first().clone());
        $("#zhudiv02 .tbl-type03:last input[type='text']").val("1");
    });
    //删除选中的html
    $(".kelong").delegate(".jsremover", "click", function () {
        if (num == 1) {
            return;
        } else { $(this).parents(".tbl-type").remove(); num--; }
    });
    //只剩一个表单时禁止删除
    $(".kelong").delegate(".jsremoverzz", "click", function () {
        if (num2 == 1) {
            return;
        } else { $(this).parents(".tbl-type02").remove(); num2--; }
    });


    initPage();

    $("#eqptype").change(function () {
        $("#mainType,#mainType2,#subType1,#subType2,#subType3,#subType4,#otherType").empty();
        $("<option></option>").val("").text("--请选择--")
                        .appendTo($("#mainType,#mainType2,#subType1,#subType2,#subType3,#subType4,#otherType"));
        var json = $.parseJSON($("#hidSubEqpType").val());
        var _this = $(this).val();
        if (_this == '1' || _this == '263') {
            //主机、内机绑定
            $.each(json, function (i, item) {
                if (item["ParentModelId"] == 1) {
                    if (item["EqpType"] == 1) {
                        $("<option></option>").val(item["ModelId"]).text(item["ModelName"])
                        .appendTo($("#mainType,#mainType2"));
                    }
                    if (item["EqpType"] == 2) {
                        $("<option></option>").val(item["ModelId"]).text(item["ModelName"])
                        .appendTo($("#subType1,#subType2,#subType3,#subType4"));
                    }
                }

            });
        } else {
            $.each(json, function (i, item) {
                if (item["ParentModelId"] == _this) {
                    $("<option></option>").val(item["ModelId"]).text(item["ModelName"])
                        .appendTo($("#otherType"));
                }
            });
        }
    });

    //提交订单
    $("#btnAdd").click(function () {
        var flag = true;
        //ProjectName,CustomName,Address,Contact,Tel
        if ($.trim($("#ProjectName").val()) == '' || $.trim($("#CustomName").val()) == ''
            || $.trim($("#Address").val()) == '' || $.trim($("#Contact").val()) == ''
            || $.trim($("#Tel").val()) == '') {
            art.dialog('包含（*）字段不能为空，请检查！', function () { });
            flag = false;
        }
        if (flag) {
            if ($("#eqptype").val() == '1' || $("#eqptype").val() == '263') {
                $("#category1,#zhudiv .tbl-type01").each(function () {
                    if ($(this).find("#mainType").val() == '' || $(this).find("#subType1").val() == '') {
                        art.dialog('包含（*）字段不能为空，请检查！', function () { });
                        flag = false;
                        return false;
                    }
                });
            } else {
                $("#category2,#zhudiv01 .tbl-type02").each(function () {
                    if ($(this).find("#otherType").val() == '') {
                        art.dialog('包含（*）字段不能为空，请检查！', function () { });
                        flag = false;
                        return false;
                    }
                });
            }
        }

        if (flag) {
            //alert(11);
            createOrders();
        }

    });

});


function GetJsonData() {
    var results = [];
    if ($("#eqptype").val() == '1' || $("#eqptype").val() == '263') {
        $("#category1,#zhudiv .tbl-type01").each(function () {
            var SubModelId2 = null;
            var SubModelName2 = null;
            var SubModelNum2 = null;

            var SubModelId3 = null;
            var SubModelName3 = null;
            var SubModelNum3 = null;

            var SubModelId4 = null;
            var SubModelName4 = null;
            var SubModelNum4 = null;
            if ($(this).find("#subType2").val() != '') {
                SubModelId2 = parseInt($(this).find("#subType2").val());
                SubModelName2 = $(this).find("#subType2").find("option:selected").text();
                SubModelNum2 = parseInt($(this).find("#subType2Num").val());
            }
            if ($(this).find("#subType3").val() != '') {
                SubModelId3 = parseInt($(this).find("#subType3").val());
                SubModelName3 = $(this).find("#subType3").find("option:selected").text();
                SubModelNum3 = parseInt($(this).find("#subType3Num").val());
            }
            if ($(this).find("#subType4").val() != '') {
                SubModelId4 = parseInt($(this).find("#subType4").val());
                SubModelName4 = $(this).find("#subType4").find("option:selected").text();
                SubModelNum4 = parseInt($(this).find("#subType4Num").val());
            }
            var strOrderType2 = {
                "ProductType": $("#eqptype").val(),
                "ProductTypeName": $("#eqptype").find("option:selected").text(),
                "MainModelId": parseInt($(this).find("#mainType").val()),
                "MainModelName": $(this).find("#mainType").find("option:selected").text(),
                "MainModelNum": parseInt($(this).find("#mainTypeNum").val()),
                "SubModelId1": parseInt($(this).find("#subType1").val()),
                "SubModelName1": $(this).find("#subType1").find("option:selected").text(),
                "SubModelNum1": parseInt($(this).find("#subType1Num").val()),
                "SubModelId2": SubModelId2,
                "SubModelName2": SubModelName2,
                "SubModelNum2": SubModelNum2,
                "SubModelId3": SubModelId3,
                "SubModelName3": SubModelName3,
                "SubModelNum3": SubModelNum3,
                "SubModelId4": SubModelId4,
                "SubModelName4": SubModelName4,
                "SubModelNum4": SubModelNum4,
                "ModelId": null,
                "ModelName": null,
                "ModelNum": null
            };
            results.push(strOrderType2);
        });
    }
    else {
        $("#category2,#zhudiv01 .tbl-type02").each(function () {
            var ModelId = null;
            var ModelName = null;
            var ModelNum = null;
            if ($(this).find("#subType2").val() != '') {
                ModelId = parseInt($(this).find("#otherType").val());
                ModelName = $(this).find("#otherType").find("option:selected").text();
                ModelNum = parseInt($(this).find("#otherTypeNum").val());
            }
            var strOrderType2 = {
                "ProductType": parseInt($("#eqptype").val()),
                "ProductTypeName": $("#eqptype").find("option:selected").text(),
                "MainModelId": null,
                "MainModelName": null,
                "SubModelId1": null,
                "SubModelName1": null,
                "SubModelNum1": null,
                "SubModelId2": null,
                "SubModelName2": null,
                "SubModelNum2": null,
                "SubModelId3": null,
                "SubModelName3": null,
                "SubModelNum3": null,
                "SubModelId4": null,
                "SubModelName4": null,
                "SubModelNum4": null,
                "ModelId": ModelId,
                "ModelName": ModelName,
                "ModelNum": ModelNum
            };
            results.push(strOrderType2);
        });
    }

    return results;
}

function GetOrderInfo() {
    var results = [];
    var strOrder = {
        "ProjectName": $("#ProjectName").val(),
        "UserLoginId": $("#userLoginId").val(),
        "CustomName": $("#CustomName").val(),
        "ProvinceId": parseInt($("#selPro").val()),
        "ProvinceName": $("#selPro").find("option:selected").text(),
        "CityId": parseInt($("#selCity").val()),
        "CityName": $("#selCity").find("option:selected").text(),
        "AreaId": parseInt($("#selArea").val()),
        "AreaName": $("#selArea").find("option:selected").text(),
        "Address": $("#Address").val(),
        "Contact": $("#Contact").val(),
        "Tel": $("#Tel").val(),
        "Remark": $("#Remark").val()
    };
    results.push(strOrder);
    return results;
}

function createOrders() {
    var ordersInfo = GetOrderInfo();
    var ordersType = GetJsonData();
    var request = {
        request_id: 'create_orders',
        token: $("#token").val(),
        orders: ordersInfo,
        orders_type: ordersType
    };
    //调用了jquery.json 库  
    var encoded = $.toJSON(request);
    $.ajax({
        url: $("#ifUrl").val(),
        type: 'POST',
        data: encoded,
        dataType: 'json',
        //contentType : 'application/json',  
        success: function (data, status, xhr) {
            if (data.response_id == 1) {
                art.dialog('订单创建成功！', function () { location.href = 'OrderCreate.aspx?token=' + $("#token").val(); });
            } else {
                art.dialog('创建失败！', function () {  });
            }
        },
        Error: function (xhr, error, exception) {
            // handle the error.    
            alert(exception.toString());
        }
    });

}

function initPage() {
    var json = {
        "request_id": "orders_init",
        "token": $("#token").val()
    };

    $.ajax({
        url: $("#ifUrl").val(),
        type: 'POST',
        data: $.toJSON(json),
        dataType: 'json',
        //contentType : 'application/json',  
        success: function (data, status, xhr) {
            //Do Anything After get Return data  
            if (data.response_id == 1) {
                //父级数据
                $.each(data.parent_eqpmodel_info, function (i, item) {
                    $("<option></option>").val(item["ModelId"]).text(item["ModelName"]).appendTo($("#eqptype"));
                });

                //子级数据
                $("#hidSubEqpType").val(JSON.stringify(data.sub_eqpmodel_info));

                //区域数据
                $("#selPro,#selCity,#selArea").empty();
                $.each(data.Provice, function (i, item) {
                    $("<option></option>").val(item["Key"]).text(item["Value"]).appendTo($("#selPro"));
                });
                $.each(data.City, function (i, item) {
                    $("<option></option>").val(item["Key"]).text(item["Value"]).appendTo($("#selCity"));
                });
                $.each(data.Area, function (i, item) {
                    $("<option></option>").val(item["Key"]).text(item["Value"]).appendTo($("#selArea"));
                });
            }
        },
        Error: function (xhr, error, exception) {
            // handle the error.    
            alert(exception.toString());
        }
    });
}


 