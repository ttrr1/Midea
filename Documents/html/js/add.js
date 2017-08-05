$(function(){
	//¼Ó
	// $(".js-add").click(function(){	    
	// 	var sum=parseInt($(this).prev(".goods-input").val())+1;
	// 	if(sum==0){return;}
	// 	$(this).prev(".goods-input").val(sum);	
	// });
	//¼õ
	// $(".js-sub").click(function(){
	// 	var sum=parseInt($(this).next(".goods-input").val())-1;
	// 	if(sum==0){return;}
	// 	$(this).next(".goods-input").val(sum);
	// });

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


	//±íµ¥ÇÐ»»
		$(".js-new-input2").change(function(){
		var _this=$(this);
		$(".include .info").hide();
		$(".include .category"+_this.val()).show();
	});
	//Ôö¼Óhtml
	$(".input01").click(function(){
		
  		$("#zhudiv").append($(".tbl-type01").first().clone());
		
		$("#zhudiv .tbl-type01:last input[type='text']").val("1");
  	});
  	$(".input02").click(function(){
  		$("#zhudiv01").append($(".tbl-type02").first().clone());
		$("#zhudiv01 .tbl-type02:last input[type='text']").val("1");
		
		
  	});
	$(".input03").click(function(){
  		$("#zhudiv02").append($(".tbl-type03").first().clone());
		$("#zhudiv02 .tbl-type03:last input[type='text']").val("1");
  	});

})