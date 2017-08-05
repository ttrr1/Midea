<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderCreate.aspx.cs" Inherits="MobilePages_OrderCreate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0">
    <title>下订单</title>
    <link rel="stylesheet" href="css/amazeui.min.css">
    <link rel="stylesheet" href="css/app.css">
    <script type="text/javascript" src="js/jquery-1.8.3.js"></script>
    <script src="js/jquery.json-2.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script type="text/javascript" src="js/add.js"></script>
</head>
<body>
    <style>
        .new-input2
        {
            background: url("images/xl.png") no-repeat right 5px top 50%;
            font-weight: normal;
            height: 40px;
            line-height: 34px;
            text-indent: 10px;
            vertical-align: top;
            border: 1px solid #dedede;
            width: 100%;
            float: left;
            padding-right: 10px;
            height: 40px;
            -webkit-appearance: none;
            appearance: none;
            border: 1px solid #dedede;
            font-size: 14px;
            width: 100%;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            color: #333333;
            border-radius: 0px;
        }
        #selArea 
    </style>
    <input id="ifUrl" type="hidden" value="/MobileInterface/InterfaceService.aspx" />
    <input id="hidSubEqpType" type="hidden" />
    <input id="token" type="hidden" value="<%=token %>" />
    <input id="userLoginId" type="hidden" value="<%=userLoginId %>" />
    <div class="mainbody">
        <section class="am-panel am-panel-default shouhuo am-margin-top-sm">
    <form method="post" action="" class="regform ">
        <div class="info-list">
            <div class="info pd">
                <div class="tbl-type">
                    <span class="tbl-cell w100"><span><i class="c-red">*</i>项目名称：</span></span>
                    <span class="tbl-cell">
                        <input type="text" style="width:100%;" value="" id="ProjectName" class="new-input" required>
                    </span>
                </div>
            </div>
            <div class="info pd">
                <div class="tbl-type">
                    <span class="tbl-cell w100"><span><i class="c-red">*</i>用户名称：</span></span>
                    <span class="tbl-cell">
                        <input type="text" style="width:100%;" value="" id="CustomName" class="new-input" required>
                    </span>
                </div>
            </div>
            <div class="info pd">
                <div class="tbl-type">
                    <span class="tbl-cell w100"><span><i class="c-red">*</i>用户地址：</span></span>
                    <span class="tbl-cell">
                        <select id="selPro" style="width:100%;display: none; float: left;" class="new-input2">
                           <option value="">--请选择--</option>
                       </select>
                       <select id="selCity" style="width:49%; float: left;" class="new-input2" required>
                           <option value="">--请选择--</option>
                       </select>
                       <select id="selArea" style="width:49%; float: left;" class="new-input2" required>
                           <option value="" style="padding-top: 8px;">--请选择--</option>
                       </select>
                    </span>
                </div>
            </div>
            <div class="info pd">
                <div class="tbl-type">
                    <span class="tbl-cell w100"><span><i class="c-red">*</i>详细地址：</span></span>
                    <span class="tbl-cell">
                        <input type="text" style="width:100%;" value="" id="Address" class="new-input" required>
                    </span>
                </div>
            </div>
            <div class="info pd">
                <div class="tbl-type">
                    <span class="tbl-cell w100"><span><i class="c-red">*</i>联系人：</span></span>
                    <span class="tbl-cell">
                        <input type="text" style="width:100%;" value="" id="Contact" class="new-input" required>
                    </span>
                </div>
            </div>
             <div class="info pd">
                <div class="tbl-type">
                    <span class="tbl-cell w100"><span><i class="c-red">*</i>电话号码：</span></span>
                    <span class="tbl-cell">
                        <input type="text" style="width:100%;" value="" id="Tel" class="new-input" required>
                    </span>
                </div>
            </div>
            <div class="info pd">
                <div class="tbl-type">
                    <span class="tbl-cell w100"><span>备注：</span></span>
                    <span class="tbl-cell">
                        <textarea id="Remark" style="width:100%;" class="new-input"></textarea>
                    </span>
                </div>
            </div>
             
            <div class="pd product">
                <div class="tbl-type">
                    <span class="tbl-cell2" style="  color: #999; font-size: 14px;">
                    <span><i class="c-red">*</i>产品类型：</span></span>
                    <div class="tbl-cell2">
                       <select id="eqptype" style="width:100%;"  class="new-input2 js-new-input2" required>
                           <option value="">--请选择--</option>
                       </select>
                                </div>
                </div>
            </div>
            
            <div class="include">
                <div class="info pd category1 kelong" id="aa_div" style="display: none;">
                    
                    <div id="category1" class="tbl-type tbl-type01">
                        <span class="tbl-cell2" style="  color: #999;    font-size: 14px;"><span style="  width: 50%;float: left;
    display: block;"><i class="c-red">*</i>主机型号：</span>
                        <input class="jsremover"  value="删除主型号" style="width:132px; float:left; text-aling:center;     color: #fff;
    background-color: #279be4;
    border-color: #279be4;    text-align: center;    border: 1px solid transparent;    cursor: pointer;">
                        </span>
                        <div class="tbl-cell2">
                           <select id="mainType" style="width:50%;"  class="new-input2"  required>
                                 <option value="">--请选择--</option>
                             </select>
                            <span class="goodsnumber am-fl">
                                      <a class="goodsless js-sub" id="min" href="#btn" rel="nofollow">-</a>
                                      <input type="text" class="goods-input" value="1" minnumlimit="1" maxnumlimit="20" maxlength="2" id="mainTypeNum" required onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'')">
                                      <a a class="goodsadd js-add" href="#btn" id="add" rel="nofollow">+</a>
                            </span>
                            
                        </div>
                        <div class="tbl-type">
                           <span class="w100" style="color: #999;font-size:14px;"><span><i class="c-red">*</i>内机型号：</span></span>
                        
                            <div class="tbl-cell2">
                               <select id="subType1" style="width:50%;"  class="new-input2" required>
                                     <option value="">--请选择--</option>
                                 </select>
                                <span class="goodsnumber am-fl">
                                          <a class="goodsless js-sub" id="min" href="#btn" rel="nofollow">-</a>
                                          <input type="text" class="goods-input" value="1" minnumlimit="1" maxnumlimit="20" maxlength="2" id="subType1Num" required onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') ">
                                          <a a class="goodsadd js-add" href="#btn" id="add" rel="nofollow">+</a>
                                </span>                            
                            </div>                    
                        </div>
                        <div class="tbl-type">
                        
                            <div class="tbl-cell2">
                               <select id="subType2" style="width:50%;"  class="new-input2">
                                     <option value="">--请选择--</option>
                                 </select>
                                <span class="goodsnumber am-fl">
                                          <a class="goodsless js-sub" id="min" href="#btn" rel="nofollow">-</a>
                                          <input type="text" class="goods-input" value="1" minnumlimit="1" maxnumlimit="20" maxlength="2" id="subType2Num" required onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') ">
                                          <a a class="goodsadd js-add" href="#btn" id="add" rel="nofollow">+</a>
                                </span>                            
                            </div>                    
                        </div>
                        <div class="tbl-type">
                        
                            <div class="tbl-cell2">
                               <select id="subType3" style="width:50%;"  class="new-input2">
                                     <option value="">--请选择--</option>
                                 </select>
                                <span class="goodsnumber am-fl">
                                          <a class="goodsless js-sub" id="min" href="#btn" rel="nofollow">-</a>
                                          <input type="text" class="goods-input" value="1" minnumlimit="1" maxnumlimit="20" maxlength="2" id="subType3Num" required onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') ">
                                          <a a class="goodsadd js-add" href="#btn" id="add" rel="nofollow">+</a>
                                </span>                            
                            </div>                    
                        </div>
                        <div class="tbl-type">
                        
                            <div class="tbl-cell2">
                               <select id="subType4" style="width:50%;"  class="new-input2">
                                     <option value="">--请选择--</option>
                                 </select>
                                <span class="goodsnumber am-fl">
                                          <a class="goodsless js-sub" id="min" href="#btn" rel="nofollow">-</a>
                                          <input type="text" class="goods-input" value="1" minnumlimit="1" maxnumlimit="20" maxlength="2" id="subType4Num" required onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') ">
                                          <a a class="goodsadd js-add" href="#btn" id="add" rel="nofollow">+</a>
                                </span>                            
                            </div>                    
                        </div>
                    </div>
                    <div id="zhudiv"></div>
                    <p class="am-u-sm-12 am-padding-top" style="padding-left:0;">
            <input class="am-btn am-btn-primary am-btn-block am-radius zhuadd input01" value="增加主型号" style="width:50%; float:left;" type="button" />
                <span style="float:left;line-height:40px;margin-left:10px;color:#999;font-size:14px;">继续添加一组型号</span>
            </p>
                </div>
                <div class="info pd category2 kelong" id="aa_div" style="display:none;">
                    
                    <div id="category2" class="tbl-type tbl-type02">
                        <span class="tbl-cell2" style="  color: #999;    font-size: 14px;"><span style="  width: 50%;float: left;
    display: block;"><i class="c-red">*</i>主机型号：</span>
                        <input class="jsremoverzz"  value="删除主型号" style="width:132px; float:left; text-aling:center;     color: #fff;
    background-color: #279be4;
    border-color: #279be4;    text-align: center;    border: 1px solid transparent;    cursor: pointer;">
                        </span>
                           <select id="otherType" style="width:50%;"  class="new-input2" required >
                                 <option value="">--请选择--</option>
                             </select>
                            <span class="goodsnumber am-fl">
                                      <a class="goodsless js-sub" id="min" href="#btn" rel="nofollow">-</a>
                                      <input type="text" class="goods-input" value="1" minnumlimit="1" maxnumlimit="20" maxlength="2" id="otherTypeNum" required onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " />
                                      <a a class="goodsadd js-add" href="#btn" id="add" rel="nofollow">+</a>
                            </span>
                            
                        </div>
                    </div>
                    <div id="zhudiv01"></div>
                    <p class="am-u-sm-12 am-padding-top" style="padding-left:0;">
                <input class="am-btn am-btn-primary am-btn-block am-radius zhuadd input02" value="增加主型号" style="width:50%; float:left; background-color:#279be4;border-color:#279be4;" type="button"  />
                    <span style="float:left;line-height:40px;margin-left:10px;color:#999;font-size:14px;">继续添加一组型号</span>
                </p>
                </div>
            </div> 

            <p class="am-u-sm-12 am-padding-top">
                <input id="btnAdd" type="button" class="am-btn am-btn-primary am-btn-block am-radius " style=" background-color:#279be4;border-color:#279be4;"value="提交订单">
            </p>
        </div>

    </form>
</section>
    </div>
    <div id="images">
    </div>
</body>
</html>
