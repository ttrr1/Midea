$(function(){
	//加
	// $(".js-add").click(function(){	    
	// 	var sum=parseInt($(this).prev(".goods-input").val())+1;
	// 	if(sum==0){return;}
	// 	$(this).prev(".goods-input").val(sum);	
	// });
	//减
	// $(".js-sub").click(function(){
	// 	var sum=parseInt($(this).next(".goods-input").val())-1;
	// 	if(sum==0){return;}
	// 	$(this).next(".goods-input").val(sum);
	// });
	//克隆之后的加减
	var num=1;
	var num2=1;
	$(".kelong").delegate(".js-add","click",function() {
		var sum=parseInt($(this).prev(".goods-input").val())+1;
		if(sum==0){return;}
		$(this).prev(".goods-input").val(sum);	
	
	})

	$(".kelong").delegate(".js-sub","click",function() {
		var sum=parseInt($(this).next(".goods-input").val())-1;
		if(sum==0){return;}
		$(this).next(".goods-input").val(sum);	
		
	})


	//触发123改变123
	// 	$(".js-new-input2").change(function(){
	// 	var _this=$(this);
	// 	$(".include .info").hide();
	// 	$(".include .category"+_this.val()).show();
	// });
//触发123改变12
		$(".js-new-input2").change(function(){
		if($(this).val()=='1'){
			$(".include .info").hide();
			$(".include .category1").show();}
		else{$(".include .info").hide();
			$(".include .category2").show();}
		
	});
	//点击增加
	$(".input01").click(function(){
			num++;
  		$("#zhudiv").append($(".tbl-type01").first().clone());
		
		$("#zhudiv .tbl-type01:last input[type='text']").val("1");
  	});
  	$(".input02").click(function(){  
  	num2++;		
  		$("#zhudiv01").append($(".tbl-type02").first().clone());
		$("#zhudiv01 .tbl-type02:last input[type='text']").val("1");
  	});
	$(".input03").click(function(){
  		$("#zhudiv02").append($(".tbl-type03").first().clone());
		$("#zhudiv02 .tbl-type03:last input[type='text']").val("1");
  	});
  	//删除选中的html
  $(".kelong").delegate(".jsremover","click",function() {
      if(num==1){
      	return;
      } else{     $(this).parents(".tbl-type").remove();num--;} 
    });
  //只剩一个表单时禁止删除
$(".kelong").delegate(".jsremoverzz","click",function() {
      if(num2==1){
      	return;
      } else{  $(this).parents(".tbl-type02").remove();num2--;} 
    });
//判断表单是否为空
$(".jssubmit").click(function(){
$("input").each(function(){
　　if($(this).text()=="")
	{return;}
	
　　});
  });













})